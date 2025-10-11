using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace BlazorSolution.Validation
{
    /// <summary>
    /// Base class cho custom validation attributes với hỗ trợ localization
    /// </summary>
    public abstract class LocalizedValidationAttribute : ValidationAttribute
    {
        /// <summary>
        /// Resource key để lấy message từ localization
        /// </summary>
        public string ResourceKey { get; set; } = string.Empty;

        /// <summary>
        /// Override để hỗ trợ localization
        /// </summary>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // Thực hiện validation logic
            var isValid = IsValidValue(value, validationContext);

            if (isValid)
            {
                return ValidationResult.Success;
            }

            // Lấy localized error message
            var errorMessage = GetLocalizedErrorMessage(validationContext);

            return new ValidationResult(errorMessage, new[] { validationContext.MemberName ?? string.Empty });
        }

        /// <summary>
        /// Override method này để implement validation logic
        /// </summary>
        protected abstract bool IsValidValue(object? value, ValidationContext validationContext);

        /// <summary>
        /// Lấy error message từ localization
        /// </summary>
        protected virtual string GetLocalizedErrorMessage(ValidationContext validationContext)
        {
            // Lấy IStringLocalizer từ DI container
            var localizerFactory = validationContext.GetService<IStringLocalizerFactory>();

            if (localizerFactory != null && !string.IsNullOrEmpty(ResourceKey))
            {
                var localizer = localizerFactory.Create("ValidationMessages", "BlazorSolution");
                var localizedString = localizer[ResourceKey];

                // Format message với field name nếu cần
                if (!localizedString.ResourceNotFound)
                {
                    return string.Format(localizedString.Value,
                        validationContext.DisplayName ?? validationContext.MemberName);
                }
            }

            // Fallback về ErrorMessage hoặc ResourceKey
            return ErrorMessage ?? ResourceKey ?? "Validation failed";
        }

        /// <summary>
        /// Lấy localized message với parameters
        /// </summary>
        protected string GetLocalizedMessage(ValidationContext validationContext, params object[] args)
        {
            var localizerFactory = validationContext.GetService<IStringLocalizerFactory>();

            if (localizerFactory != null && !string.IsNullOrEmpty(ResourceKey))
            {
                var localizer = localizerFactory.Create("ValidationMessages", "BlazorApp");
                var localizedString = localizer[ResourceKey, args];

                if (!localizedString.ResourceNotFound)
                {
                    return localizedString.Value;
                }
            }

            return ErrorMessage ?? ResourceKey ?? "Validation failed";
        }
    }
}
