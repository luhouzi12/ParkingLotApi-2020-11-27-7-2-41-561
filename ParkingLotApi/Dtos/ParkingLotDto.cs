using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingLotApi.Entities;

namespace ParkingLotApi.Dtos
{
    public class ParkingLotDto
    {
        public ParkingLotDto()
        {
        }

        public ParkingLotDto(ParkingLotEntity parkingLotEntity)
        {
            Capacity = parkingLotEntity.Capacity;
            Name = parkingLotEntity.Name;
            Location = parkingLotEntity.Location;
        }

        public string Name { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
            {
                return false;
            }

            var parkingLotDto = (ParkingLotDto)obj;
            return Name == parkingLotDto.Name && Capacity == parkingLotDto.Capacity && Location == parkingLotDto.Location;
        }
    }
}
