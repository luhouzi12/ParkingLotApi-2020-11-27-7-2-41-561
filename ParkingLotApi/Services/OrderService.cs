using ParkingLotApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkingLotApi.Dtos;
using ParkingLotApi.Entities;

namespace ParkingLotApi.Services
{
    public class OrderService
    {
        private readonly ParkingLotContext parkingLotContext;

        public OrderService(ParkingLotContext parkingLotContext)
        {
            this.parkingLotContext = parkingLotContext;
        }

        public async Task<List<OrderDto>> GetAllOrders()
        {
            var allOrders = await parkingLotContext.Orders.ToListAsync();
            return allOrders.Select(orderEntity => new OrderDto(orderEntity)).ToList();
        }

        public async Task<string> AddOrder(OrderDto orderDto)
        {
            OrderEntity orderEntity = new OrderEntity(orderDto);
            await parkingLotContext.Orders.AddAsync(orderEntity);
            await parkingLotContext.SaveChangesAsync();
            return orderDto.OrderNumber;
        }

        public async Task<OrderDto> GetOrderByNumber(string number)
        {
            var foundOrderEntity = await parkingLotContext.Orders.FirstOrDefaultAsync(order => order.OrderNumber == number);
            return new OrderDto(foundOrderEntity);
        }
    }
}
