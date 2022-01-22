using TruckManagement.Models.Entities;
using TruckManagement.Repository.Base;
using TruckManagement.Repository.Contexts;
using TruckManagement.Repository.Interfaces;

namespace TruckManagement.Repository
{
    public class TruckRepository : BaseRepository<Truck, TrucksDbContext>, ITruckRepository
    {
        public TruckRepository(TrucksDbContext context) : base(context)
        {
        }
    }
}
