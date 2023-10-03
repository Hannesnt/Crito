using Umbraco.Cms.Core.Models.PublishedContent;

namespace Crito.ViewModels
{
    public class SearchViewModel : PublishedContentWrapped
    {
        public SearchViewModel(IPublishedContent content, IPublishedValueFallback publishedValueFallback) : base(content, publishedValueFallback)
        {
        }
        public IEnumerable<IPublishedContent> SearchResults { get; set; } = Enumerable.Empty<IPublishedContent>();
        public bool HasSearched { get; set; }

        public required string SearchedValue { get; set; }
    }
}
