using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ParkingLotApi;
using ParkingLotApi.Dtos;
using Xunit;

namespace ParkingLotApiTest.ControllerTest
{
    [Collection("IntegrationTest")]
    public class ParkingLotControllerTest : TestBase
    {
        public ParkingLotControllerTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Should_Add_Parking_Lot()
        {
            // given
            var client = GetClient();

            var parkingLotDto = new ParkingLotDto()
            {
                Name = "parkinglot1",
                Capacity = 5,
                Location = "Beijing",
            };

            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            // when
            await client.PostAsync("/parkinglots", content);

            var allCompaniesResponse = await client.GetAsync("/parkinglots");
            var body = await allCompaniesResponse.Content.ReadAsStringAsync();

            var returnParkingLots = JsonConvert.DeserializeObject<List<ParkingLotDto>>(body);
            // then
            var expectedParkingLotDtoList = new List<ParkingLotDto>
            {
                parkingLotDto
            };
            Assert.Equal(expectedParkingLotDtoList, returnParkingLots);
        }

        [Fact]
        public async Task Should_Delete_Parking_Lot()
        {
            // given
            var client = GetClient();

            var parkingLotDto = new ParkingLotDto()
            {
                Name = "parkinglot1",
                Capacity = 5,
                Location = "Beijing",
            };

            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            await client.PostAsync("/parkinglots", content);

            // when
            await client.DeleteAsync("/parkinglots/parkinglot1");
            var allCompaniesResponse = await client.GetAsync("/parkinglots");
            var body = await allCompaniesResponse.Content.ReadAsStringAsync();

            var returnParkingLots = JsonConvert.DeserializeObject<List<ParkingLotDto>>(body);
            // then
            var expectedParkingLotDtoList = new List<ParkingLotDto>
            {
            };
            Assert.Equal(expectedParkingLotDtoList, returnParkingLots);
        }

        [Fact]
        public async Task Should_Get_Parking_Lots_By_Page()
        {
            // given
            var client = GetClient();
            for (int i = 1; i < 17; i++)
            {
                var parkingLotDto = new ParkingLotDto()
                {
                    Name = "parkinglot" + i,
                    Capacity = 5,
                    Location = "Beijing",
                };
                var httpContent = JsonConvert.SerializeObject(parkingLotDto);
                StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
                await client.PostAsync("/parkinglots", content);
            }

            // when
            var allCompaniesResponse = await client.GetAsync("/parkinglots/page/2");
            var body = await allCompaniesResponse.Content.ReadAsStringAsync();

            var returnParkingLots = JsonConvert.DeserializeObject<List<ParkingLotDto>>(body);
            // then
            var expectedParkingLotDtoList = new List<ParkingLotDto>
            {
                new ParkingLotDto()
                {
                    Name = "parkinglot16",
                    Capacity = 5,
                    Location = "Beijing",
                },
            };
            Assert.Equal(expectedParkingLotDtoList, returnParkingLots);
        }

        [Fact]
        public async Task Should_Update_Parking_Lot_Capacity()
        {
            // given
            var client = GetClient();

            var parkingLotDto = new ParkingLotDto()
            {
                Name = "parkinglot1",
                Capacity = 5,
                Location = "Beijing",
            };

            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            await client.PostAsync("/parkinglots", content);

            var updatingParkingLotDto = new UpdatingCapacityDto()
            {
                Name = "parkinglot1",
                Capacity = 10,
            };

            var updatinghttpContent = JsonConvert.SerializeObject(updatingParkingLotDto);
            StringContent updatingcontent = new StringContent(updatinghttpContent, Encoding.UTF8, MediaTypeNames.Application.Json);

            // when
            await client.PatchAsync("/parkinglots/parkinglot1", updatingcontent);
            var parkingLotsResponse = await client.GetAsync("/parkinglots/parkinglot1");
            var body = await parkingLotsResponse.Content.ReadAsStringAsync();

            var returnParkingLot = JsonConvert.DeserializeObject<ParkingLotDto>(body);
            // then

            Assert.Equal(10, returnParkingLot.Capacity);
        }
    }
}