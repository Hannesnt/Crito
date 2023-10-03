using Crito.Interface;
using Crito.Services;
using Crito.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace Crito.Controllers
{
    public class NewsController : RenderController
    {
        private readonly IPublishedValueFallback _publishedValueFallback;
        private readonly ISearchService _searchService;
        public NewsController(
                ILogger<RenderController> logger,
                ICompositeViewEngine compositeViewEngine,
                IUmbracoContextAccessor umbracoContextAccessor,
                IPublishedValueFallback publishedValueFallback,
                ISearchService searchService)
                : base(logger,
                    compositeViewEngine,
                    umbracoContextAccessor)
        {
            _publishedValueFallback = publishedValueFallback;
            _searchService = searchService;
        }

        public override IActionResult Index()
        {

            string queryString = HttpContext.Request.Query["query"];

            SearchViewModel viewModel = new(CurrentPage!, _publishedValueFallback)
            {
                SearchResults = _searchService.SearchContentNames(queryString),
                HasSearched = !string.IsNullOrEmpty(queryString),
                SearchedValue = queryString
            };
            if (queryString != null)
                return View("Search", viewModel);


            return CurrentTemplate(CurrentPage);
        }
        public IActionResult Search()
        {
           
            return View();
        }
    }
}
