using Microsoft.AspNetCore.Mvc;
using DotNetBatch14PKK.MiniPos.Features.MiniPos;
using DotNet_Batch14PKK.MiniPos.Features;

namespace DotNetBatch14PKK.MiniPos.Features.MiniPos
{
    [Route("api/[controller]")]
    [ApiController]
    public class MiniPosController : ControllerBase
    {
        private readonly EfcoreCategoryService _categoryService;
        private readonly EfcoreProductServices _productService;
        private readonly EfcoreSaledetailServices _saleDetailService;

        public MiniPosController()
        {
            _categoryService = new EfcoreCategoryService();
            _productService = new EfcoreProductServices();
            _saleDetailService = new EfcoreSaledetailServices();
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var categories = _categoryService.GetAllCategories();
            if (categories == null || !categories.Any())
                return NotFound("No categories found.");
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoryById(string id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
                return NotFound("Category not found.");
            return Ok(category);
        }

        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryModel categoryModel)
        {
            if (categoryModel == null)
                return BadRequest("Invalid category data.");

            var response = _categoryService.CreateCategory(categoryModel);
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }

        [HttpPatch]
        public IActionResult UpdateCategory([FromBody] CategoryModel categoryModel)
        {
            if (categoryModel == null || string.IsNullOrEmpty(categoryModel.CategoryId))
                return BadRequest("Invalid category data.");

            var response = _categoryService.UpdateCategory(categoryModel);
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(string id)
        {
            var response = _categoryService.DeleteCategory(id);
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }


        [HttpGet("product")]
        public IActionResult GetAllProducts()
        {
            var product = _productService.GetAllProducts();
            if (product == null || !product.Any())
                return NotFound("No products found.");
            return Ok(product);
        }

        [HttpGet("products/{id}")]
        public IActionResult GetProductById(string id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
                return NotFound("Product not found.");
            return Ok(product);
        }

        [HttpPost("products")]
        public IActionResult CreateProduct([FromBody] ProductModel productModel)
        {
            if (productModel == null)
                return BadRequest("Invalid product data.");

            var response = _productService.CreateProduct(productModel);
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }

        [HttpPatch("products/{id}")]
        public IActionResult UpdateProduct(string id, [FromBody] ProductModel productModel)
        {
            if (productModel == null || string.IsNullOrEmpty(id))
                return BadRequest("Invalid product data.");

            var response = _productService.UpdateProduct(id, productModel);
            if (!response.IsSuccessful)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }

        [HttpGet("sales")]
        public IActionResult GetAllSales()
        {
            var sales = _saleDetailService.GetAllSales();
            if (sales == null || !sales.Any())
                return NotFound("No sales found.");
            return Ok(sales);
        }

        [HttpGet("sales/{id}")]
        public IActionResult GetSaleById(string id)
        {
            var sale = _saleDetailService.GetSaleById(id);
            if (sale == null)
                return NotFound("Sale not found.");
            return Ok(sale);
        }

        [HttpGet("sales/{saleId}/details")]
        public IActionResult GetAllSaleDetailsBySaleId(string saleId)
        {
            var saleDetails = _saleDetailService.GetAllSaleDetailsBySaleId(saleId);
            if (saleDetails == null || !saleDetails.Any())
                return NotFound("No sale details found for the specified Sale ID.");
            return Ok(saleDetails);
        }

        [HttpPost("sales/details")]
        public IActionResult CreateSaleDetail([FromBody] CreateSaleRequest request)
        {
            if (request == null || request.ProductIds == null || request.Quantities == null ||
                request.ProductIds.Count != request.Quantities.Count || !request.ProductIds.Any())
            {
                return BadRequest("Invalid input. Product IDs and quantities must be non-empty and of the same length.");
            }

            var response = _saleDetailService.CreateSale(request.ProductIds, request.Quantities);
            if (!response.IsSuccessful)
                return BadRequest(response.Message);

            return Ok(response.Message);
        }

    }
}
