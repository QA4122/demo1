using Microsoft.EntityFrameworkCore;
using Shopdemo1.Data;
using Shopdemo1.Models;

namespace Shopdemo1.Repository
{
    public interface IProductRepository
    {
        Product GetByProductCode(string productCode);
        Product GetByProductName(string productName);
        dynamic GetAll(FilterModel filter);
        bool Add(Product product);
        bool Update(Product product);
        bool Delete(string productCode);
        bool Save();
        public int GetSameDate();
    }
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext context;

        public ProductRepository(DataContext context)
        {
            this.context = context;
        }
        public bool Add(Product product)
        {
            context.products.Add(product);
            return Save();
        }

        public bool Delete(string productCode)
        {
            var product = context.products.FirstOrDefault(x => x.ProductCode == productCode);
            if (product != null)
            {
                context.products.Remove(product);
            }
            return Save();
        }

        public dynamic GetAll(FilterModel filter)
        {
            var rs = context.products.AsNoTracking().ToList();
            if (filter.SearchString != string.Empty)
            {
                rs = rs.Where(c => c.ProductCode.Contains(filter.SearchString) || c.ProductName.Contains(filter.SearchString)).ToList();
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

        public Product GetByProductCode(string productCode)
        {
            return context.products.Where(c => c.ProductCode == productCode).FirstOrDefault() ;
        }

        public Product GetByProductName(string productName)
        {
            return context.products.Where(c => c.ProductName == productName).FirstOrDefault();
        }

        public int GetSameDate()
        {
            DateTime today = DateTime.Now;
            DateTime start = new DateTime(today.Year, today.Month, today.Day);
            var finish = start.AddDays(1);
            int count = context.products.Where(c => c.CreateDate >= start && c.CreateDate <= finish).AsNoTracking().ToList().Count;
            return count;
        }

        public bool Save()
        {
            var save = context.SaveChanges();
            return save>0 ? true : false;
        }

        public bool Update(Product product)
        {
            context.products.Update(product);
            return Save();
        }
    }
}
