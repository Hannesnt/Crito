using Crito.Context;
using Crito.Models;
using Crito.Services;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace Crito.Controllers
{
    public class SubscriptionController : SurfaceController
    {
        protected readonly SubscriptionService _subscriptionService;
        protected readonly ContactContext _contactContext;
        public SubscriptionController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider, ContactContext contactContext, SubscriptionService subscriptionService) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _contactContext = contactContext;
            _subscriptionService = subscriptionService;
        }

        public async Task<IActionResult> Index(SubscriptionForm subscriptionForm)
        {
            if (ModelState.IsValid)
            {
                if(await _subscriptionService.GetAsync(x => x.Email == subscriptionForm.Email))
                {
                    return CurrentUmbracoPage();
                }
                else
                {
                    await _contactContext.Subscriptions.AddAsync(subscriptionForm);
                    await _contactContext.SaveChangesAsync();
                }
            }

            return CurrentUmbracoPage();
            
        }
    }
}
