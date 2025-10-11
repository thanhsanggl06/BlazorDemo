using System.ComponentModel.DataAnnotations;

namespace BlazorSolution.Validation
{
    /// <summary>
    /// Validation cho mật khẩu mạnh với localization
    /// </summary>
    public class StrongPasswordAttribute : LocalizedValidationAttribute
    {
        public bool RequireUppercase { get; set; } = true;
        public bool RequireLowercase { get; set; } = true;
        public bool RequireDigit { get; set; } = true;
        public bool RequireSpecialChar { get; set; } = true;

        public StrongPasswordAttribute()
        {
            ResourceKey = "PasswordTooWeak";
        }

        protected override bool IsValidValue(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return true; // Dùng [Required] để check null
            }

            var password = value.ToString()!;

            if (RequireUppercase && !password.Any(char.IsUpper))
                return false;

            if (RequireLowercase && !password.Any(char.IsLower))
                return false;

            if (RequireDigit && !password.Any(char.IsDigit))
                return false;

            if (RequireSpecialChar && !password.Any(ch => !char.IsLetterOrDigit(ch)))
                return false;

            return true;
        }
    }
}

