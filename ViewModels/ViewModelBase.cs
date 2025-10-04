using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Reflection;

namespace BlazorSolution.ViewModels
{
    /// <summary>
    /// Base ViewModel với ObservableValidation support
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _errors = new();

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public bool HasErrors => _errors.Any();

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            // Validate khi property thay đổi
            if (propertyName != null)
            {
                ValidateProperty(propertyName);
            }
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public IEnumerable GetErrors(string? propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                return _errors.SelectMany(x => x.Value);

            return _errors.TryGetValue(propertyName, out var errors) ? errors : Enumerable.Empty<string>();
        }

        public void ValidateProperty(string propertyName)
        {
            var property = GetType().GetProperty(propertyName);
            if (property == null) return;

            var value = property.GetValue(this);
            var results = new List<ValidationResult>();
            var context = new ValidationContext(this) { MemberName = propertyName };

            Validator.TryValidateProperty(value, context, results);

            if (results.Any())
            {
                _errors[propertyName] = results.Select(r => r.ErrorMessage ?? "").ToList();
            }
            else
            {
                _errors.Remove(propertyName);
            }

            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public bool ValidateAll()
        {
            _errors.Clear();

            var results = new List<ValidationResult>();
            var context = new ValidationContext(this);

            Validator.TryValidateObject(this, context, results, true);

            foreach (var result in results)
            {
                var propertyName = result.MemberNames.FirstOrDefault() ?? "";
                if (!_errors.ContainsKey(propertyName))
                {
                    _errors[propertyName] = new List<string>();
                }
                _errors[propertyName].Add(result.ErrorMessage ?? "");
            }

            foreach (var propertyName in _errors.Keys)
            {
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }

            return !HasErrors;
        }

        public void ClearErrors()
        {
            var propertyNames = _errors.Keys.ToList();
            _errors.Clear();

            foreach (var propertyName in propertyNames)
            {
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        public string? GetFirstError(string propertyName)
        {
            return GetErrors(propertyName)?.Cast<string>().FirstOrDefault();
        }
    }
}