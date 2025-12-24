using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using schoolexam.BLL.Services.IServices;
using schoolexam.Shared.DTOs.UserAttempDto;

namespace schoolexam.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserAttempController : ControllerBase
{
    private readonly IUserAttempService _service;

    public UserAttempController(IUserAttempService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateAsync(CreateUserAttempDto dto)
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
    public async Task<IActionResult> UpdateAsync(int id,UpdateUserAttempDto dto)
    {
        var item = await _service.UpdateAsync(id , dto);
        return Ok(item);
    }

}
