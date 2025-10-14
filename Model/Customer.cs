using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlazorSolution.Model
{
    public class Customer : ObservableValidator, IValidationModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên không được để trống")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Số dt không được để trống")]
        public string PhoneNumber { get; set; }

        public void Validate()
        {
            ValidateAllProperties();
        }
    }
}
