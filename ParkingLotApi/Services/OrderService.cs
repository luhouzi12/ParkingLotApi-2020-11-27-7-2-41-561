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
            var foundParkingLot =
                await parkingLotContext.ParkingLots.FirstOrDefaultAsync(parkingLot =>
                    parkingLot.Name == orderCreateDto.ParkingLotName);
            foundParkingLot.Orders.Add(orderEntity);
            await parkingLotContext.SaveChangesAsync();
            return orderEntity.OrderNumber;
        }

        public async Task<OrderFullDto> GetOrderByNumber(string number)
        {
            var foundOrderEntity = await parkingLotContext.Orders.FirstOrDefaultAsync(order => order.OrderNumber == number);
            return new OrderFullDto(foundOrderEntity);
        }

        public async Task<OrderFullDto> GetOrderByPlateNumber(string plateNumber)
        {
            var foundOrderEntity = await parkingLotContext.Orders.FirstOrDefaultAsync(order => order.PlateNumber == plateNumber);
            return new OrderFullDto(foundOrderEntity);
        }

        public async Task<OrderFullDto> UpdateOrderCloseTime(string number)
        {
            var foundOrderEntity = await parkingLotContext.Orders.FirstOrDefaultAsync(order => order.OrderNumber == number);
            foundOrderEntity.CloseTime = DateTime.Now;
            await parkingLotContext.SaveChangesAsync();
            return new OrderFullDto(foundOrderEntity);
        }

        public async Task<bool> CanPark(string parkingLotName)
        {
            var foundParkingLot =
                await parkingLotContext.ParkingLots.FirstOrDefaultAsync(parkingLot =>
                    parkingLot.Name == parkingLotName);
            return foundParkingLot.Orders.Count < foundParkingLot.Capacity;
        }

        public async Task<OrderFullDto> CloseOrder(string number)
        {
            var foundOrderEntity = await parkingLotContext.Orders.FirstOrDefaultAsync(order => order.OrderNumber == number);
            foundOrderEntity.Status = OrderEntity.OrderStatus.Close;
            var foundParkingLot =
                await parkingLotContext.ParkingLots.FirstOrDefaultAsync(parkingLot =>
                    parkingLot.Name == foundOrderEntity.ParkingLotName);
            foundParkingLot.Orders.Remove(foundOrderEntity);
            await parkingLotContext.SaveChangesAsync();
            return new OrderFullDto(foundOrderEntity);
        }
    }
}
