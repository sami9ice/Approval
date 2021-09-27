using Api.ResponseWrapper;
using Application.Request.CustomerData;
using Application.Response.CustomerData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewCustomerActivationProcess.Controllers
{
    public class CustomerDataController : BaseApiController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<CustomerDataController> _logger;

        public CustomerDataController(ILogger<CustomerDataController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetWeatherForcast")]
        [ProducesDefaultResponseType(typeof(WeatherForecast))]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost("CustomerProfile")]
        [ProducesDefaultResponseType(typeof(CreateCustomerDataResponse))]
        public async Task<IActionResult> CreateCustomerDataAsync([FromBody] CreateCustomerDataRequest CustomerDataRequest)
        {

            (bool succeed, string message, CreateCustomerDataResponse CustomerDataResponse) = await Mediator.Send(CustomerDataRequest);
            if (succeed)
                return Ok(CustomerDataResponse.ToResponse());
            return BadRequest(message.ToResponse(false, message));
        }
        //[Authorize]
        [ProducesDefaultResponseType(typeof(GetCustomerDataByFormIDResponse[]))]
        [HttpGet("{id}/FormId")]
        public async Task<IActionResult> GetUsersById(string id)
        {
            GetCustomerByFormIdRequest Formid = new GetCustomerByFormIdRequest();
            Formid.FormId = id;
            var user = await Mediator.Send(new GetCustomerByFormIdRequest{ FormId=id});
            if (user != null)
                return Ok(user.ToResponse());
            return NotFound("No user found".ToResponse());
        }
       // [Authorize]

        [ProducesDefaultResponseType(typeof(UpdateCustomerInfoResponse))]
        [HttpPost("editCustomer")]
        public async Task<IActionResult> EditUser([FromBody] UpdateCustomerInfoRequest editCustomer)
        {
            (bool succeed, string message, UpdateCustomerInfoResponse userResponse) = await Mediator.Send(editCustomer);
            if (succeed)
                return Ok(userResponse.ToResponse());
            return BadRequest(message.ToResponse(false, message));
        }
        [ProducesDefaultResponseType(typeof(UpdateCustomerInfoResponse))]
        [HttpPost("ApprovalFlow")]
        public async Task<IActionResult> AporovalFlow([FromBody] ApprovalFlowRequest editCustomer)
        {
            (bool succeed, string message, ApprovalFlowResponse userResponse) = await Mediator.Send(editCustomer);
            if (succeed)
                return Ok(userResponse.ToResponse());
            return BadRequest(message.ToResponse(false, message));
        }
    }
}
