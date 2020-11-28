using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkingLotApi.Dtos;
using ParkingLotApi.Services;

namespace ParkingLotApi.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService orderService;

        public OrderController(OrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
        {
            var allOrderDtos = await orderService.GetAllOrders();
            return Ok(allOrderDtos);
        }

        [HttpGet("{number}")]
        public async Task<ActionResult<ParkingLotDto>> GetByNumber(string number)
        {
            var parkingLotDto = await orderService.GetOrderByNumber(number);
            return Ok(parkingLotDto);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> AddOrder(OrderDto orderDto)
        {
            var number = await orderService.AddOrder(orderDto);
            return CreatedAtAction(nameof(GetByNumber), new {number = number}, orderDto);
        }
    }
}