using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Pagning.Models;

namespace Pagning.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly IMapper _mapper;

        public ProductsController(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        [HttpGet(Name = nameof(GetProducts))]
        public IActionResult GetProducts(int page = 0, int pageSize = 10)
        {
            var items = _dataService.GetProducts(page, pageSize).Select(CreateDto);

            var numberOfProducts = _dataService.NumberOfProducts();

            var pages = (int) Math.Ceiling((double) numberOfProducts / pageSize);

            var prev = (string) null;
            if (page > 0)
            {
                prev = Url.Link(nameof(GetProducts), new {page = page - 1, pageSize});
            }

            var next = (string)null;
            if (page < pages - 1)
            {
                next = Url.Link(nameof(GetProducts), new { page = page + 1, pageSize });
            }


            var result = new
            {
                pageSizes = new int[]{5,10,15,20}, 
                count = numberOfProducts,
                pages,
                prev,
                next,
                items
            };
            
            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetProduct))]
        public IActionResult GetProduct(int id)
        {
            var product = _dataService.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(CreateDto(product));
        }

        ProductDto CreateDto(Product product)
        {
            var dto = _mapper.Map<ProductDto>(product);
            dto.Url = Url.Link(nameof(GetProduct), new {product.Id});
            dto.CategoryUrl = Url.Link(nameof(CategoriesController.GetCategory), new {Id = product.Category.Id});
            return dto;
        }

        ProductListElementDto CreateDto(ProductListElement product)
        {
            var dto = _mapper.Map<ProductListElementDto>(product);
            dto.Url = Url.Link(nameof(GetProduct), new { product.Id });
            return dto;
        }
    }
}
