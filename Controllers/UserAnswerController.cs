using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using schoolexam.BLL.Services.IServices;
using schoolexam.DAL.Contexts;
using schoolexam.DAL.Entities;
using schoolexam.Shared.DTOs.OptionDto;
using schoolexam.Shared.DTOs.UserAnswerDto;
using schoolexam.Shared.DTOs.UserAttempDto;

namespace schoolexam.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserAnswerController : ControllerBase
{
    private readonly IUserAnswerService _service;

    public UserAnswerController(IUserAnswerService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateAsync(CreateUserAnswerDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var list = await _service.GetAllAsync();
        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var item = await _service.GetByIdAsync(id);
        return Ok(item);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(int id ,UpdateUserAnswerDto dto)
    {
        var item = await _service.UpdateAsync(id , dto);
        return Ok(item);
    }
}
