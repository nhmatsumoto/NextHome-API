using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NextHome.Application.DTOs;
using NextHome.Application.UseCases.Users.Interfaces;

namespace NextHome.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ICreateUserUseCase _createUser;
    private readonly IGetUserByIdUseCase _getUserById;
    private readonly IGetAllUsersUseCase _getAllUsers;
    private readonly IUpdateUserUseCase _updateUser;
    private readonly IDeleteUserUseCase _deleteUser;
    private readonly ILogger<UserController> _logger;
    private readonly IMapper _mapper;

    public UserController(
        ICreateUserUseCase createUser,
        IGetUserByIdUseCase getUserById,
        IGetAllUsersUseCase getAllUsers,
        IUpdateUserUseCase updateUser,
        IDeleteUserUseCase deleteUser,
        ILogger<UserController> logger,
        IMapper mapper)
    {
        _createUser = createUser ?? throw new ArgumentNullException(nameof(createUser));
        _getUserById = getUserById ?? throw new ArgumentNullException(nameof(getUserById));
        _getAllUsers = getAllUsers ?? throw new ArgumentNullException(nameof(getAllUsers));
        _updateUser = updateUser ?? throw new ArgumentNullException(nameof(updateUser));
        _deleteUser = deleteUser ?? throw new ArgumentNullException(nameof(deleteUser));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    //[Authorize(Roles = "admin")] // Apenas administradores podem visualizar todos os usuários
    public async Task<ActionResult<IEnumerable<PropertyDTO>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await _getAllUsers.Execute(cancellationToken);
        var users = _mapper.Map<IEnumerable<UserDTO>>(result);
        return Ok(users);
    }

    [HttpGet("{id}")]
    //[Authorize] // Apenas usuários autenticados podem acessar
    public async Task<ActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var user = await _getUserById.Execute(id, cancellationToken);
        return user == null ? NotFound(new { Message = "Usuário não encontrado" }) : Ok(user);
    }

    [HttpPost]
    //[Authorize(Roles = "admin")] // Apenas administradores podem criar usuários
    public async Task<ActionResult> Create([FromBody] CreateUserDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var id = await _createUser.Execute(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, new { Id = id });
    }


    [HttpPut("{id}")]
    //[Authorize] // Apenas usuários autenticados podem atualizar seus dados
    public async Task<ActionResult> Update(int id, [FromBody] UpdateUserDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        dto.Id = id;
        var result = await _updateUser.Execute(dto, cancellationToken);
        return result ? NoContent() : NotFound(new { Message = "Usuário não encontrado" });
    }

    [HttpDelete("{id}")]
    //[Authorize(Roles = "admin")] // Apenas administradores podem excluir usuários
    public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _deleteUser.Execute(id, cancellationToken);
        return result ? NoContent() : NotFound(new { Message = "Usuário não encontrado" });
    }
}
