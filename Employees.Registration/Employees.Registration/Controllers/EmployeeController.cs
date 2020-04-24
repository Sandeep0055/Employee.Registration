using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data.Abstraction;
using Data.Abstraction.Model;
using Data.Abstraction.Models;
using Employees.Registration.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Employees.Registration.Controllers
{
    [Route("api")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IDataContext _DataContext;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeController> _logger;

        /// <summary>
        /// Sandeep More 24/04/2020
        /// Save Employee Data 
        /// </summary>
        /// <param name="_dataContext">DB Context</param>
        /// <param name="mapper">AutoMapper</param>
        /// <param name="logger">Logger</param>
        public EmployeeController(IDataContext _dataContext, IMapper mapper,
            ILogger<EmployeeController> logger)
        {
            _DataContext = _dataContext;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet("[controller]")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        { 
           
            var employees = await _DataContext.QueryBuilder<Employee>() 
                .Include(s=>s.Qualification)
                .ToListAsync().ConfigureAwait(false);
            return Ok(_mapper.Map<IEnumerable<EmployeeModel>>(employees));
           
        }
        [HttpPost("[controller]")]
        [ProducesResponseType(201)]
        public async Task<ActionResult> Save(EmployeeModel employeeModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employeeExist = await _DataContext.QueryBuilder<Employee>()
               .ToListAsync()
               .ConfigureAwait(false);

            var isExist = employeeExist.Where(s => s.Name == employeeModel.Name).ToList();
            if (isExist.Count > 0)
                return BadRequest(ModelState);

            employeeModel.Hobbyies = employeeModel.Hobbyies.Replace("undefined,", "");

            var id = 0L;

            if (await _DataContext.QueryBuilder<Employee>().AnyAsync().ConfigureAwait(false))
            {
                id = await _DataContext.QueryBuilder<Employee>().MaxAsync(x => x.Id).ConfigureAwait(false);
            }

            var empoyee = _mapper.Map<Employee>(employeeModel);
            empoyee.Id = empoyee.Id = ++id;

            await _DataContext.InsertAsync(empoyee).ConfigureAwait(false);
            await _DataContext.SaveAsync().ConfigureAwait(false);

            return CreatedAtAction(nameof(Get), new { employeeModel.Id }, employeeModel);
        }
        [HttpGet("{id}/[controller]")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Employee>> Get(long id)
        {
            var employee = await _DataContext.QueryBuilder<Employee>()
               .Include(s=>s.Qualification)
                .FirstOrDefaultAsync(x => x.Id == id)
                .ConfigureAwait(false);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<EmployeeModel>(employee));
        }
        [HttpPut("{id}/[controller]")]
        public async Task<ActionResult> Update(long id, EmployeeModel employeeModel)
        {
            var employee = await _DataContext.QueryBuilder<Employee>()
                .FirstOrDefaultAsync(a => a.Id == id)
                .ConfigureAwait(false);

            if (employee == null)
            {
                return NotFound();
            }

            _mapper.Map(employeeModel, employee);


            await _DataContext.SaveAsync().ConfigureAwait(false);

         
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult> Delete(int id)
        {
            var ruleGroup = await _DataContext.QueryBuilder<Employee>()
                .FirstOrDefaultAsync(a => a.Id == id).ConfigureAwait(false);

            if (ruleGroup == null)
            {
                return NotFound();
            }

            await _DataContext.DeleteAsync(ruleGroup).ConfigureAwait(false);
            await _DataContext.SaveAsync().ConfigureAwait(false);  

            return NoContent();
        }


    }
}