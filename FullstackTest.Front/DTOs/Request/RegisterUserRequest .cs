﻿namespace FullstackTest.Front.DTOs.Request;

public class RegisterUserRequest
{
    public string FullName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
