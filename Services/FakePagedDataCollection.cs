using C1.DataCollection;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

public class FakePagedDataCollection<T> : C1PagedDataCollection<T> where T : class
{
    private C1DataCollection<T>? _rootData;

    public FakePagedDataCollection(IEnumerable source) : base(source)
    {

        _rootData = (C1DataCollection<T>?)source;
    }

    public override bool CanSort(params SortDescription[] sortDescriptions)
    {
        if (_rootData != null)
            return true;
        return false;
    }

    public override async Task MoveToPageAsync(int pageIndex, CancellationToken cancellationToken = default)
    {
        await base.MoveToPageAsync(pageIndex, cancellationToken);
        var spanCollection = new C1SpanDataCollection<T>(_rootData);
        spanCollection.Slice(CurrentPage * PageSize, PageSize);
        Source = spanCollection;
    }



    public override async Task SortAsync(SortDescription[] sortDescriptions, CancellationToken cancellationToken = default)
    {

        if (sortDescriptions != null && sortDescriptions.Length > 0)
        {
            foreach (var sort in sortDescriptions)
            {
                Console.WriteLine($"  Sort by: {sort.SortPath}, Direction: {sort.Direction}");
            }
        }

        try
        {
            await _rootData.SortAsync(sortDescriptions, cancellationToken);
            var spanCollection = new C1SpanDataCollection<T>(_rootData);
            spanCollection.Slice(CurrentPage * PageSize, PageSize);
            Source = spanCollection;

            if (this.CurrentPage != 0)
            {
                Console.WriteLine($"  Moving from page {this.CurrentPage} to page 0");
                await this.MoveToPageAsync(0, cancellationToken);
            }

            await RefreshAsync();
            Console.WriteLine("✓ Base sorting completed");

        }

        catch (Exception ex)
        {
            throw;
        }
    }


}