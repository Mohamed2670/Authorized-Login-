using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MwTesting.Authentication;
using MwTesting.Data;
using MwTesting.Dtos;
using MwTesting.Model;

namespace MwTesting.Controllers
{
    [Route("api/product")]
    [ApiController]
    [Authorize]
    public class ProductController(IMapper mapper, IProductAc productAc) : ControllerBase
    {

        [HttpGet]
        [CheckPermission(Perm.ReadProducts)]
        public ActionResult<IEnumerable<ProductReadDto>> Get()
        {
            var item = productAc.GetAllProducts();
            // Get this values form that 
            var final = mapper.Map<IEnumerable<ProductReadDto>>(item);
            return Ok(final);
        }

        [HttpGet("{id}", Name = "GetProductById")]
        [CheckPermission(Perm.ReadProducts)]
        public ActionResult<ProductReadDto> GetById(int id)
        {
            var item = productAc.GetProductById(id);
            if (item == null)
            {
                return NotFound();
            }
            var final = mapper.Map<ProductReadDto>(item);
            return Ok(final);
        }
        [HttpPost]
        [CheckPermission(Perm.AddProducts)]
        public ActionResult<Product> CreateProduct(ProductCreateDto product)
        {
            var temp = mapper.Map<Product>(product);
            productAc.CreateProduct(temp);
            productAc.SaveChanges();
            return Ok(temp);
        }
        [HttpPut("{id}")]
        
        public ActionResult<Product> UpdateProduct(int id, ProductUpdateDto product)
        {
            var item = productAc.GetProductById(id);
            if (item == null)
            {
                return NotFound();
            }
            mapper.Map(product, item);
            productAc.SaveChanges();
            return Ok(product);
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var item = productAc.GetProductById(id);
            if (item == null)
            {
                return NotFound();
            }
            productAc.DeleteProduct(item);
            productAc.SaveChanges();
            return Ok();
        }
       

    }
}