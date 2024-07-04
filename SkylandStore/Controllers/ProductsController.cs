using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkelandStore.Core;
using SkelandStore.Core.Entities;
using SkelandStore.Core.Interface_sRepository;
using SkelandStore.Core.Specification;
using SkylandStore.DTOs;
using SkylandStore.Errors;
using SkylandStore.Helper;

namespace SkylandStore.Controllers
{
    [Authorize]
    public class ProductsController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductType> _types;
        private readonly IGenericRepository<ProductBrand> _brands;

        public ProductsController(IMapper mapper,
                                 IGenericRepository<Product> ProductRepo,
                                  IGenericRepository<ProductType> Types,
                                  IGenericRepository<ProductBrand> Brands
                                  )
        {
            _productRepo = ProductRepo;
            _mapper = mapper;
            _types = Types;
            _brands = Brands;
        }
        //Get All Products
        //BaseURL/api/Products

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDTo>>> GetAllProducts([FromQuery] ProductSpecParams Params)
        {//Using [FromQuery] cause ProductSpecParams is an Object and it Send into Query params ,Note that HttpGet Not have Body Cause it an Get endPoint So we Using Param to get Required Parameters 

            //var products = await _productRepo.GetAllAsync(); //to get without include
            var spec = new ProductWithBrandAndTypeWithSpecification(Params);
            var Products = await _productRepo.GetAllWithSpecAsync(spec);
            var MappedProduct = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTo>>(Products);
            var CountSpec = new ProductWithFilterationForCountAsync(Params);//Get All products
            var count = await _productRepo.GetCountWithSpecAsync(CountSpec);
            var ReturnedObject = new Pagination<ProductToReturnDTo>
            {
                PageSize = Params.PageSize,
                PageIndex = Params.PageIndex,
                Count = count,
                Data = MappedProduct

            };
            return Ok(ReturnedObject);
        }

        //Get Product By Id
        [HttpGet("{id}")]
        #region Detect Responses in decumentation swager
        [ProducesResponseType(typeof(ProductToReturnDTo), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        #endregion
        //BaseURl/api/Product/{id}

        public async Task<ActionResult<ProductToReturnDTo>> GetProductById(int id)
        {
            //var Result = await _productRepo.GetByIdAsync(id);
            var Spec = new ProductWithBrandAndTypeWithSpecification(id);

            var Result = await _productRepo.GetByIdWithSpecAsync(Spec);
            if (Result is null) return NotFound(new ApiResponse(404));
            var MappingProduct = _mapper.Map<Product, ProductToReturnDTo>(Result);
            return Ok(MappingProduct);
        }

        //Get All Types
        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetAllTypes()
        {
            var Type = await _types.GetAllAsync();
            return Ok(Type);
        }

        //Get All Brands
        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
        {
            var Brands = await _brands.GetAllAsync();
            return Ok(Brands);
        }
    }
}
