using Crito.Context;
using Crito.Models;
using Crito.Services;
using MailKit;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace Crito.Controllers;

public class ContactsController : SurfaceController
{
    protected readonly ContactContext _contactContext;

    public ContactsController(ContactContext contactContext, IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
    {
        _contactContext = contactContext;
    }

    [HttpPost]
    public async Task<IActionResult> Index(ContactForm contactForm)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Form was not submitted.";
            return LocalRedirect("/contact");
        }
        else
        {
            TempData["SuccessMessage"] = "Form submitted successfully";
            using var mail = new Services.MailService("no-reply@crito.com", "smtp.crito.com", 587, "contactform@crito.com", "password");

            await mail.SendAsync(contactForm.Email, "Your request was recieved.", "Hi your Rquest was recieved and we will be in contact with you as soon as possible.").ConfigureAwait(false);

            await mail.SendAsync("Hannes@hotmail.com", $"{contactForm.Name} sent a contact request", contactForm.Message).ConfigureAwait(false);
            await _contactContext.Contacts.AddAsync(contactForm);
            await _contactContext.SaveChangesAsync();

            return LocalRedirect("/contact");
        }




        
    }
}
