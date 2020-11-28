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
        public async Task<ActionResult<OrderCreateDto>> CreateOrder(OrderCreateDto orderCreateDto)
        {
            if (!await orderService.CanPark(orderCreateDto.ParkingLotName))
            {
                return BadRequest("The parking lot is full");
            }

            var number = await orderService.AddOrder(orderCreateDto);
            return CreatedAtAction(nameof(GetByNumber), new { number = number }, orderCreateDto);
        }

        [HttpPatch("{orderPatchDto.OrderNumber}")]
        public async Task<ActionResult<OrderFullDto>> CarLeave(OrderPatchDto orderPatchDto)
        {
            await orderService.UpdateOrderCloseTime(orderPatchDto.OrderNumber);
            await orderService.CloseOrder(orderPatchDto.OrderNumber);
            return Ok(await GetByNumber(orderPatchDto.OrderNumber));
        }
    }
}