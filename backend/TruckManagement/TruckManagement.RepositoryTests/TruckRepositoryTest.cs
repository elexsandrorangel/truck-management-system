using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckManagement.Models.Entities;
using TruckManagement.Models.Entities.Enums;
using TruckManagement.Repository;
using TruckManagement.Repository.Contexts;
using Xunit;

namespace TruckManagement.RepositoryTests
{
    public class TruckRepositoryTest
    {
        private async Task<TrucksDbContext> GetTestDbContextAsync()
        {
            // Create db context options specifying in memory database
            var options = new DbContextOptionsBuilder<TrucksDbContext>()
                .UseInMemoryDatabase(databaseName: $"truck_app_test_${Guid.NewGuid()}")
                .Options;

            //Use this to instantiate the db context
            var dbContext = new TrucksDbContext(options);
            dbContext.Database.EnsureCreated();

            if (await dbContext.Trucks.CountAsync() == 0)
            {
                int currentYear = DateTime.Today.Year;

                dbContext.Trucks.Add(new Truck
                {
                    Id = 1,
                    Model = TruckModelEnum.FH,
                    ManufactureYear = currentYear,
                    ModelYear = currentYear,
                    Color = "Silver",
                    CreatedAt = DateTime.UtcNow
                });
                dbContext.Trucks.Add(new Truck
                {
                    Id = 2,
                    Model = TruckModelEnum.FH,
                    ManufactureYear = currentYear,
                    ModelYear = currentYear,
                    Color = "Silver",
                    CreatedAt = DateTime.UtcNow
                });
                dbContext.Trucks.Add(new Truck
                {
                    Id = 3,
                    Model = TruckModelEnum.FH,
                    ManufactureYear = currentYear,
                    ModelYear = currentYear + 1,
                    Color = "Silver",
                    CreatedAt = DateTime.UtcNow
                });
                dbContext.Trucks.Add(new Truck
                {
                    Id = 4,
                    Model = TruckModelEnum.FH,
                    ManufactureYear = currentYear,
                    ModelYear = currentYear,
                    Color = "White",
                    CreatedAt = DateTime.UtcNow
                });
                dbContext.Trucks.Add(new Truck
                {
                    Id = 5,
                    Model = TruckModelEnum.FH,
                    ManufactureYear = currentYear,
                    ModelYear = currentYear,
                    Color = "White",
                    CreatedAt = DateTime.UtcNow
                });
                dbContext.Trucks.Add(new Truck
                {
                    Id = 6,
                    Model = TruckModelEnum.FM,
                    ManufactureYear = currentYear,
                    ModelYear = currentYear,
                    Color = "White",
                    CreatedAt = DateTime.UtcNow
                });
                dbContext.Trucks.Add(new Truck
                {
                    Id = 7,
                    Model = TruckModelEnum.FM,
                    ManufactureYear = currentYear,
                    ModelYear = currentYear,
                    Color = "White"
                });
                await dbContext.SaveChangesAsync();
            }
            return dbContext;
        }

        #region Count

        [Fact]
        public async Task TruckRepository_Count_Should_Return_NumberOfEntities()
        {
            var dbContext = await GetTestDbContextAsync();

            var truckRepository = new TruckRepository(dbContext);

            var trucks = await truckRepository.CountAsync();

            Assert.Equal(7, trucks);
        }

        [Fact]
        public async Task TruckRepository_Count_Expression_Should_Return_NumberOfEntities()
        {
            var dbContext = await GetTestDbContextAsync();

            var truckRepository = new TruckRepository(dbContext);

            var trucks = await truckRepository.CountAsync(x => x.Model == TruckModelEnum.FM);

            Assert.Equal(2, trucks);
        }

        #endregion Count

        #region Add

        [Fact]
        public async Task TruckRepository_Save_NullObject_ShouldBeThrowArgumentNull_Exception()
        {
            var dbContext = await GetTestDbContextAsync();
            var truckRepository = new TruckRepository(dbContext);
            var ex = Assert.Throws<AggregateException>(() => truckRepository.AddAsync((Truck)null).Result);

            Assert.NotNull(ex.InnerException);
            Assert.IsType<ArgumentNullException>(ex.InnerException);
        }

        [Fact]
        public async Task TruckRepository_Save_Should_ReturnsObject()
        {
            var dbContext = await GetTestDbContextAsync();
            var truckRepository = new TruckRepository(dbContext);
            var data = await truckRepository.AddAsync(new Truck
            {
                Color = "Blue",
                ModelYear = 2022,
                ManufactureYear = 2022,
                Id = 10,
                CreatedAt = DateTime.UtcNow,
                Model = TruckModelEnum.FH
            });

            Assert.NotNull(data);

            var trucksCount = await truckRepository.CountAsync();
            Assert.Equal(8, trucksCount);
        }


        [Fact]
        public async Task TruckRepository_Save_List()
        {
            var dbContext = await GetTestDbContextAsync();
            var truckRepository = new TruckRepository(dbContext);
            var data = await truckRepository.AddAsync(new List<Truck> {
                new Truck
                {
                    Color = "Blue",
                    ModelYear = 2022,
                    ManufactureYear = 2022,
                    Id = 10,
                    CreatedAt = DateTime.UtcNow,
                    Model = TruckModelEnum.FH
                }, new Truck
                {
                    Color = "Blue",
                    ModelYear = 2022,
                    ManufactureYear = 2022,
                    Id = 11,
                    CreatedAt = DateTime.UtcNow,
                    Model = TruckModelEnum.FM
                }
            });

            Assert.NotNull(data);

            // Assert this request result
            Assert.NotEmpty(data);
            Assert.Equal(2, data.Count());

            // Database counting check
            var trucksCount = await truckRepository.CountAsync();
            Assert.Equal(9, trucksCount);
        }

        #endregion Add

        #region Delete

        [Fact]
        public async Task TruckRepository_Delete_NullObject_ShouldBeThrowInvalidOperationException()
        {
            var dbContext = await GetTestDbContextAsync();
            var truckRepository = new TruckRepository(dbContext);
            var ex = Assert.Throws<AggregateException>(() => truckRepository.DeleteAsync((Truck)null).Wait());

            Assert.NotNull(ex.InnerException);
            Assert.IsType<InvalidOperationException>(ex.InnerException);
            Assert.Equal("Entity does not exists", ex.InnerException.Message);
        }


        [Fact]
        public async Task TruckRepository_Delete_NonExisting_ShouldBeThrowInvalidOperationException()
        {
            var dbContext = await GetTestDbContextAsync();
            var truckRepository = new TruckRepository(dbContext);

            var data = await truckRepository.GetAsync(3000, false);
            var ex = Assert.Throws<AggregateException>(() => truckRepository.DeleteAsync(data).Wait());

            Assert.NotNull(ex.InnerException);
            Assert.IsType<InvalidOperationException>(ex.InnerException);
            Assert.Equal("Entity does not exists", ex.InnerException.Message);
        }

        [Fact]
        public async Task TruckRepository_Delete_InvalidId_ShouldBeThrowArgumentNull_Exception()
        {
            var dbContext = await GetTestDbContextAsync();
            var truckRepository = new TruckRepository(dbContext);

            var ex = Assert.Throws<AggregateException>(() => truckRepository.DeleteAsync(3000).Wait());

            Assert.NotNull(ex.InnerException);
            Assert.IsType<InvalidOperationException>(ex.InnerException);
            Assert.Equal("Entity does not exists", ex.InnerException.Message);
        }
        #endregion Delete

        #region Exists

        [Fact]
        public async Task TruckRepository_Exists_Returns_Nothing_FutureDate()
        {
            var dbContext = await GetTestDbContextAsync();
            var truckRepository = new TruckRepository(dbContext);
            var truck = truckRepository.Exists(x => x.CreatedAt == DateTime.Now);

            //Assert  
            Assert.False(truck);
        }

        [Fact]
        public async Task TruckRepository_Exists_Returns_Nothing_PreviousCreate()
        {
            var dbContext = await GetTestDbContextAsync();
            var truckRepository = new TruckRepository(dbContext);
            var truck = truckRepository.Exists(x => x.Model == TruckModelEnum.FM);

            //Assert  
            Assert.True(truck);
        }

        #endregion Exists

        #region Add

        [Fact]
        public async Task TruckRepository_Update_NullObject_ShouldBeThrowArgumentNull_Exception()
        {
            var dbContext = await GetTestDbContextAsync();
            var truckRepository = new TruckRepository(dbContext);
            var ex = Assert.Throws<AggregateException>(() => truckRepository.UpdateAsync((Truck)null).Result);

            Assert.NotNull(ex.InnerException);
            Assert.IsType<InvalidOperationException>(ex.InnerException);
        }

        #endregion Add

        #region Get

        [Fact]
        public async Task TruckRepository_GetByIdAsync_Returns_Truck()
        {
            var dbContext = await GetTestDbContextAsync();
            var truckRepository = new TruckRepository(dbContext);
            var truck = truckRepository.GetAsync(1, false).Result;

            //Assert  
            Assert.NotNull(truck);
            Assert.IsAssignableFrom<Truck>(truck);
        }

        [Fact]
        public async Task TruckRepository_GetSingleOrDefault_Returns_Truck()
        {
            var dbContext = await GetTestDbContextAsync();
            var truckRepository = new TruckRepository(dbContext);
            var truck = await truckRepository.GetSingleOrDefaultAsync(x => x.Id == 3, false);

            //Assert  
            Assert.NotNull(truck);
            Assert.IsAssignableFrom<Truck>(truck);
            Assert.Equal(3, truck.Id);
        }

        [Fact]
        public async Task TruckRepository_GetAll_Returns_Trucks()
        {
            var dbContext = await GetTestDbContextAsync();

            var truckRepository = new TruckRepository(dbContext);

            var trucks = await truckRepository.GetAsync();

            Assert.NotNull(trucks);
            Assert.IsAssignableFrom<IEnumerable<Truck>>(trucks);
            Assert.NotEmpty(trucks);
            Assert.Equal(7, trucks.Count());
        }

        [Fact]
        public async Task TruckRepository_GetAll_Invalid_Page_Returns_Nothing()
        {
            var dbContext = await GetTestDbContextAsync();

            var truckRepository = new TruckRepository(dbContext);

            var trucks = await truckRepository.GetAsync(page: 100, qty: 20, track: false);

            Assert.NotNull(trucks);
            Assert.IsAssignableFrom<IEnumerable<Truck>>(trucks);
            Assert.Empty(trucks);
        }

        [Fact]
        public async Task TruckRepository_GetFiltered_Returns_Trucks_From_ThisYear()
        {
            var dbContext = await GetTestDbContextAsync();

            var truckRepository = new TruckRepository(dbContext);

            var trucks = await truckRepository.GetAsync(x => x.ManufactureYear == 2022);

            Assert.NotNull(trucks);
            Assert.IsAssignableFrom<IEnumerable<Truck>>(trucks);
            Assert.NotEmpty(trucks);
            Assert.Equal(7, trucks.Count());
        }

        [Fact]
        public async Task TruckRepository_GetFiltered_Returns_FM_Only()
        {
            var dbContext = await GetTestDbContextAsync();

            var truckRepository = new TruckRepository(dbContext);

            var trucks = await truckRepository.GetAsync(x => x.Model == TruckModelEnum.FM);

            Assert.NotNull(trucks);
            Assert.IsAssignableFrom<IEnumerable<Truck>>(trucks);
            Assert.NotEmpty(trucks);
            Assert.Equal(2, trucks.Count());
        }
        #endregion Get
    }
}
