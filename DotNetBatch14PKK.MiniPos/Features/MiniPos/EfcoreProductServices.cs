using DotNet_Batch14PKK.MiniPos.Features;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DotNetBatch14PKK.MiniPos.Features.MiniPos
{
    public class EfcoreProductServices
    {
        private readonly DotNet_Batch14PKK.MiniPos.Features.AppDbContent _db;

        public EfcoreProductServices()
        {
            _db = new AppDbContent();
        }

        public List<ProductModel> GetAllProducts()
        {
            return _db.product.AsNoTracking().ToList();
        }

        public ProductModel GetProductById(string productId)
        {
            return _db.product.AsNoTracking().FirstOrDefault(p => p.ProductId == productId)!;
        }

        public ResponseModel UpdateProduct(string id, ProductModel updatedProduct)
        {
            var existingProduct = _db.product.FirstOrDefault(p => p.ProductId == id);
            if (existingProduct == null)
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "Product not found."
                };
            }

            if (!string.IsNullOrEmpty(updatedProduct.ProductName))
            {
                existingProduct.ProductName = updatedProduct.ProductName;
            }
            if (updatedProduct.Qty > 0)
            {
                existingProduct.Qty = updatedProduct.Qty;
            }
            if (updatedProduct.Price > 0)
            {
                existingProduct.Price = updatedProduct.Price;
            }
            if (updatedProduct.CatCode>0)
            {
                existingProduct.CatCode = updatedProduct.CatCode;
            }

            _db.Entry(existingProduct).State = EntityState.Modified;
            var result = _db.SaveChanges();

            return new ResponseModel
            {
                IsSuccessful = result > 0,
                Message = result > 0 ? "Product updated successfully." : "Product update failed."
            };
        }
        public ResponseModel CreateProduct(ProductModel newProduct)
        {

            var categoryExists = _db.cats.Any(c => c.Catcode == newProduct.CatCode);
            if (!categoryExists)
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "Category not found. Please provide a valid Categorycode."
                };
            }

            if (string.IsNullOrEmpty(newProduct.ProductId))
            {
                newProduct.ProductId = Guid.NewGuid().ToString();
            }

            _db.product.Add(newProduct);
            var result = _db.SaveChanges();

            return new ResponseModel
            {
                IsSuccessful = result > 0,
                Message = result > 0 ? "Product created successfully." : "Product creation failed."
            };
        }

    }
}
