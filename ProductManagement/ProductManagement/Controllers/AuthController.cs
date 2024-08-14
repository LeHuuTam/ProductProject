using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.IdentityModel.Tokens;
using ProductManagement.Commands;
using ProductManagement.Models;
using ProductManagement.Queries;
using ProductManagement.Services;
using ProductManagement.Validators;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductManagement.Controllers
{
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<LoginQuery> _loginValidator;

        public AuthController(IMediator mediator, IValidator<LoginQuery> loginValidator) 
        {
            _mediator = mediator;
            _loginValidator = loginValidator;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginQuery loginQuery)
        {
            var validte = await _loginValidator.ValidateAsync(loginQuery);
            if (!validte.IsValid)
            {
                return BadRequest(validte.Errors);
            }
            try
            {
                var tokenString = await _mediator.Send(loginQuery);
                if (string.IsNullOrEmpty(tokenString))
                    return BadRequest();
                return Ok(new { Token = tokenString });
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
            
        }
    }
}
