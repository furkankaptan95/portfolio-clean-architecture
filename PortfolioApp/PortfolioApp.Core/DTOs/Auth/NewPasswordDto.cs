﻿namespace PortfolioApp.Core.DTOs.Auth;
public class NewPasswordDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string Token { get; set; }
}
