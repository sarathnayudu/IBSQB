using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IntuitSampleMVC.Models
{
    public class RegisterModel:ModelBase
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
    public class PreLogin:ModelBase
    {
        public string Companyname { get; set; }
        public string UserName { get; set; }
    }
    public class LogOnModel
    {
        public string CompanyName { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
    public class ChangePasswordModel
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
    public class ForgotPasswordModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "email address")]
        public string Email { get; set; }
    }

    public class PasswordResetModel
    {
        [Required]
        [Display(Name = "Reset Token")]
        public string ResetToken { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}