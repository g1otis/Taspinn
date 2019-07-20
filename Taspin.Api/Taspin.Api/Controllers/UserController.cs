﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taspin.Api.Services;
using Taspin.Data.Dac;
using Taspin.Data.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Taspin.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UsersDac _dac;
        private readonly IUserService _userService;

        public UserController(UsersDac dac, IUserService userService)
        {
            _dac = dac;
            _userService = userService;
        }

        [HttpGet()]
        public ActionResult<List<UserModel>> Get()
        {
            return _dac.SelectUsers();
        }

        // GET api/values/5
        [HttpGet("{userName}")]
        public ActionResult<User> Get(string userName)
        {
            return _userService.Get(userName);
        }
    }
}