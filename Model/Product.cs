using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace BlazorSolution.Model
{
    public class Product : ObservableValidator
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên không được để trống")]
        [MinLength(2, ErrorMessage = "Tên phải ít nhất 2 ký tự")]
        public string Name { get; set; }
        public decimal Price { get; set; }

        public void Validate()
        {
            ValidateAllProperties();
        }
    }
}
