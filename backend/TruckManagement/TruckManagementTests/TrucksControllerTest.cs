using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckManagement.Business.Interfaces;
using TruckManagement.Controllers;
using TruckManagement.ViewModels;
using Xunit;

namespace TruckManagementTests
{
    public class TrucksControllerTest
    {
        #region Get

        [Fact]
        public async Task TrucksController_Get_All_NoResult()
        {
            var mockBusiness = new Mock<ITruckBusiness>();

            mockBusiness.Setup(x => x.GetAsync(1, int.MaxValue))
                .Returns(Task.FromResult(new List<TruckViewModel>().AsEnumerable()));

            var controller = new TrucksController(mockBusiness.Object);

            var result = await controller.Get(page: 1, qty: int.MaxValue);

            Assert.IsAssignableFrom<OkObjectResult>(result);
            var responseObj = (result as OkObjectResult).Value as IEnumerable<TruckViewModel>;
            Assert.Empty(responseObj);
        }

        [Fact]
        public async Task TrucksController_Get_All_NoResult_NullList()
        {
            var mockBusiness = new Mock<ITruckBusiness>();

            mockBusiness.Setup(x => x.GetAsync(1, int.MaxValue))
                .Returns(Task.FromResult((IEnumerable<TruckViewModel>)null));

            var controller = new TrucksController(mockBusiness.Object);

            var result = await controller.Get(page: 100, qty: int.MaxValue);

            Assert.IsAssignableFrom<OkObjectResult>(result);
            var responseObj = (result as OkObjectResult).Value as IEnumerable<TruckViewModel>;
            Assert.Empty(responseObj);
        }

        [Fact]
        public async Task TrucksController_Get_All_Trucks()
        {
            var mockBusiness = new Mock<ITruckBusiness>();

            var fakeList = new List<TruckViewModel>()
            {
                new TruckViewModel(),
                new TruckViewModel()
            };

            mockBusiness.Setup(x => x.GetAsync(1, int.MaxValue))
                .Returns(Task.FromResult(fakeList.AsEnumerable()));

            var controller = new TrucksController(mockBusiness.Object);

            var result = await controller.Get(page: 1, qty: int.MaxValue);

            Assert.IsAssignableFrom<OkObjectResult>(result);
            var responseObj = (result as OkObjectResult).Value as IEnumerable<TruckViewModel>;
            Assert.NotEmpty(responseObj);
            Assert.Equal(2, responseObj.Count());
        }


        [Fact]
        public async Task TrucksController_Get_ById_ReturnsNotFound()
        {
            var mockBusiness = new Mock<ITruckBusiness>();

            mockBusiness.Setup(x => x.GetAsync(It.IsAny<int>(), false))
                .Returns(Task.FromResult((TruckViewModel)null));

            var controller = new TrucksController(mockBusiness.Object);

            var result = await controller.Get(id: 1);

            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task TrucksController_Get_ById_ReturnsTruck()
        {
            var mockBusiness = new Mock<ITruckBusiness>();

            mockBusiness.Setup(x => x.GetAsync(It.IsAny<int>(), false))
                .Returns(Task.FromResult(new TruckViewModel()));

            var controller = new TrucksController(mockBusiness.Object);

            var result = await controller.Get(id: 1);

            Assert.IsAssignableFrom<OkObjectResult>(result);
            var responseObj = (result as OkObjectResult).Value as TruckViewModel;
            Assert.NotNull(responseObj);
        }

        #endregion Get

        #region Create

        [Fact]
        public void TrucksController_Create_NullObjectPassed_ShoudThrowException()
        {
            var mockBusiness = new Mock<ITruckBusiness>();

            mockBusiness.Setup(x => x.AddAsync((TruckViewModel)null)).Throws<ArgumentNullException>();

            var controller = new TrucksController(mockBusiness.Object);
        
            var ex = Assert.Throws<AggregateException>(() => controller.CreateAsync(null).Result);
            Assert.NotNull(ex.InnerException);
            Assert.IsType<ArgumentNullException>(ex.InnerException);
        }

        [Fact]
        public async Task TrucksController_Create_InvalidObjectPassed_Shoud_Return_BadRequest()
        {
            int currentYear = DateTime.Today.Year;

            // Arrange
            var emptyTruck = new TruckViewModel();

            var mockBusiness = new Mock<ITruckBusiness>();

            mockBusiness.Setup(x => x.AddAsync(new TruckViewModel())).Throws<Exception>();
            var controller = new TrucksController(mockBusiness.Object);

            var result = await controller.CreateAsync(emptyTruck);
            Assert.IsAssignableFrom<BadRequestResult>(result);
        }

        #endregion Create
    }
}
