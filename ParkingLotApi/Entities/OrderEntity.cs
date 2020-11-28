using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ParkingLotApi.Dtos;

namespace ParkingLotApi.Entities
{
    public class OrderEntity
    {
        public OrderEntity()
        {
        }

        public OrderEntity(OrderDto orderDto)
        {
            OrderNumber = orderDto.OrderNumber;
            ParkingLotName = orderDto.ParkingLotName;
            PlateNumber = orderDto.PlateNumber;
            CreationTime = orderDto.CreationTime;
            CloseTime = orderDto.CloseTime;
            Status = (OrderStatus)orderDto.Status;
        }

        public enum OrderStatus
        {
            Close,
            Open
        }

        [Key]
        public string OrderNumber { get; set; } = Guid.NewGuid().ToString();
        public string ParkingLotName { get; set; }
        public string PlateNumber { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime CloseTime { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Open;
    }
}
