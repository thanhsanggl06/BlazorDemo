using System.ComponentModel.DataAnnotations;

namespace BlazorSolution.Validation
{
    /// <summary>
    /// So sánh 2 fields (như Password confirmation) với localization
    /// </summary>
    public class CompareFieldAttribute : LocalizedValidationAttribute
    {
        public string OtherProperty { get; set; }

        public CompareFieldAttribute(string otherProperty)
        {
            OtherProperty = otherProperty;
            ResourceKey = "FieldsDoNotMatch";
        }

        protected override bool IsValidValue(object? value, ValidationContext validationContext)
        {
            var otherPropertyInfo = validationContext.ObjectType.GetProperty(OtherProperty);

            if (otherPropertyInfo == null)
            {
                return false;
            }

            var otherValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance);

            if (value == null && otherValue == null)
                return true;

            if (value == null || otherValue == null)
                return false;

            return value.Equals(otherValue);
        }

        protected override string GetLocalizedErrorMessage(ValidationContext validationContext)
        {
            // Lấy display name của field cần so sánh
            var otherPropertyInfo = validationContext.ObjectType.GetProperty(OtherProperty);
            var otherDisplayName = OtherProperty;

            if (otherPropertyInfo != null)
            {
                var displayAttr = otherPropertyInfo.GetCustomAttributes(typeof(System.ComponentModel.DisplayNameAttribute), true)
                    .FirstOrDefault() as System.ComponentModel.DisplayNameAttribute;

                if (displayAttr != null)
                {
                    otherDisplayName = displayAttr.DisplayName;
                }
            }

            return GetLocalizedMessage(validationContext,
                validationContext.DisplayName ?? validationContext.MemberName,
                otherDisplayName);
        }
    }
}
