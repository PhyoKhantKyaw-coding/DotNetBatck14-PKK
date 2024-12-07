using DotNetBatch14PKK.Mini_Pos.Features.MiniPos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DotNetBatch14PKK.Mini_Pos.Features.MiniPos;

public class EfcoreCategoryService
{
    private readonly AppDbContent _db;

    public EfcoreCategoryService()
    {
        _db = new AppDbContent();
    }

    public List<CategoryModel> GetAllCategories()
    {
        return _db.cats.AsNoTracking().ToList();
    }

    public CategoryModel GetCategoryById(string categoryId)
    {
        return _db.cats.AsNoTracking().FirstOrDefault(cat => cat.CategoryId == categoryId);
    }

    public ResponseModel CreateCategory(CategoryModel category)
    {
        category.CategoryId = Guid.NewGuid().ToString();
        _db.cats.Add(category);
        var result = _db.SaveChanges();

        return new ResponseModel
        {
            IsSuccessful = result > 0,
            Message = result > 0 ? "Category created successfully." : "Category creation failed."
        };
    }

    public ResponseModel UpdateCategory(CategoryModel category)
    {
        var existingCategory = _db.cats.AsNoTracking().FirstOrDefault(cat => cat.CategoryId == category.CategoryId);
        if (existingCategory == null)
        {
            return new ResponseModel
            {
                IsSuccessful = false,
                Message = "Category not found."
            };
        }

        existingCategory.CatName = category.CatName;
        _db.Entry(existingCategory).State = EntityState.Modified;
        var result = _db.SaveChanges();

        return new ResponseModel
        {
            IsSuccessful = result > 0,
            Message = result > 0 ? "Category updated successfully." : "Category update failed."
        };
    }

    public ResponseModel DeleteCategory(string categoryId)
    {
        var category = _db.cats.FirstOrDefault(cat => cat.CategoryId == categoryId);
        if (category == null)
        {
            return new ResponseModel
            {
                IsSuccessful = false,
                Message = "Category not found."
            };
        }

        var isCategoryInUse = _db.product.Any(prod => prod.CategoryId == categoryId);
        if (isCategoryInUse)
        {
            return new ResponseModel
            {
                IsSuccessful = false,
                Message = "Category cannot be deleted because it is in use."
            };
        }

        _db.cats.Remove(category);
        var result = _db.SaveChanges();

        return new ResponseModel
        {
            IsSuccessful = result > 0,
            Message = result > 0 ? "Category deleted successfully." : "Category deletion failed."
        };
    }
}
