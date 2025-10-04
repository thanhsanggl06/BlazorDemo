using BlazorSolution.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace BlazorSolution.Models
{
    // Login Form Model
    public class LoginViewModel : ViewModelBase
    {
        private string _email = "";
        private string _password = "";
        private bool _rememberMe;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public bool RememberMe
        {
            get => _rememberMe;
            set => SetProperty(ref _rememberMe, value);
        }
    }

    // User Registration Form Model
    public class UserRegistrationViewModel : ViewModelBase
    {
        private string _firstName = "";
        private string _lastName = "";
        private string _email = "";
        private string _phone = "";
        private string _password = "";
        private string _confirmPassword = "";
        private DateTime? _birthDate;
        private string? _gender;
        private string _country = "";
        private string _city = "";
        private string _address = "";
        private string _postalCode = "";
        private bool _agreeTerms;

        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phone
        {
            get => _phone;
            set => SetProperty(ref _phone, value);
        }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$",
            ErrorMessage = "Password must contain uppercase, lowercase and number")]
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        [Required(ErrorMessage = "Confirm password is required")]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        [Required(ErrorMessage = "Birth date is required")]
        public DateTime? BirthDate
        {
            get => _birthDate;
            set => SetProperty(ref _birthDate, value);
        }

        public string? Gender
        {
            get => _gender;
            set => SetProperty(ref _gender, value);
        }

        [Required(ErrorMessage = "Country is required")]
        public string Country
        {
            get => _country;
            set => SetProperty(ref _country, value);
        }

        [Required(ErrorMessage = "City is required")]
        public string City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }

        [Required(ErrorMessage = "Address is required")]
        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        [Required(ErrorMessage = "Postal code is required")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid postal code")]
        public string PostalCode
        {
            get => _postalCode;
            set => SetProperty(ref _postalCode, value);
        }

        [Range(typeof(bool), "true", "true", ErrorMessage = "You must agree to terms and conditions")]
        public bool AgreeTerms
        {
            get => _agreeTerms;
            set => SetProperty(ref _agreeTerms, value);
        }
    }

    // Product Form Model
    public class ProductViewModel : ViewModelBase
    {
        private string _productName = "";
        private string _sku = "";
        private string? _category;
        private decimal _price;
        private decimal _cost;
        private int _quantity;
        private string? _unit;
        private string _description = "";
        private bool _isActive = true;
        private DateTime? _availableFrom;
        private string _tags = "";

        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters")]
        public string ProductName
        {
            get => _productName;
            set => SetProperty(ref _productName, value);
        }

        [Required(ErrorMessage = "SKU is required")]
        [RegularExpression(@"^[A-Z0-9-]+$", ErrorMessage = "SKU must contain only uppercase letters, numbers and hyphens")]
        public string SKU
        {
            get => _sku;
            set => SetProperty(ref _sku, value);
        }

        [Required(ErrorMessage = "Category is required")]
        public string? Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 999999.99, ErrorMessage = "Price must be between 0.01 and 999,999.99")]
        public decimal Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        [Range(0, 999999.99, ErrorMessage = "Cost must be between 0 and 999,999.99")]
        public decimal Cost
        {
            get => _cost;
            set => SetProperty(ref _cost, value);
        }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be positive")]
        public int Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value);
        }

        public string? Unit
        {
            get => _unit;
            set => SetProperty(ref _unit, value);
        }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value);
        }

        public DateTime? AvailableFrom
        {
            get => _availableFrom;
            set => SetProperty(ref _availableFrom, value);
        }

        public string Tags
        {
            get => _tags;
            set => SetProperty(ref _tags, value);
        }

        public decimal Margin => Price > 0 ? ((Price - Cost) / Price) * 100 : 0;
    }

    // Search/Filter Form Model
    public class SearchFilterViewModel : ViewModelBase
    {
        private string _searchText = "";
        private string? _category;
        private decimal? _minPrice;
        private decimal? _maxPrice;
        private DateTime? _dateFrom;
        private DateTime? _dateTo;
        private string? _status;
        private List<string> _selectedTags = new();

        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        public string? Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

        [Range(0, 999999.99, ErrorMessage = "Min price must be positive")]
        public decimal? MinPrice
        {
            get => _minPrice;
            set => SetProperty(ref _minPrice, value);
        }

        [Range(0, 999999.99, ErrorMessage = "Max price must be positive")]
        public decimal? MaxPrice
        {
            get => _maxPrice;
            set => SetProperty(ref _maxPrice, value);
        }

        public DateTime? DateFrom
        {
            get => _dateFrom;
            set => SetProperty(ref _dateFrom, value);
        }

        public DateTime? DateTo
        {
            get => _dateTo;
            set => SetProperty(ref _dateTo, value);
        }

        public string? Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        public List<string> SelectedTags
        {
            get => _selectedTags;
            set => SetProperty(ref _selectedTags, value);
        }
    }
}