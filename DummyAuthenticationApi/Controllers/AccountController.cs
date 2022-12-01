using System.Diagnostics;
using System.Text.Json;
using DummyAuthenticationApi.Models;
using DummyAuthenticationApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DummyAuthenticationApi.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    private readonly ITokenService _tokenService;
    
    public AccountController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }
    
    
    [HttpPost("v1/login")]
    public IActionResult Login() => Ok(_tokenService.GenerateTokem(new User
    {
        Name = "Dummy",
        Email = "dummy@test.com"
    }));

    [Authorize]
    [HttpGet("v1/test1")]
    public IActionResult Test1()
    {
        Console.WriteLine("Hi");
        return Ok(User.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value);
    } 
    
}