using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BlazorSolution.Validation
{
    /// <summary>
    /// Validation attribute cho số điện thoại Việt Nam với localization
    /// </summary>
    public class PhoneNumberAttribute : LocalizedValidationAttribute
    {
        public PhoneNumberAttribute()
        {
            ResourceKey = "PhoneInvalid";
        }

        protected override bool IsValidValue(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return true; // Dùng [Required] để check null
            }

            var phoneNumber = value.ToString()!;

            // Regex cho số điện thoại Việt Nam: 0xxxxxxxxx hoặc +84xxxxxxxxx
            var regex = new Regex(@"^(0|\+84)[0-9]{9,10}$");

            return regex.IsMatch(phoneNumber);
        }
    }
}
