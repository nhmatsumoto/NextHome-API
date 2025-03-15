﻿namespace NextHome.Application.DTOs;

public class UserDTO
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsAvailable { get; set; }
}
