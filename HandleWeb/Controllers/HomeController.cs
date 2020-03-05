using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HandleWeb.Models;
using FluentValidation;
using SharedHelper.Exceptions;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using ApplicationHelper.Responses;
using ApplicationHelper.Requests;
using DataBase.ServiceRepository;
using ApplicationHelper.Messages;

namespace HandleWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> AddUser([FromBody]AddUserRequest request)
        {
            // Reponse
            var response = await UserService.AddUser(request);

            // Return
            return CreatedAtRoute("Users_GetUser", new { response.UserId }, response);
        }


        public IActionResult Index()
        {
            var a = 1;
            var b = 2;
            if (a != b)
            {
                throw new ConflictException(UserMessage.UserAlreadyExists);
            }
            else
            {
                throw new ConflictException(UserMessage.UserAlreadyExists);
            }

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
