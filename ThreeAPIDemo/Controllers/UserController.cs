using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using ThreeAPIDemo.Dtos;
using ThreeAPIDemo.Extensions;
using ThreeAPIDemo.ModelBInders;
using ThreeAPIDemo.Models;
using ThreeAPIDemo.Services;

namespace ThreeAPIDemo.Controllers
{
    [ApiController]
    public class UserController:BaseController
    {
        public UserController(IMapper mapper) : base(mapper)
        {
        }

        [HttpGet]

        public ActionResult<string> GetUserName(string userId, [FromServices]IUserService userService)
        {
            return Ok($"{userService.GetUserName(userId)}");
        }
        
        [HttpPost]
        public ActionResult<User> AddUser(User user)
        {
            return user;
        }

        [HttpGet("{ids}")]
        public ActionResult<List<User>> GetUsersByIds(
            [ModelBinder(BinderType = typeof(StringToListModelBinder))]IEnumerable<string> ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }

            return Ok();

        }
        
        [HttpGet("{userId}")]
        public ActionResult GetUserById(string userId)
        {
            User user = new User()
            {
                Name = "oec2003",
                Email = "oec2003@qq.com",
                Password = "123456"
            };
            return Ok(base.Mapper.Map<UserDto>(user));
        }
        
        [HttpGet]
        public ActionResult GetUsers([FromBody]string fields)
        {
            var userList =new List<User>() 
            {
                new User(){ Name = "oec2003",Email = "oec2003@qq.com",Password = "123456"},
                new User(){ Name = "oec2004",Email = "oec2004@qq.com",Password = "123456"},
                new User(){ Name = "oec2004",Email = "oec2004@qq.com",Password = "123456"}
            };
            var returnResult = base.Mapper.Map<List<UserDto>>(userList);
            return Ok(returnResult.GetData(fields));
        }
    }
}