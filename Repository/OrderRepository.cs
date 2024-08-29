using Shopdemo1.Data;
using Shopdemo1.Models;

namespace Shopdemo1.Repository
{
    public interface IOrderRepository
    {
        Order GetById(int id);  // Lấy thông tin đơn hàng theo mã
        List<Order> GetAll(FilterModel filter);  // Lấy tất cả đơn hàng
        List<Order> GetByCustomerId(int customerId, FilterModel filter);  // Lấy đơn hàng theo mã khách hàng
        bool AddOrder(Order order);  // Thêm đơn hàng mới
        bool UpdateOrder(Order order);  // Cập nhật thông tin đơn hàng
        bool DeleteOrder(int id);  // Xóa đơn hàng theo mã
        bool Save();
    }
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext context;

        public OrderRepository(DataContext context)
        {
            this.context = context;
        }
        public bool AddOrder(Order order)
        {
            context.orders.Add(order);
            return Save();
        }

        public bool DeleteOrder(int id)
        {
            var i = context.orders.Where(c=> c.OrderId == id).FirstOrDefault();
            context.orders.Remove(i);
            return Save();
        }

        public List<Order> GetAll(FilterModel filter)
        {
            return context.orders.Skip((filter.PageNumber - 1) * filter.PageSize)
        .Take(filter.PageSize).ToList();
        }

        public List<Order> GetByCustomerId(int customerId, FilterModel filter)
        {
            return context.orders.Where(c=>c.CustomerId==customerId).Skip((filter.PageNumber - 1) * filter.PageSize)
        .Take(filter.PageSize).ToList();
        }

        public Order GetById(int id)
        {
            return context.orders.Where(c => c.OrderId == id).FirstOrDefault();
        }

        public bool Save()
        {
            var save = context.SaveChanges();
            return save > 0 ? true : false;
        }

        public bool UpdateOrder(Order order)
        {
            context.orders.Update(order);
            return Save();
        }
    }
}
