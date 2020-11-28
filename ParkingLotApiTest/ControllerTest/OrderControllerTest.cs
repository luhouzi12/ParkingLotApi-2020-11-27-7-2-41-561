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
            var parkingLotDto = new ParkingLotDto()
            {
                Name = "parkinglot1",
                Capacity = 5,
                Location = "Beijing",
            };

            var parkingLotContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent parkingLotcontent = new StringContent(parkingLotContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            // when
            await client.PostAsync("/parkinglots", parkingLotcontent);
            var orderCreateDto = new OrderCreateDto()
            {
                OrderNumber = Guid.NewGuid().ToString(),
                ParkingLotName = "parkinglot1",
                PlateNumber = "abc123",
                Status = OrderCreateDto.OrderStatus.Open,
            };
            var httpContent = JsonConvert.SerializeObject(orderCreateDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);

            await client.PostAsync("/orders", content);
            var allOrdersResponse = await client.GetAsync("/orders");
            var body = await allOrdersResponse.Content.ReadAsStringAsync();

            var returnOrders = JsonConvert.DeserializeObject<List<OrderFullDto>>(body);
            // then
            Assert.Equal(orderCreateDto.OrderNumber, returnOrders[0].OrderNumber);
        }

        [Fact]
        public async Task Should_Get_Order_By_Number()
        {
            // given
            var client = GetClient();
            var parkingLotDto = new ParkingLotDto()
            {
                Name = "parkinglot1",
                Capacity = 5,
                Location = "Beijing",
            };

            var parkingLotContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent parkingLotcontent = new StringContent(parkingLotContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            // when
            await client.PostAsync("/parkinglots", parkingLotcontent);
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

            var orderResponse = await client.GetAsync($"/orders/{orderCreateDto.OrderNumber}");
            var body = await orderResponse.Content.ReadAsStringAsync();

            var returnOrder = JsonConvert.DeserializeObject<OrderFullDto>(body);
            // then
            Assert.Equal(orderCreateDto.OrderNumber, returnOrder.OrderNumber);
        }

        [Fact]
        public async Task Should_Patch_Order_When_Car_Leaves()
        {
            // given
            var client = GetClient();
            var parkingLotDto = new ParkingLotDto()
            {
                Name = "parkinglot1",
                Capacity = 5,
                Location = "Beijing",
            };

            var parkingLotContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent parkingLotcontent = new StringContent(parkingLotContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            // when
            await client.PostAsync("/parkinglots", parkingLotcontent);
            var orderCreateDto = new OrderCreateDto()
            {
                OrderNumber = Guid.NewGuid().ToString(),
                ParkingLotName = "parkinglot1",
                PlateNumber = "abc123",
                Status = OrderCreateDto.OrderStatus.Open,
            };
            var httpContent = JsonConvert.SerializeObject(orderCreateDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            var orderPatchDto = new OrderPatchDto()
            {
                OrderNumber = orderCreateDto.OrderNumber,
            };
            var patchContent = JsonConvert.SerializeObject(orderPatchDto);
            StringContent content2 = new StringContent(patchContent, Encoding.UTF8, MediaTypeNames.Application.Json);

            // when
            await client.PostAsync("/orders", content);
            await client.PatchAsync($"/orders/{orderCreateDto.OrderNumber}", content2);

            var orderResponse = await client.GetAsync($"/orders/{orderCreateDto.OrderNumber}");
            var body = await orderResponse.Content.ReadAsStringAsync();

            var returnOrder = JsonConvert.DeserializeObject<OrderFullDto>(body);
            // then
            Assert.Equal(OrderFullDto.OrderStatus.Close, returnOrder.Status);
        }

        [Fact]
        public async Task Should_Return_Error_Message_When_Parking_Lot_Is_Full()
        {
            // given
            var client = GetClient();
            var parkingLotDto = new ParkingLotDto()
            {
                Name = "parkinglot1",
                Capacity = 0,
                Location = "Beijing",
            };

            var parkingLotContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent parkingLotcontent = new StringContent(parkingLotContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            // when
            await client.PostAsync("/parkinglots", parkingLotcontent);
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
            var postResponse = await client.PostAsync("/orders", content);

            var body = await postResponse.Content.ReadAsStringAsync();
            // then
            Assert.Equal("The parking lot is full", body);
        }
    }
}
