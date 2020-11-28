using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ParkingLotApi;
using ParkingLotApi.Dtos;
using ParkingLotApi.Repository;
using ParkingLotApi.Services;
using Xunit;

namespace ParkingLotApiTest.ServiceTest
{
    [Collection("IntegrationTest")]
    public class ParkingLotServicesTest : TestBase
    {
        public ParkingLotServicesTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Should_Get_Parking_Lots_By_Range()
        {
            // given
            var scope = Factory.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;

            ParkingLotContext context = scopedServices.GetRequiredService<ParkingLotContext>();

            // when
            int startindex = 1;
            int endindex = 2;
            ParkingLotDto parkingLotDto1 = new ParkingLotDto()
            {
                Name = "parkinglot1",
                Capacity = 4,
                Location = "Beijing",
            };
            ParkingLotDto parkingLotDto2 = new ParkingLotDto()
            {
                Name = "parkinglot2",
                Capacity = 3,
                Location = "Beijing",
            };
            ParkingLotService parkingLotService = new ParkingLotService(context);
            await parkingLotService.AddParkingLot(parkingLotDto1);
            await parkingLotService.AddParkingLot(parkingLotDto2);
            var parkingLotList = await parkingLotService.GetParkingLotsByRange(startindex, endindex);
            var expectedList = new List<ParkingLotDto>()
            {
                parkingLotDto2
            };
            Assert.Equal(expectedList, parkingLotList);
        }

        [Fact]
        public async Task Should_Get_Parking_Lots_By_Name()
        {
            // given
            var scope = Factory.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;

            ParkingLotContext context = scopedServices.GetRequiredService<ParkingLotContext>();

            // when
            int startindex = 1;
            int endindex = 2;
            ParkingLotDto parkingLotDto1 = new ParkingLotDto()
            {
                Name = "parkinglot1",
                Capacity = 4,
                Location = "Beijing",
            };
            ParkingLotDto parkingLotDto2 = new ParkingLotDto()
            {
                Name = "parkinglot2",
                Capacity = 3,
                Location = "Beijing",
            };
            ParkingLotService parkingLotService = new ParkingLotService(context);
            await parkingLotService.AddParkingLot(parkingLotDto1);
            await parkingLotService.AddParkingLot(parkingLotDto2);
            var parkingLot = await parkingLotService.GetByName("parkinglot2");

            Assert.Equal(parkingLotDto2, parkingLot);
        }
    }
}
