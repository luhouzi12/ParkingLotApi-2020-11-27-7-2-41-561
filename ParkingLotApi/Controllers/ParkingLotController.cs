using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParkingLotApi.Dtos;
using ParkingLotApi.Services;

namespace ParkingLotApi.Controllers
{
    [ApiController]
    [Route("parkinglots")]
    public class ParkingLotController : ControllerBase
    {
        private readonly ParkingLotService parkingLotService;
        public ParkingLotController(ParkingLotService parkingLotService)
        {
            this.parkingLotService = parkingLotService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingLotDto>>> List()
        {
            var parkingLotDtos = await parkingLotService.GetAllParkingLots();
            return Ok(parkingLotDtos);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<ParkingLotDto>> GetByName(string name)
        {
            var parkingLotDto = await this.parkingLotService.GetByName(name);
            return Ok(parkingLotDto);
        }

        [HttpPost]
        public async Task<ActionResult<ParkingLotDto>> Add(ParkingLotDto parkingLotDto)
        {
            var name = await parkingLotService.AddParkingLot(parkingLotDto);
            return CreatedAtAction(nameof(GetByName), new { name = name }, parkingLotDto);
        }

        [HttpDelete("{name}")]
        public async Task<ActionResult> Delete(string name)
        {
            await parkingLotService.DeleteParkingLot(name);
            return NoContent();
        }
    }
}
