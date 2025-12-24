using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using schoolexam.BLL.Services.IServices;
using schoolexam.DAL.Contexts;
using schoolexam.DAL.Entities;
using schoolexam.Shared.DTOs.TestDto;

namespace QuizTest.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly ITestService _service;

    public TestController(ITestService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateAsync([FromBody] CreateTestDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var tests = await _service.GetAllAsync();
        return Ok(tests);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var test = await _service.GetByIdAsync(id);
        return Ok(test);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, UpdateTestDto dto)
    {
        var test = await _service.UpdateAsync(id, dto);
        return Ok(test);
    }

}
