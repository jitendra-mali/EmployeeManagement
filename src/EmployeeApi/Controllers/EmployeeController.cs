using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Infrastructure.Employee.Models.Response;
using Infrastructure.Employee.Models;
using Infrastructure.Employee.Models.Request;
using Microsoft.Extensions.Logging;
using Employee.Contracts;
using System.Net;
using System.Net.Mime;
using FluentValidation;
namespace EmployeeApi.Controllers
{
    [Route("employeerequests")]
    [ApiVersion("1.0")]
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)] 
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeReader _employeeReader;
        private readonly IEmployeeAdd _employeeAdd;
        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeReader employeeReader, IEmployeeAdd employeeAdd)
        {
            _logger = logger;
            _employeeReader = employeeReader;
            _employeeAdd = employeeAdd;
        }
        [HttpGet]
        [Route("getemployees")]
        public Response GetEmployees()
        {
            _logger.LogInformation("Request received for retrieving employees",typeof(EmployeeController));
            var response = new Response();
            try
            {
                response = _employeeReader.Get();
                response.HttpStatusCode = (int)HttpStatusCode.OK;
                return response;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                response.HttpStatusCode = (int)HttpStatusCode.InternalServerError;
                response.Errors = new List<Error>() { new Error { Message = ex.Message } };
                return response;
            }
            
        }
        [HttpPut]
        [Route("addemployee")]
        public Response AddEmployee(EmployeeAddRequest employeeAddRequest)
        {

            _logger.LogInformation("Request received for adding employee", typeof(EmployeeController));
            var response = new Response();
            try
            {
                response = _employeeAdd.Add(employeeAddRequest);
                response.HttpStatusCode = (int)HttpStatusCode.OK;
                return response;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                response.HttpStatusCode = (int)HttpStatusCode.InternalServerError;
                response.Errors = new List<Error>() { new Error { Message = ex.Message } };
                return response;
            }
            
        }
    }
}
