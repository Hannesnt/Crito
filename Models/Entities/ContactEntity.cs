using System.ComponentModel.DataAnnotations;

namespace Crito.Models.Entities
{
    public class ContactEntity
    {
        public int Id { get; set; }

        public Guid ContactUmbracoKey { get; set; }
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Message { get; set; } = null!;

    }
}
