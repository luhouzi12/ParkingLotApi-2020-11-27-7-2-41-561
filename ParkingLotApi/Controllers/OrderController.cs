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
        public async Task<ActionResult<IEnumerable<OrderCreateDto>>> GetAll()
        {
            var allOrderDtos = await orderService.GetAllOrders();
            return Ok(allOrderDtos);
        }

        [HttpGet("{number}")]
        public async Task<ActionResult<OrderFullDto>> GetByNumber(string number)
        {
            var orderFullDto = await orderService.GetOrderByNumber(number);
            return Ok(orderFullDto);
        }

        [HttpPost]
        public async Task<ActionResult<OrderCreateDto>> CreateOrder(OrderCreateDto orderDto)
        {
            var number = await orderService.AddOrder(orderDto);
            return CreatedAtAction(nameof(GetByNumber), new { number = number }, orderDto);
        }

        [HttpPatch("{number}")]
        public async Task<ActionResult<OrderFullDto>> CarLeave(string number)
        {
            await orderService.UpdateOrderCloseTime(number);
            await orderService.CloseOrder(number);
            return Ok(await GetByNumber(number));
        }
    }
}