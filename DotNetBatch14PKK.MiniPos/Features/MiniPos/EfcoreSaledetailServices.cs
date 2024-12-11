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

        public List<SaleDetailModel> GetAllSaleDetailsBySaleId(int vouNO)
        {
            return _db.saledetail
                .AsNoTracking()
                .Where(sd => sd.VoucherNo == vouNO)
                .ToList();
        }

        public ResponseModel CreateSale(List<CreateSalesRequest> saleProducts,int vouNo)
        {
            var saleId = Guid.NewGuid().ToString();
            var sale = new SaleModel
            {
                SaleId = saleId,
                SaleDate = DateTime.Now,
                STotalQty = 0,
                STA = 0,
                VoucherNo = vouNo,

            };

            foreach (var saleProduct in saleProducts)
            {
                var product = _db.product.FirstOrDefault(p => p.ProductCode == Convert.ToInt32( saleProduct.ProductCode));
                if (product == null)
                {
                    return new ResponseModel
                    {
                        IsSuccessful = false,
                        Message = $"Product with 101 {saleProduct.ProductCode} not found."
                    };
                }

                if (product.Qty < saleProduct.Quantity)
                {
                    return new ResponseModel
                    {
                        IsSuccessful = false,
                        Message = $"Insufficient stock for Product code {saleProduct.ProductCode}."
                    };
                }

                var totalPrice = product.Price * saleProduct.Quantity;
                product.Qty -= saleProduct.Quantity;
                _db.Entry(product).State = EntityState.Modified;

                var saleDetail = new SaleDetailModel
                {
                    SaleDetailId = Guid.NewGuid().ToString(),
                    VoucherNo = vouNo,
                    ProductCode =Convert.ToInt32(saleProduct.ProductCode),
                    Qty = saleProduct.Quantity,
                    TotalPrice = totalPrice
                };

                _db.saledetail.Add(saleDetail);

                sale.STotalQty += saleProduct.Quantity;
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
