using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SmartApartment.Api.Controllers
{
    public class BaseController : ControllerBase
    {

        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;
        public BaseController(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger(this.GetType());
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        protected ObjectResult HandleException(Exception e, [CallerMemberName] string callerName = "")
        {
            _logger.LogError($"{this.GetType().Name} {callerName} ERROR: {e}");
            int statusCode = (int)HttpStatusCode.InternalServerError;
            return StatusCode(statusCode, $"{callerName} has some error. Reason: {e}");
        }
    }
}
