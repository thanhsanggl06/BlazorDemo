using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
namespace BlazorSolution.Model
{
    /// <summary>
    /// Custom validator để kiểm tra tên đăng nhập chỉ chứa chữ cái, số và gạch dưới
    /// </summary>
    public class UsernameValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var username = value.ToString();
            var regex = new Regex(@"^[a-zA-Z0-9_]+$");

            if (!regex.IsMatch(username))
            {
                return new ValidationResult(
                    ErrorMessage ?? "Tên đăng nhập chỉ được chứa chữ cái, số và gạch dưới"
                );
            }

            return ValidationResult.Success;
        }
    }

    /// <summary>
    /// Custom validator để kiểm tra mật khẩu mạnh
    /// Yêu cầu: ít nhất 1 chữ hoa, 1 chữ thường, 1 số
    /// </summary>
    public class StrongPasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var password = value.ToString();

            var hasUpperCase = new Regex(@"[A-Z]").IsMatch(password);
            var hasLowerCase = new Regex(@"[a-z]").IsMatch(password);
            var hasNumber = new Regex(@"\d").IsMatch(password);

            if (!hasUpperCase || !hasLowerCase || !hasNumber)
            {
                return new ValidationResult(
                    ErrorMessage ?? "Mật khẩu phải chứa ít nhất 1 chữ hoa, 1 chữ thường và 1 số"
                );
            }

            return ValidationResult.Success;
        }
    }

    /// <summary>
    /// Custom validator để kiểm tra số điện thoại Việt Nam
    /// </summary>
    public class PhoneNumberValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var phoneNumber = value.ToString();
            // Loại bỏ khoảng trắng và dấu gạch ngang
            phoneNumber = phoneNumber.Replace(" ", "").Replace("-", "");

            // Kiểm tra số điện thoại Việt Nam (10-11 số, bắt đầu bằng 0)
            var regex = new Regex(@"^0\d{9,10}$");

            if (!regex.IsMatch(phoneNumber))
            {
                return new ValidationResult(
                    ErrorMessage ?? "Số điện thoại không hợp lệ (phải bắt đầu bằng 0 và có 10-11 số)"
                );
            }

            return ValidationResult.Success;
        }
    }

    /// <summary>
    /// Custom validator để kiểm tra ngày sinh hợp lệ (không được trong tương lai)
    /// </summary>
    public class DateNotInFutureAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (value is DateTime dateValue)
            {
                if (dateValue > DateTime.Now)
                {
                    return new ValidationResult(
                        ErrorMessage ?? "Ngày không được là ngày trong tương lai"
                    );
                }
            }

            return ValidationResult.Success;
        }
    }

    /// <summary>
    /// Custom validator để kiểm tra giá trị lớn hơn một giá trị khác
    /// </summary>
    public class GreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public GreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var currentValue = value as IComparable;
            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
            {
                return new ValidationResult($"Property {_comparisonProperty} không tồn tại");
            }

            var comparisonValue = property.GetValue(validationContext.ObjectInstance) as IComparable;

            if (currentValue == null || comparisonValue == null)
            {
                return ValidationResult.Success;
            }

            if (currentValue.CompareTo(comparisonValue) <= 0)
            {
                return new ValidationResult(
                    ErrorMessage ?? $"Giá trị phải lớn hơn {_comparisonProperty}"
                );
            }

            return ValidationResult.Success;
        }
    }

    /// <summary>
    /// Custom validator để kiểm tra URL hợp lệ
    /// </summary>
    public class ValidUrlAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var url = value.ToString();

            if (!Uri.TryCreate(url, UriKind.Absolute, out var uriResult) ||
                (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
            {
                return new ValidationResult(
                    ErrorMessage ?? "URL không hợp lệ (phải bắt đầu với http:// hoặc https://)"
                );
            }

            return ValidationResult.Success;
        }
    }
}
