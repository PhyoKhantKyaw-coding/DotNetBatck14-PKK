using DotNet_Batch14PKK.MiniPos.Features;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DotNetBatch14PKK.MiniPos.Features.MiniPos
{
    public class EfcoreSaledetailServices
    {
        private readonly AppDbContent _db;

        public EfcoreSaledetailServices()
        {
            _db = new AppDbContent();
        }

        public List<SaleModel> GetAllSales()
        {
            return _db.sale.AsNoTracking().ToList();
        }

        public SaleModel GetSaleById(string saleId)
        {
            return _db.sale.AsNoTracking().FirstOrDefault(s => s.SaleId == saleId)!;
        }

        public List<SaleDetailModel> GetAllSaleDetailsBySaleId(string saleId)
        {
            return _db.saledetail
                .Include(sd => sd.ProductId)
                .AsNoTracking()
                .Where(sd => sd.SaleId == saleId)
                .ToList();
        }

        public ResponseModel CreateSale(List<string> productIds, List<int> quantities)
        {
            if (productIds == null || quantities == null || productIds.Count != quantities.Count || !productIds.Any())
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "Product IDs and quantities must be non-empty and of the same length."
                };
            }

            var saleId = Guid.NewGuid().ToString();
            var sale = new SaleModel
            {
                SaleId = saleId,
                SaleDate = DateTime.Now,
                STotalQty = 0,
                STA = 0
            };

            for (int i = 0; i < productIds.Count; i++)
            {
                var productId = productIds[i];
                var qty = quantities[i];

                var product = _db.product.FirstOrDefault(p => p.ProductId == productId);
                if (product == null)
                {
                    return new ResponseModel
                    {
                        IsSuccessful = false,
                        Message = $"Product with ID {productId} not found."
                    };
                }

                if (product.Qty < qty)
                {
                    return new ResponseModel
                    {
                        IsSuccessful = false,
                        Message = $"Insufficient stock for Product ID {productId}."
                    };
                }

                var totalPrice = product.Price * qty;

                product.Qty -= qty;
                _db.Entry(product).State = EntityState.Modified;

                var saleDetail = new SaleDetailModel
                {
                    SaleDetailId = Guid.NewGuid().ToString(),
                    SaleId = saleId,
                    ProductId = productId,
                    Qty = qty,
                    TotalPrice = totalPrice
                };

                _db.saledetail.Add(saleDetail);

                sale.STotalQty += qty;
                sale.STA += totalPrice;
            }

            _db.sale.Add(sale);

            var result = _db.SaveChanges();

            return new ResponseModel
            {
                IsSuccessful = result > 0,
                Message = result > 0 ? "Sale created successfully, and product quantities updated." : "Failed to create sale."
            };
        }


    }
}
