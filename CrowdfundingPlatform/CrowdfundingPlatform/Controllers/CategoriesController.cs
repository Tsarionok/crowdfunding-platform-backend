using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Service;
using CrowdfundingPlatform.Models;
using CrowdfundingPlatform.Models.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrowdfundingPlatform.Controllers
{
    // TODO: Include automapper
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<ICollection<CategoryModel>>> Get()
        {
            ICollection<CategoryModel> categories = new List<CategoryModel>();

            foreach (CategoryDTO readableCategory in await _service.ReadAll())
            {
                categories.Add(new CategoryModel
                {
                    Id = readableCategory.Id,
                    Name = readableCategory.Name
                });
            }
            return Ok(categories);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryModel>> Get(int id)
        {
            CategoryDTO readableCategory = await _service.ReadById(id);

            return Ok(new CategoryModel
            {
                Id = readableCategory.Id,
                Name = readableCategory.Name
            });
        }

        [Authorize(Roles = "editor")]
        [HttpPost]
        public async Task<ActionResult> Post(CategoryCreateModel model)
        {
            if (model == null || model.Name == "")
            {
                return BadRequest();
            }
            await _service.Create(new CategoryDTO
            {
                Name = model.Name
            });

            return Ok();
        }

        [Authorize(Roles = "editor")]
        [HttpPut]
        public async Task<ActionResult> Put(CategoryModel category)
        {
            if (category == null)
            {
                return NotFound();
            }
            await _service.Update(new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name
            });
            return Ok();
        }

        // TODO: refactor return to CategoryModel type
        [Authorize(Roles = "editor")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            CategoryDTO category = await _service.DeleteById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
    }
}
