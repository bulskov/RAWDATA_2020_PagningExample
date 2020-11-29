using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Pagning.Models;

namespace Pagning.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly IMapper _mapper;

        public CategoriesController(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        [HttpGet(Name = nameof(GetCategories))]
        public IActionResult GetCategories()
        {
            var items = _dataService.GetCategories().Select(CreateDto);

            return Ok(items);
        }

        [HttpGet("{id}", Name = nameof(GetCategory))]
        public IActionResult GetCategory(int id)
        {
            var category = _dataService.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(CreateDto(category));
        }

        CategoryDto CreateDto(Category category)
        {
            var dto = _mapper.Map<CategoryDto>(category);
            dto.Url = Url.Link(nameof(GetCategory), new { category.Id });
            return dto;
        }
    }
}
