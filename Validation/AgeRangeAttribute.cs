using System.ComponentModel.DataAnnotations;

namespace BlazorSolution.Validation
{
    /// <summary>
    /// Validation cho tuổi với localization
    /// </summary>
    public class AgeRangeAttribute : LocalizedValidationAttribute
    {
        public int MinAge { get; set; }
        public int MaxAge { get; set; }

        public AgeRangeAttribute(int minAge, int maxAge)
        {
            MinAge = minAge;
            MaxAge = maxAge;
            ResourceKey = "AgeRange";
        }

        protected override bool IsValidValue(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return true;
            }

            if (value is not DateTime birthDate)
            {
                return false;
            }

            var age = CalculateAge(birthDate);
            return age >= MinAge && age <= MaxAge;
        }

        private int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;

            if (birthDate.Date > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }

        protected override string GetLocalizedErrorMessage(ValidationContext validationContext)
        {
            return GetLocalizedMessage(validationContext,
                validationContext.DisplayName ?? validationContext.MemberName,
                MinAge,
                MaxAge);
        }
    }
}
