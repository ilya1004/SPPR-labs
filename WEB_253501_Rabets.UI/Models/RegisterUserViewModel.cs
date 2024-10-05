﻿using System.ComponentModel.DataAnnotations;

namespace WEB_253501_Rabets.UI.Models;

public class RegisterUserViewModel
{
    [Required] 
    public string Email { get; set; }

    [Required] 
    public string Password { get; set; }

    [Required]
    [Compare(nameof(Password))] 
    public string ConfirmPassword { get; set; }

    public IFormFile? Avatar { get; set; }
}
