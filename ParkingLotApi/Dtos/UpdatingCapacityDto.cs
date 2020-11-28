using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingLotApi.Dtos
{
    public class UpdatingCapacityDto
    {
        public UpdatingCapacityDto()
        {
        }

        public UpdatingCapacityDto(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
        }

        public string Name { get; set; }
        public int Capacity { get; set; }
    }
}
