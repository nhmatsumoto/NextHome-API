using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NextHome.Application.DTOs;
using NextHome.Application.UseCases.Properties.Interfaces;
using NextHome.Domain.Entities;

namespace NextHome.API.Controllers;

[ApiController]
[Route("[controller]")]
//[Authorize]
public class PropertyController : ControllerBase
{
    private readonly IGetAllPropertiesUseCase _getAllPropertiesUseCase;
    private readonly IGetPropertyByIdUseCase _getPropertyByIdUseCase;
    private readonly ICreatePropertyUseCase _createPropertyUseCase;
    private readonly IUpdatePropertyUseCase _updatePropertyUseCase;
    private readonly IDeletePropertyUseCase _deletePropertyUseCase;
    private readonly ILogger<PropertyController> _logger;
    private readonly IMapper _mapper;

    public PropertyController(
        ILogger<PropertyController> logger,
        IGetAllPropertiesUseCase getAllPropertiesUseCase,
        IGetPropertyByIdUseCase getPropertyByIdUseCase,
        ICreatePropertyUseCase createPropertyUseCase,
        IUpdatePropertyUseCase updatePropertyUseCase,
        IDeletePropertyUseCase deletePropertyUseCase,
        IMapper mapper)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _getAllPropertiesUseCase = getAllPropertiesUseCase ?? throw new ArgumentNullException(nameof(getAllPropertiesUseCase));
        _getPropertyByIdUseCase = getPropertyByIdUseCase ?? throw new ArgumentNullException(nameof(getPropertyByIdUseCase));
        _createPropertyUseCase = createPropertyUseCase ?? throw new ArgumentNullException(nameof(createPropertyUseCase));
        _updatePropertyUseCase = updatePropertyUseCase ?? throw new ArgumentNullException(nameof(updatePropertyUseCase));
        _deletePropertyUseCase = deletePropertyUseCase ?? throw new ArgumentNullException(nameof(deletePropertyUseCase));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PropertyDTO>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await _getAllPropertiesUseCase.ExecuteAsync(cancellationToken);
        var properties = _mapper.Map<IEnumerable<PropertyDTO>>(result);
        return Ok(properties);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PropertyDTO>> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _getPropertyByIdUseCase.ExecuteAsync(id, cancellationToken);
        if (result == null)
            return NotFound();

        var property = _mapper.Map<PropertyDTO>(result);
        return Ok(property);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] PropertyDTO propertyDto, CancellationToken cancellationToken)
    {
        var property = _mapper.Map<Property>(propertyDto);
        var propertyId = await _createPropertyUseCase.ExecuteAsync(property, cancellationToken);

        if (propertyId > 0)
        {
            return CreatedAtAction(nameof(GetById), new { id = propertyId }, propertyDto);
        }

        return BadRequest();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] PropertyDTO propertyDto, CancellationToken cancellationToken)
    {
        if (id != propertyDto.Id)
            return BadRequest("ID mismatch.");

        var existingProperty = await _getPropertyByIdUseCase.ExecuteAsync(id, cancellationToken);
        if (existingProperty == null)
            return NotFound();

        var property = _mapper.Map(propertyDto, existingProperty);
        var success = await _updatePropertyUseCase.ExecuteAsync(property, cancellationToken);

        if (!success)
            return BadRequest("Failed to update property.");

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var existingProperty = await _getPropertyByIdUseCase.ExecuteAsync(id, cancellationToken);
        if (existingProperty == null)
            return NotFound();

        var success = await _deletePropertyUseCase.ExecuteAsync(id, cancellationToken);
        if (!success)
            return BadRequest("Failed to delete property.");

        return NoContent();
    }
}
