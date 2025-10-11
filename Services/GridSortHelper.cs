using C1.DataCollection;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        public static C1PagedDataCollection<T>? SortAllData<T>(
            List<T> allData,
            string propertyName,
            bool ascending)
            where T : class
        {
            if (string.IsNullOrEmpty(propertyName) || allData == null || !allData.Any())
                return null;

            var propertyInfo = typeof(T).GetProperty(propertyName);

            if (propertyInfo == null)
            {
                Console.WriteLine($"Property '{propertyName}' not found");
                return null;
            }

            var propertyType = propertyInfo.PropertyType;

            var sortedData = SortByPropertyType(allData, propertyInfo, propertyType, ascending);
            var dataCollection = new C1DataCollection<T>(sortedData);
            var pagedCollection = new C1PagedDataCollection<T>(dataCollection);

            Console.WriteLine($" Sorted ALL data by '{propertyName}' ({propertyType.Name})");
            return pagedCollection;
        }
        public static C1PagedDataCollection<T>? SortCurrentPage<T>(
            List<T> allData,
            C1PagedDataCollection<T> currentPagedCollection,
            string propertyName,
            bool ascending
           )
            where T : class
        {

            if (string.IsNullOrEmpty(propertyName) || currentPagedCollection == null || !currentPagedCollection.Any())
                return null;

            var propertyInfo = typeof(T).GetProperty(propertyName);
            if (propertyInfo == null)
            {
                Console.WriteLine($"Property '{propertyName}' not found");
                return null;
            }

            var propertyType = propertyInfo.PropertyType;

            // Get current page index
            var currentPageIndex = currentPagedCollection.CurrentPage;

            // Get items of current page
            var currentPageData = currentPagedCollection.ToList();

            //get pagesize
            var pageSize = currentPagedCollection.PageSize;

            // Sort current page
            var sortedPageData = SortByPropertyType(currentPageData, propertyInfo, propertyType, ascending);

            // Copy All data
            var updatedAllData = new List<T>(allData);

            // update items only currentpage
            var startIndex = currentPageIndex * pageSize;
            for (int i = 0; i < sortedPageData.Count; i++)
            {
                if (startIndex + i < updatedAllData.Count)
                {
                    updatedAllData[startIndex + i] = sortedPageData[i];
                }
            }

            // recreate paged data with update
            var dataCollection = new C1DataCollection<T>(updatedAllData);
            var pagedCollection = new C1PagedDataCollection<T>(dataCollection);
            pagedCollection.PageSize = pageSize;

            Console.WriteLine($"Sorted CURRENT PAGE ONLY by '{propertyName}' ({propertyType.Name})");
            return pagedCollection;
        }
    }
}
