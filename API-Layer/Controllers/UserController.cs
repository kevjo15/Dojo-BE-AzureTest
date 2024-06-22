﻿using System.Security.Claims;
using Application_Layer.Commands.DeleteUser;
using Application_Layer.Commands.RegisterNewUser;
using Application_Layer.Commands.UpdateUser;
using Application_Layer.DTO_s;
using Application_Layer.Queries.GetAllUsers;
using Application_Layer.Queries.GetUserByEmail;
using Application_Layer.Queries.GetUserById;
using Application_Layer.Queries.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Application_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterUserDTO userDto)
        {
            try
            {
                return Ok(await _mediator.Send(new RegisterUserCommand(userDto)));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUserDTO)
        {
            try
            {
                var loginResult = await _mediator.Send(new LoginUserQuery(loginUserDTO));

                if (loginResult.Successful)
                {
                    return Ok(new { token = loginResult.Token });
                }
                else
                {
                    return BadRequest(loginResult.Error);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Teacher")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(id));

            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound($"User with ID {id} was not found.");
            }
        }

        [Authorize(Roles = "Teacher")]
        [HttpGet("by-email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _mediator.Send(new GetUserByEmailQuery(email));

            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound($"User with Email {email} was not found.");
            }
        }

        [HttpGet]
        [Route("GetAllUsers")]
        //[Authorize(Roles = "Teacher")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                return Ok(await _mediator.Send(new GetAllUsersQuery()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPut("updateUser")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdatingUserDTO userDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var command = new UpdateUserCommand(userDto);

            try
            {
                var result = await _mediator.Send(command);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Failed to update user information.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Teacher")]
        [HttpDelete("deleteUser/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not recognized.");
            }

            var command = new DeleteUserCommand(userId);
            var result = await _mediator.Send(command);

            if (result)
            {
                return Ok("User successfully deleted.");
            }
            else
            {
                return BadRequest("Failed to delete the user.");
            }
        }
    }
}
