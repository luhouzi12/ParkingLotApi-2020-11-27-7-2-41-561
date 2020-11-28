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

        public async Task<List<OrderFullDto>> GetAllOrders()
        {
            var allOrders = await parkingLotContext.Orders.ToListAsync();
            return allOrders.Select(orderEntity => new OrderFullDto(orderEntity)).ToList();
        }

        public async Task<string> AddOrder(OrderCreateDto orderCreateDto)
        {
            OrderEntity orderEntity = new OrderEntity(orderCreateDto);
            await parkingLotContext.Orders.AddAsync(orderEntity);
            await parkingLotContext.SaveChangesAsync();
            return orderCreateDto.OrderNumber;
        }

        public async Task<OrderFullDto> GetOrderByNumber(string number)
        {
            var foundOrderEntity = await parkingLotContext.Orders.FirstOrDefaultAsync(order => order.OrderNumber == number);
            return new OrderFullDto(foundOrderEntity);
        }
    }
}
