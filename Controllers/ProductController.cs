using Microsoft.AspNetCore.Mvc;
using Shopdemo1.Models;
using Shopdemo1.Repository;

namespace Shopdemo1.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;
       // private readonly ILogRepository logRepository;
        private readonly IImageRepository imageRepository;

        public ProductController(IProductRepository productRepository, IImageRepository imageRepository)
        {
            this.productRepository = productRepository;
           // this.logRepository = logRepository;
            this.imageRepository = imageRepository;
        }
        [HttpPost("all-product")]
        public IActionResult GetAll(FilterModel filter)
        {
            List<dynamic> rs = new List<dynamic>();
            var products = productRepository.GetAll(filter);
            foreach (var i in products.data)
            {
                string filePath = "D:\\bandemoDA\\MediaDemo2\\ProductImage\\Image_" + i.ProductCode + "_1.png";
                var content = new byte[0];
                if (System.IO.File.Exists(filePath))
                {
                    content = System.IO.File.ReadAllBytes(filePath);
                }
                //var imageSource = new ByteArrayContent(content);
                var temp = new
                {
                    productCode = i.ProductCode,
                    productName = i.ProductName,
                    decription = i.Description,
                    createDate = i.CreateDate,
                    price = i.Price,
                    warnAmount = i.WarnAmount,
                    stock = i.Stock,
                    categoryId = i.CategoryId,
                    imageSource = content.ToArray()
                };
                rs.Add(temp);
            }
            var obj = new
            {
                data = rs,
                length = products.length
            };
            return Ok(new
            {
                response_code = 200,
                response_data = obj
            });
        }
        
        
        [HttpGet("product-detail")]
        public IActionResult GetProductDetail(string ProductCode)
        {
            var i = productRepository.GetByProductCode(ProductCode);
            if (i == null)
            {
                return Ok(new
                {
                    response_code = 300
                });
            }
            string filePath = "D:\\bandemoDA\\MediaDemo2\\ProductImage\\Image_" + product.ProductCode + "_1.png";
            var content = new byte[0];
            if (System.IO.File.Exists(filePath))
            {
                content = System.IO.File.ReadAllBytes(filePath);
            }
            List<Image> ListImageSource = new List<Image>();
            ListImageSource = imageRepository.GetByProduct(ProductCode).ToList();
            List<dynamic> ImageByte = new List<dynamic>();
            foreach (var temp1 in ListImageSource)
            {
                string imagePath = "D:\\bandemoDA\\MediaDemo2\\ProductImage\\" + temp1.ImageName;
                var bytes = new byte[0];
                if (System.IO.File.Exists(imagePath))
                {
                    bytes = System.IO.File.ReadAllBytes(imagePath);
                }
                var image = new
                {
                    Id = temp1.Id,
                    bytes = bytes
                };
                ImageByte.Add(image);
            }
            int id = 0;
            if (ListImageSource.Count() != 0)
            {
                id = (int)ListImageSource.Select(c => c.Id).First();
            }
            dynamic baseImage = new
            {
                id = id,
                bytes = content.ToArray()
            };
            //var imageSource = new ByteArrayContent(content);
            var temp = new
            {
                productCode = i.ProductCode,
                productName = i.ProductName,
                decription = i.Description,
                createDate = i.CreateDate,
                price = i.Price,
                warnAmount = i.WarnAmount,
                stock = i.Stock,
                categoryId = i.CategoryId,
                imageSource = baseImage,
                allImage = ImageByte,
                imageCount = ImageByte.Count,
            };
            return Ok(new
            {
                response_code = 200,
                response_data = temp
            });
        }
        [HttpPost("add-product")]
        public IActionResult AddProduct(Product product, [FromHeader] string userName)
        {
            var checkExist = productRepository.GetByProductName(product.ProductName);
            if (checkExist == null)
            {
                if (product.Stock == 0)
                {
                    product.Stock = 1;
                }
                if (product.WarnAmount == 0)
                {
                    product.WarnAmount = 5;
                }
                if (product.Price == 0)
                {
                    product.Price = 1;
                }
                string productCode = "";
                productCode = "PROD" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day;
                product.CreateDate = DateTime.Now;
                int count = productRepository.GetSameDate();
                string countString = "";
                count++;
                if (count < 10)
                    countString += "00" + count;
                else if (count >= 10 && count < 100)
                    countString += "0" + count;
                else
                    countString += count;
                productCode += countString;
                product.ProductCode = productCode;
             /*   Log log = new Log();
                log.CreateDate = DateTime.Now;
                log.ProductCode = productCode;
                log.CreatedBy = userName;
                log.Type = "CREATE";
                log.Content = userName + " thêm sản phẩm " + productCode;*/
                var add = productRepository.Add(product);
               // var addlog = logRepository.AddLog(log);

                if (!add)
                    return Ok(new
                    {
                        response_code = 200,
                        response_data = "Có lỗi khi thêm sản phẩm"
                    });
                return Ok(new
                {
                    response_code = 200,
                    response_data = "Thêm sản phẩm thành công",
                    productCode = productCode
                });
            }
            return Ok(new
            {
                response_code = 00,
                response_data = "Sản phẩm đã tồn tại"
            });
        }
        [HttpPost("update-product")]
        public IActionResult UpdateProduct(Product product, [FromHeader] string userName)
        {
            var checkExist = productRepository.GetByProductCode(product.ProductCode);
            if (checkExist != null)
            {
                /*Log log = new Log();
                log.CreateDate = DateTime.Now;
                log.CreatedBy = userName;
                log.Type = "UPDATE";
                log.ProductCode = product.ProductCode;
                log.Content = userName + " sửa sản phẩm " + product.ProductCode;*/
                var update = productRepository.Update(product);
                //var addlog = logRepository.AddLog(log);
                if (!update)
                    return Ok(new
                    {
                        response_code = 00,
                        response_data = "Có lỗi khi sửa sản phẩm"
                    });
                return Ok(new
                {
                    response_code = 200,
                    response_data = "Sửa sản phẩm thành công"
                });
            }
            return Ok(new
            {
                response_code = 00,
                response_data = "Sản phẩm không tồn tại"
            });
        }
        public class Input
        {
            public string productCode { get; set; }
        }
       

        [HttpDelete("delete-product")]
        public IActionResult DeleteProduct(string productCode, [FromHeader] string Username)
        {
            var delete = productRepository.Delete(productCode);
           /* Log log = new Log();
            log.CreateDate = DateTime.Now;
            log.ProductCode = productCode;
            log.CreatedBy = Username;
            log.Type = "DELETE";
            log.Content = Username + " xóa sản phẩm " + productCode;
            var addlog = logRepository.AddLog(log);*/
            var images = imageRepository.GetByProduct(productCode);
            foreach (var i in images)
            {
                imageRepository.DeleteImage(i.ImageName);
                imageRepository.DeleteImage((int)i.Id);
            }
            if (!delete)
                return Ok(new
                {
                    response_code = 300,
                    response_data = "Có lỗi khi xóa sản phẩm"
                });
            return Ok(new
            {
                response_code = 200,
                response_data = "Xóa sản phẩm thành công"
            });
        }
    }
}
