using GTCOAgileCOEPortal.Data;
using GTCOAgileCOEPortal.Dtos;
using GTCOAgileCOEPortal.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GTCOAgileCOEPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ChatService _chatService;
        private readonly Database _db;
        public ChatController(ChatService chatService, Database db)
        {
            _chatService = chatService;
            _db = db;
        }

        

        [HttpPost("register-user")]
        public IActionResult RegisterUser([FromBody]UserDto newUser)
        {
            int saltLength = 10;
            User model = new()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = newUser.FirstName.ToLower(),
                LastName = newUser.LastName.ToLower(),
                Email = newUser.Email,
                Role = newUser.Role,
                Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password, saltLength),
                CreatedAt = DateTime.Now,
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorResponse { Error = "Invalid request body" });
            }
;
            _db.dataBase.Add(model);

            //_db.Users.Add(model);
            //_db.SaveChanges();

            if (_chatService.AddUserToList(model.Id))
            {
                //return NoContent();
                return Ok(model);
            }

            return BadRequest(new ErrorResponse { Error = "user already exist" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody]UserLogin req)
        {
            User user = _db.dataBase.FirstOrDefault(u => u.Email == req.Email);
            if (user == null)
            {
                return NotFound((new ErrorResponse { Error = $"Invalid login credentials, user not found {req.Email}" }));
            }

            bool PasswordMatch = BCrypt.Net.BCrypt.Verify(req.Password, user.Password);
            if (!PasswordMatch)
            {
                return Unauthorized(new ErrorResponse { Error = "Invalid login credentials" });
            }

            return Ok(user);
        }

        [HttpGet("Users")]
        public IActionResult Users()
        {
            return Ok(_db.dataBase);
        }

        [HttpGet("productOwnerMessages")]
        public IActionResult ProductOwnerMessages()
        {
            return Ok(_db.ProdOwnerMessHistory);
        }
    }
}
