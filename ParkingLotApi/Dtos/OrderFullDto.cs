using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingLotApi.Entities;

namespace ParkingLotApi.Dtos
{
    public class OrderFullDto
    {
        public OrderFullDto()
        {
        }

        public OrderFullDto(OrderEntity orderEntity)
        {
            OrderNumber = orderEntity.OrderNumber;
            ParkingLotName = orderEntity.ParkingLotName;
            PlateNumber = orderEntity.PlateNumber;
            CreationTime = orderEntity.CreationTime;
            CloseTime = orderEntity.CloseTime;
            Status = (OrderStatus)orderEntity.Status;
        }

        public enum OrderStatus
        {
            Close,
            Open
        }

        public string OrderNumber { get; set; } = Guid.NewGuid().ToString();
        public string ParkingLotName { get; set; }
        public string PlateNumber { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime CloseTime { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Open;
    }
}