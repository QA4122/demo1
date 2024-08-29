using Microsoft.EntityFrameworkCore;
using Shopdemo1.Data;
using Shopdemo1.Models;

namespace Shopdemo1.Repository
{
    public interface ICategoryRepository
    {
        Category GetByName(string Name);  // Lấy thông tin danh mục theo mã
        dynamic GetAll(FilterModel filter);  // Lấy tất cả danh mục
        List<Product> GetAllProd(FilterModel filter, int id);
        bool AddCategory(Category category);  // Thêm danh mục mới
        bool UpdateCategory(Category category);  // Cập nhật thông tin danh mục
        bool DeleteCategory(int id);  // Xóa danh mục theo mã
        bool Save();
    }
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext context;

        public CategoryRepository(DataContext context)
        {
            this.context = context;
        }
        public bool AddCategory(Category category)
        {
            context.categories.Add(category);
            return Save();
        }

        public bool DeleteCategory(int id)
        {
            
            var i = context.categories.Where(c=>c.CategoryId == id).FirstOrDefault();
            if (i != null)
            {
                var p = context.products.Where(c => c.CategoryId == id).ToList();
                context.products.RemoveRange(p);
                context.categories.Remove(i);
            }
            return Save();
        }

        public dynamic GetAll(FilterModel filter)
        {
            var rs = context.categories.AsNoTracking().ToList();
            if (filter.SearchString != string.Empty)
            {
                rs = rs.Where(c => c.CategoryName.Contains(filter.SearchString)).ToList();
            }
            int length = rs.Count();
            var ressult = rs.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            var temp = new
            {
                data = ressult.ToList(),
                length = length,
            };
            return temp;
        }

        public List<Product> GetAllProd(FilterModel filter, int id)
        {
            return context.products.Where(c => c.CategoryId == id).ToList();
        }

        public Category GetByName(string Name)
        {
            return context.categories.Where(c => c.CategoryName == Name).FirstOrDefault();
        }

        public bool Save()
        {
            var save = context.SaveChanges();
            return save > 0 ? true : false;
        }

        public bool UpdateCategory(Category category)
        {
            context.categories.Update(category);
            return Save();
        }
    }
}
