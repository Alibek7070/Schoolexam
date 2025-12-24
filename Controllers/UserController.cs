using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using schoolexam.BLL.Services.IServices;
using schoolexam.DAL.Enum;
using schoolexam.Shared.DTOs.UserDto;

namespace SchoolExam.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register(RegisterDTO registerDto)
    {
        return Ok(await userService.RegisterAsync(registerDto));
    }
    [HttpGet("Login")]
    public async Task<IActionResult> Login([FromQuery] LoginDTO loginDto)
    {
        var response = await userService.LoginAsync(loginDto);
        return Ok(response);
    }
    [HttpPut("user-role")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AssginUserRole(int userId, Role role)
    {
        await userService.AssginUserRoleAsync(userId, role);
        return Ok($"Role Qushildi {role}");
    }
}
