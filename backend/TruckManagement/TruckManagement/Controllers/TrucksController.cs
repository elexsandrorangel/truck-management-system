using Microsoft.AspNetCore.Mvc;
using TruckManagement.Business.Interfaces;
using TruckManagement.Models.Entities;
using TruckManagement.ViewModels;

namespace TruckManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrucksController : BaseController<ITruckBusiness, Truck, TruckViewModel>
    {
        public TrucksController(ITruckBusiness business) : base(business)
        {
        }
    }
}
