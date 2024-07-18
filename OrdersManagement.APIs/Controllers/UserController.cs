
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrdersManagement.APIs.Errors;
using OrdersManagement.Core.Dtos;
using OrdersManagement.Core.Entities;
using OrdersManagement.Core.Repositories.Interfaces;

namespace OrdersManagement.APIs.Controllers
{
    /// <summary>
    /// API Controller for user management operations.
    /// </summary>
    public class UserController : BaseApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;


        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        #region Register
        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="model">The Registration Details.</param>
        /// <returns>The Registered User Details.</returns>
        [HttpPost("Register")]
        [ProducesResponseType(typeof(ReturnedRegisterDto), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //Check if the email exist
            if (CheckEmailExists(model.Email).Result.Value)
                return BadRequest(error: new ApiResponse(statusCode: 400, message: "Email is already exists:( "));

            var user = new User()
            {
                Email = model.Email,
                UserName = model.UserName,

            };

            var Result = await _userManager.CreateAsync(user, model.Password);

            if (!Result.Succeeded)
                return BadRequest(error: new ApiResponse(statusCode: 400, message: "Registration Failed Please Try Again:( "));

            var ReturnUser = new ReturnedRegisterDto()
            {
                UserName = user.UserName,
                Email = user.Email,
                Message = "You Successfully Register :)"

            };
            return Ok(ReturnUser);
        }
        #endregion


        #region Login
        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="model">The login credentials.</param>
        /// <returns>The logged-in user details with authentication token.</returns>
        [HttpPost("Login")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(typeof(ApiResponse), 401)]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
                return Unauthorized(new ApiResponse(statusCode: 401, message: "You are not Authorized please Register First :("));

            var Result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: false);

            if (!Result.Succeeded)
                return Unauthorized(value: new ApiResponse(statusCode: 401, message: "Your Password Incorrect Please Try Again :("));

            return Ok(value: new UserDto()
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager),
                Message = "You're Successfully Logging In :)"
            });

        }
        #endregion

        /// <summary>
        /// Checks if an email exists in the user database.
        /// </summary>
        /// <param name="email">The email address to check.</param>
        /// <returns>True if the email exists; false otherwise.</returns>
        [ProducesResponseType(typeof(bool), 200)]

        [HttpPut(template: "emailExists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }

    }
}