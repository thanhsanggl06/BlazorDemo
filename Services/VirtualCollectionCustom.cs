using C1.DataCollection;
using System.Text.Json;

namespace BlazorSolution.Services
{
    public class VirtualCollectionCustom<T> : C1VirtualDataCollection<T> where T : class
    {
        public C1DataCollection<T>? DataSource { get; set; }

        public VirtualCollectionCustom(C1DataCollection<T> data)
        {
            DataSource = data;
        }

        public override bool CanSort(params SortDescription[] sortDescriptions)
        {
            return true;
        }

        protected override async Task<Tuple<int, IReadOnlyList<T>>> GetPageAsync(
            int pageIndex,
            int startingIndex,
            int count,
            IReadOnlyList<SortDescription> sortDescriptions = null,
            FilterExpression filterExpression = null,
            CancellationToken cancellationToken = default)
        {
            if (DataSource == null)
                return new Tuple<int, IReadOnlyList<T>>(0, new List<T>());

            if (sortDescriptions != null && sortDescriptions.Count > 0)
            {
                var sortArray = sortDescriptions.ToArray();
                await DataSource.SortAsync(sortArray);
            }

            var spanCollection = new C1SpanDataCollection<T>(DataSource);
            var result = spanCollection.Skip(startingIndex).Take(count).ToList();

            return new Tuple<int, IReadOnlyList<T>>(DataSource.Count, result);
        }
    }
}