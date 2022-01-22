using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TruckManagement.Business.Implementation;
using TruckManagement.Business.Profiles;
using TruckManagement.Infra.Core.Exceptions;
using TruckManagement.Models.Entities.Enums;
using TruckManagement.Repository.Interfaces;
using TruckManagement.ViewModels;
using Xunit;

namespace TruckManagement.BusinessTests
{
    public class TruckBusinessTest
    {
        private static IMapper _mapper;

        public TruckBusinessTest()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfiles());
            });
            _mapper = mappingConfig.CreateMapper();
        }

        #region Add

        [Fact]
        public void TruckBusiness_Add_NullObjectPassed_ShoudThrowException()
        {
            var mockRepo = new Mock<ITruckRepository>();
            var service = new TruckBusiness(mockRepo.Object, _mapper);

            var ex = Assert.Throws<AggregateException>(() => service.AddAsync((TruckViewModel)null).Result);
            Assert.NotNull(ex.InnerException);
            Assert.IsType<ArgumentNullException>(ex.InnerException);
        }

        [Fact]
        public void TruckBusiness_Add_InvalidObjectPassed_ShoudThrowException()
        {
            int currentYear = DateTime.Today.Year;

            // Arrange
            var emptyTruck = new TruckViewModel();

            var mockRepo = new Mock<ITruckRepository>();
            var service = new TruckBusiness(mockRepo.Object, _mapper);

            var ex = Assert.Throws<AggregateException>(() => service.AddAsync(emptyTruck).Result);
            Assert.NotNull(ex.InnerException);
            Assert.IsType<AppITException>(ex.InnerException);
        }

        [Fact]
        public void TruckBusiness_Add_Should_Not_Save_WithoutColor()
        {
            int currentYear = DateTime.Today.Year;

            // Arrange
            var colorMissingItem = new TruckViewModel()
            {
                Model = TruckModelEnum.FH,
                ManufactureYear = currentYear,
                ModelYear = currentYear,
            };

            var mockRepo = new Mock<ITruckRepository>();
            var service = new TruckBusiness(mockRepo.Object, _mapper);

            var ex = Assert.Throws<AggregateException>(() => service.AddAsync(colorMissingItem).Result);
            Assert.NotNull(ex.InnerException);
            Assert.IsType<AppITException>(ex.InnerException);
            Assert.Equal("Color is required", ex.InnerException.Message);
        }

        [Fact]
        public void TruckBusiness_Add_Should_Not_Save_WithInvalid_ManufactoryYear()
        {
            int currentYear = DateTime.Today.Year;

            var invalidObject = new TruckViewModel()
            {
                Model = TruckModelEnum.FH,
                Color = "Black",
                ManufactureYear = currentYear - 1,
                ModelYear = currentYear,
            };

            var mockRepo = new Mock<ITruckRepository>();
            var service = new TruckBusiness(mockRepo.Object, _mapper);

            var ex = Assert.Throws<AggregateException>(() => service.AddAsync(invalidObject).Result);
            Assert.NotNull(ex.InnerException);
            Assert.IsType<AppITException>(ex.InnerException);
            Assert.Equal("Manufacture year must be the current year", ex.InnerException.Message);
        }

        [Fact]
        public void TruckBusiness_Add_Should_Not_Save_WithInvalid_ModelYear()
        {
            int currentYear = DateTime.Today.Year;

            var invalidObject = new TruckViewModel()
            {
                Model = TruckModelEnum.FH,
                Color = "White",
                ManufactureYear = currentYear,
                ModelYear = currentYear - 1,
            };

            var mockRepo = new Mock<ITruckRepository>();
            var service = new TruckBusiness(mockRepo.Object, _mapper);

            var ex = Assert.Throws<AggregateException>(() => service.AddAsync(invalidObject).Result);
            Assert.NotNull(ex.InnerException);
            Assert.IsType<AppITException>(ex.InnerException);
            Assert.Equal("Model year must be the current or next year", ex.InnerException.Message);
        }

        #endregion Add

        #region Count

        [Fact]
        public async Task TruckBusiness_Count_Should_Return_NumberOfEntities()
        {
            var mockRepo = new Mock<ITruckRepository>();

            mockRepo.Setup(rep => rep.CountAsync()).ReturnsAsync(GetTestTrucks().Count);
            var service = new TruckBusiness(mockRepo.Object, _mapper);

            // Act
            var result = await service.CountAsync();
            // Assert
            Assert.Equal(6, result);
        }

        #endregion Count

        private static List<TruckViewModel> GetTestTrucks()
        {
            int currentYear = DateTime.Today.Year;

            var trucks = new List<TruckViewModel>
            {
                new TruckViewModel
                {
                    Id = 1,
                    Model = TruckModelEnum.FH,
                    ManufactureYear = currentYear,
                    ModelYear = currentYear,
                    Color = "Silver"
                },
                new TruckViewModel
                {
                    Id = 2,
                    Model = TruckModelEnum.FH,
                    ManufactureYear = currentYear,
                    ModelYear = currentYear + 1,
                    Color = "Silver"
                },
                new TruckViewModel
                {
                    Id = 3,
                    Model = TruckModelEnum.FH,
                    ManufactureYear = currentYear,
                    ModelYear = currentYear,
                    Color = "White"
                },
                new TruckViewModel
                {
                    Id = 4,
                    Model = TruckModelEnum.FH,
                    ManufactureYear = currentYear,
                    ModelYear = currentYear,
                    Color = "White"
                },
                new TruckViewModel
                {
                    Id = 5,
                    Model = TruckModelEnum.FM,
                    ManufactureYear = currentYear,
                    ModelYear = currentYear,
                    Color = "White"
                },
                new TruckViewModel
                {
                    Id = 6,
                    Model = TruckModelEnum.FM,
                    ManufactureYear = currentYear,
                    ModelYear = currentYear,
                    Color = "White"
                }
            };

            return trucks;
        }
    }
}
