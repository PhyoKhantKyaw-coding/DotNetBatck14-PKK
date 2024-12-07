using DotNetBatch14PKK.Mini_Pos.Features.MiniPos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DotNetBatch14PKK.Mini_Pos.Features.MiniPos
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
            return _db.sale.Include(s => s.STotalQty).Include(s => s.STA).AsNoTracking().ToList();
        }

        public SaleModel GetSaleById(string saleId)
        {
            return _db.sale.AsNoTracking().FirstOrDefault(s => s.SaleId == saleId);
        }

        public List<SaleDetailModel> GetAllSaleDetailsBySaleId(string saleId)
        {
            return _db.saledetail
                .Include(sd => sd.ProductId)
                .AsNoTracking()
                .Where(sd => sd.SaleId == saleId)
                .ToList();
        }

        public ResponseModel CreateSale(List<SaleDetailModel> saleDetails)
        {
            if (saleDetails == null || !saleDetails.Any())
            {
                return new ResponseModel
                {
                    IsSuccessful = false,
                    Message = "Sale details list cannot be empty."
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

            foreach (var detail in saleDetails)
            {
                detail.SaleDetailId = Guid.NewGuid().ToString();
                detail.SaleId = saleId;

                var product = _db.product.FirstOrDefault(p => p.ProductId == detail.ProductId);
                if (product == null)
                {
                    return new ResponseModel
                    {
                        IsSuccessful = false,
                        Message = $"Product with ID {detail.ProductId} not found."
                    };
                }

                if (product.Qty < detail.Qty)
                {
                    return new ResponseModel
                    {
                        IsSuccessful = false,
                        Message = $"Insufficient stock for Product ID {detail.ProductId}."
                    };
                }

                detail.TotalPrice = detail.Qty * product.Price;

                product.Qty -= detail.Qty;
                _db.Entry(product).State = EntityState.Modified;

                sale.STotalQty += detail.Qty;
                sale.STA += detail.TotalPrice;

                _db.saledetail.Add(detail);
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
