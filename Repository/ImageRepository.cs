using Microsoft.EntityFrameworkCore;
using Shopdemo1.Data;
using Shopdemo1.Models;

namespace Shopdemo1.Repository
{
    public interface IImageRepository
    {
        public List<Image> GetAll();
        public Image GetById(int id);
        public int CountImage(string ProductCode);
        public List<Image> GetByProduct(string productCode);
        public void SaveImage(IFormFile file, string name);
        public void DeleteImage(string name);
        public bool Addimage(Image image);
        public bool UpdateImage(Image image, int id);
        public bool DeleteImage(int id);
        public bool Save();
    }
    public class ImageRepository : IImageRepository
    {
        private readonly DataContext dbContext;
        private string TargetFolder = "D:\\bandemoDA\\MediaDemo2\\ProductImage\\";
        public ImageRepository(DataContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public bool Addimage(Image image)
        {
            dbContext.images.Add(image);
            return Save();
        }

        public int CountImage(string ProductCode)
        {
            return dbContext.images.Where(c => c.ProductCode == ProductCode).AsNoTracking().Count();
        }

        public bool DeleteImage(int id)
        {
            var image = dbContext.images.Where(c => c.Id == id).FirstOrDefault();
            dbContext.images.Remove(image);
            return Save();
        }

        public void DeleteImage(string name)
        {
            File.Delete(TargetFolder + name);
        }

        public List<Image> GetAll()
        {
            return dbContext.images.AsNoTracking().ToList();
        }

        public Image GetById(int id)
        {
            return dbContext.images.Where(c => c.Id == id).AsNoTracking().FirstOrDefault();
        }

        public List<Image> GetByProduct(string productCode)
        {
            return dbContext.images.Where(c => c.ProductCode == productCode).AsNoTracking().ToList();
        }

        public bool Save()
        {
            var saved = dbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async void SaveImage(IFormFile file, string name)
        {
            try
            {
                using (Stream fileStream = new FileStream(TargetFolder + name, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        public bool UpdateImage(Image image, int id)
        {
            image.Id = id;
            dbContext.images.Update(image);
            return Save();
        }
    }
}
