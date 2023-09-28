using System.ComponentModel.DataAnnotations;
using Crito.Models.Entities;

namespace Crito.Models;

public class ContactForm
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    public string Message { get; set; } = null!;
    public string? RedirectUrl { get; set; } = "/";
    public string? ReDirectUrl { get; internal set; }

    public static implicit operator ContactEntity(ContactForm contactform)
    {
        return new ContactEntity
        {
            Name = contactform.Name,
            Email = contactform.Email,
            Message = contactform.Message,
            ContactUmbracoKey = Guid.NewGuid(),
        };
    }
}
