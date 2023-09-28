using System.Linq.Expressions;
using Crito.Context;
using Crito.Models;
using Crito.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Crito.Services
{
    public class SubscriptionService
    {
        protected readonly ContactContext _contactContext;
        public SubscriptionService(ContactContext contactContext)
        {
            _contactContext = contactContext;
        }

        public async Task<bool> GetAsync(Expression<Func<SubscriptionEntity, bool>> predicate)
        {
            if (await _contactContext.Subscriptions.AnyAsync(predicate))
                return true;

            return false;
        }
    }
}
