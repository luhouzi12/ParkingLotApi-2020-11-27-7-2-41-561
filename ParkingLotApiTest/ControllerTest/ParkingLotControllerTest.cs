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
    }
}