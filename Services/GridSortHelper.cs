using System.Reflection;
using System.Linq.Expressions;

namespace BlazorSolution.Services
{
    public static class GridSortHelper
    {
        public static List<T> SortByProperty<T>(List<T> data, string propertyName, bool ascending)
        {
            if (string.IsNullOrEmpty(propertyName) || data == null || !data.Any())
                return data;

            try
            {
                var propertyInfo = typeof(T).GetProperty(propertyName);
                if (propertyInfo == null)
                {
                    Console.WriteLine($"Property '{propertyName}' not found");
                    return data;
                }

                // ✅ XÁC ĐỊNH KIỂU DỮ LIỆU TỪ BINDING
                var propertyType = propertyInfo.PropertyType;
                var isNullable = Nullable.GetUnderlyingType(propertyType) != null;
                var actualType = isNullable ? Nullable.GetUnderlyingType(propertyType) : propertyType;

                Console.WriteLine($"Sorting '{propertyName}' - Type: {actualType.Name}, Nullable: {isNullable}");

                // ✅ SORT THEO ĐÚNG KIỂU DỮ LIỆU
                return SortByPropertyType(data, propertyInfo, propertyType, ascending);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Sort error: {ex.Message}");
                return data;
            }
        }

        private static List<T> SortByPropertyType<T>(List<T> data, PropertyInfo propertyInfo, Type propertyType, bool ascending)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyInfo);

            // ✅ XỬ LÝ THEO TỪNG KIỂU DỮ LIỆU
            if (propertyType == typeof(string))
            {
                // String: Case-insensitive, null handling
                var lambda = Expression.Lambda<Func<T, string>>(property, parameter);
                return ascending
                    ? data.OrderBy(lambda.Compile(), StringComparer.OrdinalIgnoreCase).ToList()
                    : data.OrderByDescending(lambda.Compile(), StringComparer.OrdinalIgnoreCase).ToList();
            }
            else if (propertyType == typeof(int) || propertyType == typeof(int?))
            {
                var conversion = Expression.Convert(property, typeof(int?));
                var lambda = Expression.Lambda<Func<T, int?>>(conversion, parameter);
                return ascending
                    ? data.OrderBy(lambda.Compile()).ToList()
                    : data.OrderByDescending(lambda.Compile()).ToList();
            }
            else if (propertyType == typeof(decimal) || propertyType == typeof(decimal?))
            {
                var conversion = Expression.Convert(property, typeof(decimal?));
                var lambda = Expression.Lambda<Func<T, decimal?>>(conversion, parameter);
                return ascending
                    ? data.OrderBy(lambda.Compile()).ToList()
                    : data.OrderByDescending(lambda.Compile()).ToList();
            }
            else if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
            {
                var conversion = Expression.Convert(property, typeof(DateTime?));
                var lambda = Expression.Lambda<Func<T, DateTime?>>(conversion, parameter);
                return ascending
                    ? data.OrderBy(lambda.Compile()).ToList()
                    : data.OrderByDescending(lambda.Compile()).ToList();
            }
            else if (propertyType == typeof(bool) || propertyType == typeof(bool?))
            {
                var conversion = Expression.Convert(property, typeof(bool?));
                var lambda = Expression.Lambda<Func<T, bool?>>(conversion, parameter);
                return ascending
                    ? data.OrderBy(lambda.Compile()).ToList()
                    : data.OrderByDescending(lambda.Compile()).ToList();
            }
            else if (propertyType == typeof(double) || propertyType == typeof(double?))
            {
                var conversion = Expression.Convert(property, typeof(double?));
                var lambda = Expression.Lambda<Func<T, double?>>(conversion, parameter);
                return ascending
                    ? data.OrderBy(lambda.Compile()).ToList()
                    : data.OrderByDescending(lambda.Compile()).ToList();
            }
            else if (propertyType == typeof(long) || propertyType == typeof(long?))
            {
                var conversion = Expression.Convert(property, typeof(long?));
                var lambda = Expression.Lambda<Func<T, long?>>(conversion, parameter);
                return ascending
                    ? data.OrderBy(lambda.Compile()).ToList()
                    : data.OrderByDescending(lambda.Compile()).ToList();
            }
            else
            {
                // Generic fallback cho các type khác
                var conversion = Expression.Convert(property, typeof(object));
                var lambda = Expression.Lambda<Func<T, object>>(conversion, parameter);
                return ascending
                    ? data.OrderBy(lambda.Compile()).ToList()
                    : data.OrderByDescending(lambda.Compile()).ToList();
            }
        }

        // ✅ METHOD ĐỂ LẤY THÔNG TIN PROPERTY TỪ BINDING
        public static PropertyInfo GetPropertyInfo<T>(string propertyName)
        {
            return typeof(T).GetProperty(propertyName);
        }

        // ✅ METHOD ĐỂ LẤY KIỂU DỮ LIỆU TỪ BINDING
        public static Type GetPropertyType<T>(string propertyName)
        {
            var propInfo = typeof(T).GetProperty(propertyName);
            if (propInfo == null) return null;

            var type = propInfo.PropertyType;
            return Nullable.GetUnderlyingType(type) ?? type;
        }

        // ✅ METHOD KIỂM TRA NULLABLE
        public static bool IsNullableProperty<T>(string propertyName)
        {
            var propInfo = typeof(T).GetProperty(propertyName);
            if (propInfo == null) return false;

            return Nullable.GetUnderlyingType(propInfo.PropertyType) != null;
        }
    }

    // Helpers/GridSortExtensions.cs
    public static class GridSortExtensions
    {
        public static List<T> SmartSort<T>(this List<T> data, string propertyName, bool ascending = true)
        {
            return GridSortHelper.SortByProperty(data, propertyName, ascending);
        }

        public static string GetPropertyTypeName<T>(this string propertyName)
        {
            var type = GridSortHelper.GetPropertyType<T>(propertyName);
            return type?.Name ?? "Unknown";
        }

        public static bool IsNullable<T>(this string propertyName)
        {
            return GridSortHelper.IsNullableProperty<T>(propertyName);
        }
    }
}
