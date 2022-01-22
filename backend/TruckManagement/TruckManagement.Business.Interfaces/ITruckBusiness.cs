using TruckManagement.Business.Interfaces.Base;
using TruckManagement.Models.Entities;
using TruckManagement.ViewModels;

namespace TruckManagement.Business.Interfaces
{
    public interface ITruckBusiness : IBaseBusiness<Truck, TruckViewModel>
    {
    }
}
