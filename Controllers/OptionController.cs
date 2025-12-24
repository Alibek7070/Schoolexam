using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using schoolexam.BLL.Services.IServices;
using schoolexam.DAL.Contexts;
using schoolexam.DAL.Entities;
using schoolexam.Shared.DTOs.OptionDto;
using schoolexam.Shared.DTOs.QuestionDto;

namespace schoolexam.Controllers;


[Route("api/[controller]")]
[ApiController]
public class OptionController : ControllerBase
{
    private readonly IOptionService _service;

    public OptionController(IOptionService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateAsync(CreateOptionDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var options = await _service.GetAllAsync();
        return Ok(options);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var option = await _service.GetByIdAsync(id);
        return Ok(option);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, UpdateOptionDto dto)
    {
        var option = await _service.UpdateAsync(id, dto);
        return Ok(option);
    }

}


