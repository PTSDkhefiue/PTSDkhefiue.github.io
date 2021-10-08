using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Controllers
{
    public class ErrorController:Controller
    {
        private ILogger<ErrorController> logger;

        public ErrorController(ILogger<ErrorController>logger)
        {
            this.logger = logger;
        }
        //如果状态码为404，则路径将变为Error/404
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "抱歉，用户访问的页面不存在";
                    logger.LogWarning($"发生了一个404错误，路径={statusCodeResult.OriginalPath}以及查询字符串={statusCodeResult.OriginalQueryString}");
                    break;
            }
            return View("NotFound");
        }
        [AllowAnonymous]
        [Route("Error")]
        public IActionResult Error()
        {
            var exceptionHandlePathFeature = HttpContext.Features.Get < IExceptionHandlerPathFeature>();
            logger.LogError($"路径:{exceptionHandlePathFeature.Path},产生了一个错误{exceptionHandlePathFeature.Error}");
            //ViewBag.ExceptionPath = exceptionHandlePathFeature.Path;
            //ViewBag.ExceptionMessage = exceptionHandlePathFeature.Error.Message;
            //ViewBag.StackTrace = exceptionHandlePathFeature.Error.StackTrace;
            return View("Error");
        }

    }
}
