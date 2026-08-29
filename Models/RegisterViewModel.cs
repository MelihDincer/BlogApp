using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models;

public class RegisterViewModel
{
    [Required]
    [Display(Name = "UserName")]
    public string? UserName { get; set; }
    [Required]
    [Display(Name = "Ad Soyad")]
    public string? Name { get; set; }
    [Required]
    [EmailAddress]
    [Display(Name = "E-posta")]
    public string? Email { get; set; }
    [Required]
    [StringLength(10, ErrorMessage ="{0} alanı en az {2} karakter uzunluğunda olmalıdır.", MinimumLength = 6)]
    [Display(Name = "Parola")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    [Required]
    [Compare(nameof(Password), ErrorMessage = "Parolanız eşleşmiyor.")]
    [Display(Name = "Parola Tekrar")]
    [DataType(DataType.Password)]
    public string? ConfirmPassword { get; set; }
}