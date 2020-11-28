using ParkingLotApi;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using ParkingLotApi.Dtos;
using Xunit;
using Newtonsoft.Json;

namespace ParkingLotApiTest.ControllerTest
{
    public class OrderControllerTest : TestBase
    {
        public OrderControllerTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Should_Add_Order()
        {
            // given
            var client = GetClient();
            var orderCreateDto = new OrderCreateDto()
            {
                OrderNumber = Guid.NewGuid().ToString(),
                ParkingLotName = "parkinglot1",
                PlateNumber = "abc123",
                Status = OrderCreateDto.OrderStatus.Open,
            };
            var httpContent = JsonConvert.SerializeObject(orderCreateDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            // when
            await client.PostAsync("/orders", content);
            var allOrdersResponse = await client.GetAsync("/orders");
            var body = await allOrdersResponse.Content.ReadAsStringAsync();

            var returnOrders = JsonConvert.DeserializeObject<List<OrderFullDto>>(body);
            // then
            Assert.Equal(orderCreateDto.OrderNumber, returnOrders[0].OrderNumber);
        }
    }
}
