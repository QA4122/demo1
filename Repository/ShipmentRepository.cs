using Shopdemo1.Data;
using Shopdemo1.Models;

namespace Shopdemo1.Repository
{
    public interface IShipmentRepository
    {
        Shipment GetById(int id);  // Lấy thông tin giao hàng theo mã
        dynamic GetAll(FilterModel filter);  // Lấy tất cả thông tin giao hàng
        List<Shipment> GetByCustomer(int id, FilterModel filter);
        bool AddShipment(Shipment shipment);  // Thêm thông tin giao hàng mới
        bool UpdateShipment(Shipment shipment);  // Cập nhật thông tin giao hàng
        bool DeleteShipment(int id);  // Xóa thông tin giao hàng theo mã
        bool Save();
    }
    public class ShipmentRepository : IShipmentRepository
    {
        private readonly DataContext context;

        public ShipmentRepository(DataContext context)
        {
            this.context = context;
        }
        public bool AddShipment(Shipment shipment)
        {
            context.shipments.Add(shipment);
            return Save();
        }

        public bool DeleteShipment(int id)
        {
            var i = context.shipments.Where(c=>c.ShipmentId == id).FirstOrDefault();
            context.shipments.Remove(i);
            return Save();
        }

        public dynamic GetAll(FilterModel filter)
        {
            return context.shipments.Skip((filter.PageNumber - 1) * filter.PageSize)
        .Take(filter.PageSize).ToList();
        }

        public List<Shipment> GetByCustomer(int id, FilterModel filter)
        {
            return context.shipments.Where(c => c.customer_id == id).Skip((filter.PageNumber - 1) * filter.PageSize)
        .Take(filter.PageSize).ToList();
        }

        public Shipment GetById(int id)
        {
            return context.shipments.Where(c => c.ShipmentId == id).FirstOrDefault();
        }

        public bool Save()
        {
            var save = context.SaveChanges();
            return save > 0? true: false;
        }

        public bool UpdateShipment(Shipment shipment)
        {
            context.shipments.Update(shipment);
            return Save();
        }
    }
}
