using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data.Abstraction;
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
    public class MasterController : ControllerBase
    { 
        private readonly IDataContext _DataContext;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeController> _logger;


        /// <summary>
        /// Sandeep More 24/04/2020
        /// Get Master Data 
        /// </summary>
        /// <param name="_dataContext">DB Context</param>
        /// <param name="mapper">AutoMapper</param>
        /// <param name="logger">Logger</param>
        public MasterController(IDataContext _dataContext, IMapper mapper,
        ILogger<EmployeeController> logger)
        {
            _DataContext = _dataContext;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("Hobbies/[controller]")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Hobby>>> Hobbies()
        {

            var hobbies = await _DataContext.QueryBuilder<Hobby>() 
                .ToListAsync().ConfigureAwait(false);
            var obj = _mapper.Map<IEnumerable<HobbyModel>>(hobbies);
            return Ok(obj);

        }
        [HttpGet("Qualifications/[controller]")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Qualification>>> Qualifications()
        {

            var qualifications = await _DataContext.QueryBuilder<Qualification>()
                .ToListAsync().ConfigureAwait(false);
            return Ok(qualifications);

        }
    }
}