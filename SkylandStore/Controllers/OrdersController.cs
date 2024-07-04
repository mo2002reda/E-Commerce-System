using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkelandStore.Core.Entities.Order_Aggregation;
using SkelandStore.Core.Services;
using SkylandStore.DTOs;
using SkylandStore.Errors;
using System.Security.Claims;
using AggregateAddress = SkelandStore.Core.Entities.Order_Aggregation.Address;

namespace SkylandStore.Controllers
{
    public class OrdersController : ApiController
    {
        private readonly IOrderServices _orderServices;
        private readonly IMapper _mapper;

        public OrdersController(IOrderServices orderServices, IMapper mapper)
        {
            _orderServices = orderServices;
            _mapper = mapper;
        }

        //Create Order => BaseURl/Api/Orders
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Order>> CreateOrder(OrderDTO orderDTO)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var mappedAddress = _mapper.Map<AddressDTO, AggregateAddress>(orderDTO.ShippingAddress);
            var Order = await _orderServices.CreateOrderAsync(buyerEmail, orderDTO.basketId, orderDTO.DeliveryMethodID, mappedAddress);
            if (Order is null) return BadRequest(new ApiResponse(400, "There Is a Problem In Your Order"));
            return Ok(Order);


        }
    }
}
