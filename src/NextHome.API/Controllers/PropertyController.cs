using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextHome.Application.DTOs;
using NextHome.Domain.Entities;
using NextHome.Domain.Interfaces;

namespace NextHome.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class PropertyController : ControllerBase
{
    private readonly ILogger<PropertyController> _logger;
    private readonly IPropertyService _propertyService;
    private readonly IMapper _mapper;
    public PropertyController(ILogger<PropertyController> logger, IPropertyService propertyService, IMapper mapper)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _propertyService = propertyService ?? throw new ArgumentNullException(nameof(propertyService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Property>>> GetAll()
    {
        var result = await _propertyService.GetAllPropertiesAsync();
        var properties = _mapper.Map<IEnumerable<PropertyDTO>>(result);
        return Ok(properties);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PropertyDTO>> GetById(int id)
    {
        var result = await _propertyService.GetPropertyByIdAsync(id);
        if (result == null)
            return NotFound();

        var property = _mapper.Map<PropertyDTO>(result);
        return Ok(property);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] PropertyDTO propertyDto)
    {
        var property = _mapper.Map<Property>(propertyDto);
        var result = await _propertyService.AddPropertyAsync(property);
        if (result == 1)
        {
            return CreatedAtAction(nameof(GetById), new { id = property.Id }, propertyDto);
        }

        return BadRequest();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] PropertyDTO propertyDto)
    {
        if (id != propertyDto.Id)
            return BadRequest("ID mismatch.");

        var existingProperty = await _propertyService.GetPropertyByIdAsync(id);
        if (existingProperty == null)
            return NotFound();

        var property = _mapper.Map(propertyDto, existingProperty);
        await _propertyService.UpdatePropertyAsync(property);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existingProperty = await _propertyService.GetPropertyByIdAsync(id);
        if (existingProperty == null)
            return NotFound();

        await _propertyService.DeletePropertyAsync(id);
        return NoContent();
    }
}
