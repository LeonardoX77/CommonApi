
using Common.Core.Data.Context;
using Common.Core.Generic.Repository;
using Common.Domain.Entities;

namespace Common.Business.Repositories
{
    public class LocationRepository : BaseRepository<Location>
    {
        public LocationRepository(AppDbContext ctx) : base(ctx)
        {
        }
    }

}