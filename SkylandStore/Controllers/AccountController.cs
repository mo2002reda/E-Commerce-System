using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkelandStore.Core.Entities.Identity;
using SkelandStore.Core.Services;
using SkylandStore.DTOs;
using SkylandStore.Errors;
using SkylandStore.Extentions;
using System.Security.Claims;

namespace SkylandStore.Controllers
{
    public class AccountController : ApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signIn;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signIn,
                                 ITokenService tokenService,
                                 IMapper mapper)
        {
            _userManager = userManager;
            _signIn = signIn;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        //Register
        //BaseURL/api/Account/Register
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDto model)
        {
            if (ChackEmailExist(model.Email).Result.Value)
            {
                return BadRequest(new ApiResponse(400, "This Email Already Used"));
            }

            var User = new AppUser
            {
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                PhoneNumber = model.PhoneNumber,
                DisplayName = model.DisplayName,
            };
            var Result = await _userManager.CreateAsync(User, model.Password);
            if (!Result.Succeeded) return BadRequest(new ApiResponse(404));

            var ReturnDTO = new UserDTO
            {
                Email = User.Email,
                UserName = User.UserName,
                Token = await _tokenService.CreateTokenAsync(User, _userManager)
            };
            return Ok(ReturnDTO);
        }

        //Login
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO model)
        {   //1)Get User From database
            var User = await _userManager.FindByEmailAsync(model.Email);
            if (User is null) return Unauthorized(new ApiResponse(401));
            //2)Check at Password 
            var Result = await _signIn.CheckPasswordSignInAsync(User, model.Password, false);
            if (!Result.Succeeded) return BadRequest(new ApiResponse(401));
            return Ok(new UserDTO
            {
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                Token = await _tokenService.CreateTokenAsync(User, _userManager)
            });
        }


        //Get Current User ==>baseURL/api/Account/GetCurrentuser
        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDTO>> GetCurrentuser()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);//User Is the Prop That has all claims Of Current User
            var user = await _userManager.FindByEmailAsync(Email);
            var ReturenUser = new UserDTO
            {
                Email = user.Email,
                UserName = user.UserName,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            };
            return Ok(ReturenUser);
        }

        //Get Adderss Of Current User => BaseURL/api/Account/Address
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDTO>> GetAddress()
        {
            var user = await _userManager.FindUserWithAddressAsync(User);
            var MappedAddress = _mapper.Map<Address, AddressDTO>(user.Address);
            return Ok(MappedAddress);
        }

        //Update Address Of User => BaseUrl/api/Account/NewAddress
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDTO>> UpdateAddress(AddressDTO address)
        {
            var user = await _userManager.FindUserWithAddressAsync(User);
            var MappedAddress = _mapper.Map<AddressDTO, Address>(address);
            MappedAddress.Id = user.Address.Id;
            user.Address = MappedAddress;
            var Result = await _userManager.UpdateAsync(user);
            if (!Result.Succeeded) return BadRequest(new ApiResponse(400));
            return Ok(address);
        }

        //Check If Email Exist => BaseURL/api/Account/EmailExist
        [HttpGet("EmailExist")]
        public async Task<ActionResult<bool>> ChackEmailExist(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null ? true : false;
        }
    }
}
