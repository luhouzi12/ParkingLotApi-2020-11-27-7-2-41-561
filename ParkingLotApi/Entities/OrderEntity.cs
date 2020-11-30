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

        public OrderEntity(OrderCreateDto orderDto)
        {
            OrderNumber = Guid.NewGuid().ToString();
            ParkingLotName = orderDto.ParkingLotName;
            PlateNumber = orderDto.PlateNumber;
            CreationTime = DateTime.Now;
            Status = OrderStatus.Open;
        }

        public enum OrderStatus
        {
            Close,
            Open
        }

        [Key]
        public string OrderNumber { get; set; }
        public string ParkingLotName { get; set; }
        public string PlateNumber { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime CloseTime { get; set; }
        public OrderStatus Status { get; set; }
    }
}
