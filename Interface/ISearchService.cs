using Umbraco.Cms.Core.Models.PublishedContent;

namespace Crito.Interface
{
    public interface ISearchService
    {
        IEnumerable<IPublishedContent> SearchContentNames(string query);
    }
}
