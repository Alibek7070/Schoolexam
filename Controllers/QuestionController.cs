using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using schoolexam.BLL.Services.IServices;
using schoolexam.DAL.Contexts;
using schoolexam.DAL.Entities;
using schoolexam.Shared.DTOs.QuestionDto;

namespace schoolexam.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuestionController : ControllerBase
{
    private readonly IQuestionService _service;

    public QuestionController(IQuestionService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateAsync(CreateQuestionDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var questions = await _service.GetAllAsync();
        return Ok(questions);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var question = await _service.GetByIdAsync(id);
        return Ok(question);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, UpdateQuestionDto dto)
    {

        var question = await _service.UpdateAsync(id, dto);
        return Ok(question);
    }
}


