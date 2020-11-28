using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkingLotApi.Dtos;
using ParkingLotApi.Entities;
using ParkingLotApi.Repository;

namespace ParkingLotApi.Services
{
    public class ParkingLotService
    {
        private readonly ParkingLotContext parkingLotContext;

        public ParkingLotService(ParkingLotContext parkingLotContext)
        {
            this.parkingLotContext = parkingLotContext;
        }

        public async Task<List<ParkingLotDto>> GetAllParkingLots()
        {
            var parkingLots = await parkingLotContext.ParkingLots.ToListAsync();
            return parkingLots.Select(parkingLotEntity => new ParkingLotDto(parkingLotEntity)).ToList();
        }

        public async Task<string> AddParkingLot(ParkingLotDto parkingLotDto)
        {
            ParkingLotEntity parkingLotEntity = new ParkingLotEntity(parkingLotDto);
            await parkingLotContext.ParkingLots.AddAsync(parkingLotEntity);
            await parkingLotContext.SaveChangesAsync();
            return parkingLotEntity.Name;
        }

        public async Task<ParkingLotDto> GetByName(string name)
        {
            var foundParkingLotEntity = await 
                parkingLotContext.ParkingLots.FirstOrDefaultAsync(parkingLot => parkingLot.Name == name);
            return new ParkingLotDto(foundParkingLotEntity);
        }

        public async Task DeleteParkingLot(string name)
        {
            var foundParkingLotEntity = await 
                parkingLotContext.ParkingLots.FirstOrDefaultAsync(parkingLot => parkingLot.Name == name);
            parkingLotContext.ParkingLots.Remove(foundParkingLotEntity);
            await parkingLotContext.SaveChangesAsync();
        }
    }
}
