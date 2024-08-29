using Shopdemo1.Data;
using Shopdemo1.Models;

namespace Shopdemo1.Repository
{
    public interface IOrderItemRepository
    {
        OrderItem GetById(int id);  // Lấy thông tin chi tiết đơn hàng theo mã
        List<OrderItem> GetByOrderId(int orderId, FilterModel filter);  // Lấy danh sách chi tiết đơn hàng theo mã đơn hàng
        bool AddOrderItem(OrderItem orderItem);  // Thêm chi tiết đơn hàng mới
        bool UpdateOrderItem(OrderItem orderItem);  // Cập nhật thông tin chi tiết đơn hàng
        bool DeleteOrderItem(int id);  // Xóa chi tiết đơn hàng theo mã
        bool save();
    }
    public class OrderItemrRepository : IOrderItemRepository
    {
        private readonly DataContext context;

        public OrderItemrRepository(DataContext context)
        {
            this.context = context;
        }
        public bool AddOrderItem(OrderItem orderItem)
        {
            context.orderItems.Add(orderItem);
            return save();
        }

        public bool DeleteOrderItem(int id)
        {
            var orderItem = context.orderItems.Where(c => c.OrderItemId == id).FirstOrDefault();
            context.orderItems.Remove(orderItem);
            return save();
        }

        public OrderItem GetById(int id)
        {
            return context.orderItems.Where(c => c.OrderItemId == id).FirstOrDefault();
        }

        public List<OrderItem> GetByOrderId(int orderId, FilterModel filter)
        {
            return context.orderItems.Where(c => c.OrderId == orderId).Skip((filter.PageNumber - 1) * filter.PageSize)
        .Take(filter.PageSize).ToList();
        }

        public bool save()
        {
            var save = context.SaveChanges();
            return save > 0 ? true : false;
        }

        public bool UpdateOrderItem(OrderItem orderItem)
        {
            context.Update(orderItem);
            return save();
        }
    }
}
