using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkelandStore.Core.Entities;
using SkelandStore.Core.Interface_sRepository;
using SkylandStore.DTOs;
using SkylandStore.Errors;
namespace SkylandStore.Controllers
{
    public class BasketController : ApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository,
                                IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        //Get Basket - ReCreate Basket
        [HttpGet]
        public async Task<ActionResult<CustomerBasketDTO>> GetCustomerBasket(string Basketid)
        {
            var Basket = await _basketRepository.GetBasketAsync(Basketid);
            if (Basket is not null)
            {
                var mappedBasket = _mapper.Map<CustomerBasket, CustomerBasketDTO>(Basket);
            }
            return Basket is null ? new CustomerBasketDTO(Basketid) : Ok(Basket);
            //if Basket is null this mean that Expire date ended so it ReCreate New Empty Basket with the same Id Of Basket
        }

        //Update Basket
        [HttpPost]
        public async Task<ActionResult<CustomerBasketDTO>> UpdateCustomerBasket(CustomerBasketDTO customerBasket)
        {
            var MappedBasket = _mapper.Map<CustomerBasketDTO, CustomerBasket>(customerBasket);
            var Basket = await _basketRepository.UpdateBasketAsync(MappedBasket);
            return Basket is null ? BadRequest(new ApiResponse(400)) : Ok(Basket);
        }

        //Delete Basket
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteCustomerBasket(string BasketId)
        => await _basketRepository.Deletebasket(BasketId);


    }
}
