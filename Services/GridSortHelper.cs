using C1.DataCollection;
using System.Linq.Expressions;
using System.Reflection;

namespace BlazorSolution.Services
{
    public enum GridSortMode
    {
        AllData,        // Sort toàn bộ dữ liệu
        CurrentPageOnly // Sort chỉ page hiện tại
    }
    public static class GridSortHelper
    {
        public static (List<T> sortedData, C1PagedDataCollection<T> pagedCollection, int currentPageIndex) SortWithMode<T>(
            List<T> allData,
            C1PagedDataCollection<T> currentPagedCollection,
            string propertyName,
            bool ascending,
            GridSortMode sortMode,
            int pageSize = 10)
            where T : class
        {
            if (string.IsNullOrEmpty(propertyName) || allData == null || !allData.Any())
                return (allData, currentPagedCollection, currentPagedCollection.CurrentPage);

            try
            {
                var propertyInfo = typeof(T).GetProperty(propertyName);
                if (propertyInfo == null)
                {
                    Console.WriteLine($"Property '{propertyName}' not found");
                    return (allData, currentPagedCollection, currentPagedCollection.CurrentPage);
                }

                var propertyType = propertyInfo.PropertyType;

                if (sortMode == GridSortMode.AllData)
                {
                    // ✅ SORT ALL DATA - Reset về page 1
                    var sortedData = SortByPropertyType(allData, propertyInfo, propertyType, ascending);
                    var dataCollection = new C1DataCollection<T>(sortedData);
                    var pagedCollection = new C1PagedDataCollection<T>(dataCollection);
                    pagedCollection.PageSize = pageSize;

                    Console.WriteLine($"✓ Sorted ALL data by '{propertyName}' ({propertyType.Name})");
                    return (sortedData, pagedCollection, 0);
                }
                else
                {
                    // ✅ SORT CURRENT PAGE ONLY - Giữ nguyên tất cả pages
                    var currentPageIndex = currentPagedCollection.CurrentPage;

                    // Lấy items của page hiện tại
                    var currentPageData = currentPagedCollection.ToList();

                    // Sort items của page hiện tại
                    var sortedPageData = SortByPropertyType(currentPageData, propertyInfo, propertyType, ascending);

                    // ✅ TẠO BẢN COPY CỦA ALL DATA
                    var updatedAllData = new List<T>(allData);

                    // ✅ THAY THẾ ITEMS CỦA PAGE HIỆN TẠI TRONG ALL DATA
                    var startIndex = currentPageIndex * pageSize;
                    for (int i = 0; i < sortedPageData.Count; i++)
                    {
                        if (startIndex + i < updatedAllData.Count)
                        {
                            updatedAllData[startIndex + i] = sortedPageData[i];
                        }
                    }

                    // ✅ TẠO LẠI PAGED COLLECTION VỚI ALL DATA ĐÃ UPDATE
                    var dataCollection = new C1DataCollection<T>(updatedAllData);
                    var pagedCollection = new C1PagedDataCollection<T>(dataCollection);
                    pagedCollection.PageSize = pageSize;

                    Console.WriteLine($"✓ Sorted CURRENT PAGE ONLY by '{propertyName}' ({propertyType.Name})");
                    return (updatedAllData, pagedCollection, currentPageIndex);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Sort error: {ex.Message}");
                return (allData, currentPagedCollection, currentPagedCollection.CurrentPage);
            }
        }

        private static List<T> SortByPropertyType<T>(
            List<T> data,
            PropertyInfo propertyInfo,
            Type propertyType,
            bool ascending)
            where T : class
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyInfo);

            if (propertyType == typeof(string))
            {
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
                var conversion = Expression.Convert(property, typeof(object));
                var lambda = Expression.Lambda<Func<T, object>>(conversion, parameter);
                return ascending
                    ? data.OrderBy(lambda.Compile()).ToList()
                    : data.OrderByDescending(lambda.Compile()).ToList();
            }
        }

        public static Type GetPropertyType<T>(string propertyName) where T : class
        {
            var propInfo = typeof(T).GetProperty(propertyName);
            if (propInfo == null) return null;

            var type = propInfo.PropertyType;
            return Nullable.GetUnderlyingType(type) ?? type;
        }
    }

    // Helpers/GridSortExtensions.cs
    //public static class GridSortExtensions
    //{
    //    public static List<T> SmartSort<T>(this List<T> data, string propertyName, bool ascending = true)
    //    {
    //        return GridSortHelper.SortByProperty(data, propertyName, ascending);
    //    }

    //    public static string GetPropertyTypeName<T>(this string propertyName)
    //    {
    //        var type = GridSortHelper.GetPropertyType<T>(propertyName);
    //        return type?.Name ?? "Unknown";
    //    }

    //    public static bool IsNullable<T>(this string propertyName)
    //    {
    //        return GridSortHelper.IsNullableProperty<T>(propertyName);
    //    }
    //}
}
