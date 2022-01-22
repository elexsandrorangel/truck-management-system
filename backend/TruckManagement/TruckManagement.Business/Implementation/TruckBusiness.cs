using AutoMapper;
using System;
using TruckManagement.Business.Base;
using TruckManagement.Business.Interfaces;
using TruckManagement.Infra.Core.Exceptions;
using TruckManagement.Models.Entities;
using TruckManagement.Repository.Interfaces;
using TruckManagement.ViewModels;

namespace TruckManagement.Business.Implementation
{
    public class TruckBusiness : BaseBusiness<Truck, TruckViewModel, ITruckRepository>, ITruckBusiness
    {
        public TruckBusiness(ITruckRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        protected override void ValidateInsert(TruckViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.Color))
            {
                throw new AppITException("Color is required");
            }

            if (model.ManufactureYear != DateTime.Today.Year)
            {
                throw new AppITException("Manufacture year must be the current year");
            }

            if (model.ModelYear != DateTime.Today.Year && model.ModelYear != DateTime.Today.AddYears(1).Year)
            {
                throw new AppITException("Model year must be the current or next year");
            }
        }
    }
}
