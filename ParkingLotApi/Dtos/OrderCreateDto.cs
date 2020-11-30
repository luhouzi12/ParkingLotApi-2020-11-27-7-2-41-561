using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingLotApi.Entities;

namespace ParkingLotApi.Dtos
{
    public class OrderCreateDto
    {
        public OrderCreateDto()
        {
        }

        public enum OrderStatus
        {
            Close,
            Open
        }

        public string ParkingLotName { get; set; }
        public string PlateNumber { get; set; }
    }
}
