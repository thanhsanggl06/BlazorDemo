using System.ComponentModel.DataAnnotations;

namespace BlazorSolution.Validation
{
    /// <summary>
    /// Validation cho ngày trong khoảng hợp lệ với localization
    /// </summary>
    public class DateRangeAttribute : LocalizedValidationAttribute
    {
        public DateRangeType RangeType { get; set; }
        public int? MinDaysFromNow { get; set; }
        public int? MaxDaysFromNow { get; set; }

        public DateRangeAttribute(DateRangeType rangeType)
        {
            RangeType = rangeType;

            ResourceKey = rangeType switch
            {
                DateRangeType.Future => "FutureDate",
                DateRangeType.Past => "PastDate",
                DateRangeType.Custom => "DateRangeError",
                _ => "DateInvalid"
            };
        }

        protected override bool IsValidValue(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return true; // Dùng [Required] để check null
            }

            if (value is not DateTime dateValue)
            {
                return false;
            }

            var today = DateTime.Today;

            return RangeType switch
            {
                DateRangeType.Future => dateValue > today,
                DateRangeType.Past => dateValue < today,
                DateRangeType.Today => dateValue == today,
                DateRangeType.Custom => ValidateCustomRange(dateValue, today),
                _ => true
            };
        }

        private bool ValidateCustomRange(DateTime dateValue, DateTime today)
        {
            if (MinDaysFromNow.HasValue)
            {
                var minDate = today.AddDays(MinDaysFromNow.Value);
                if (dateValue < minDate)
                    return false;
            }

            if (MaxDaysFromNow.HasValue)
            {
                var maxDate = today.AddDays(MaxDaysFromNow.Value);
                if (dateValue > maxDate)
                    return false;
            }

            return true;
        }

        protected override string GetLocalizedErrorMessage(ValidationContext validationContext)
        {
            if (RangeType == DateRangeType.Custom && MinDaysFromNow.HasValue && MaxDaysFromNow.HasValue)
            {
                return GetLocalizedMessage(validationContext,
                    validationContext.DisplayName ?? validationContext.MemberName,
                    MinDaysFromNow.Value,
                    MaxDaysFromNow.Value);
            }

            return base.GetLocalizedErrorMessage(validationContext);
        }
    }

    public enum DateRangeType
    {
        Future,
        Past,
        Today,
        Custom
    }
}
