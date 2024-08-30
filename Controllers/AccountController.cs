using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Shopdemo1.Models;
using Shopdemo1.Repository;
namespace Shopdemo1.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccoutRepository _accoutRepository;
        private readonly IProfileReository _profileReository;

        public AccountController(IAccoutRepository accoutRepository, IProfileReository profileReository)
        {
            _accoutRepository = accoutRepository;
            _profileReository = profileReository;
        }
        [HttpPost("add-account")]
        public IActionResult AddAccount(Account account)
        {
            var rs = _accoutRepository.AddAccount(account);
            return Ok(new
            {
                response_code = 00,
                response_data = rs
            });
        }
        [HttpPost("get-all")]
        public IActionResult GetAll(FilterModel filter)
        {
            var rs = _accoutRepository.GetAll(filter);
            return Ok(new
            {
                response_code = 00,
                response_data = rs
            });
        }
        [HttpGet("get-by-username")]
        public IActionResult GetbyUserName(string userName)
        {
            var rs = _accoutRepository.GetByUsername(userName);
            return Ok(new
            {
                response_code = 00,
                response_data = rs
            });
        }
        [HttpPost("edit-account")]
        public IActionResult EditAccount(Account account)
        {
            var edit = _accoutRepository.UpdateAccount(account, account.Username);
            if (edit)
            {
                return Ok(new
                {
                    response_code = 200,
                    response_data = ""
                });
            }
            return Ok(new
            {
                response_code = 300,
                response_data = ""
            });
        }
        [HttpPost("login")]
        public IActionResult Login(Account account)
        {
            var acc = _accoutRepository.GetByUsername(account.Username);
            if (acc == null)
                return Ok(new
                {
                    response_code = 00,
                    response_data = "Tên đăng nhập đã tồn tại"
                });
            if (acc.Password == account.Password)
                return Ok(new
                {
                    response_code = 200,
                    response_data = "Thành công"
                });
            return Ok(new
            {
                response_code = 00,
                response_data = "Sai mật khẩu"
            });
        }
        [HttpPost("sign-up")]
        public IActionResult SignUp(Account account)
        {
            var acc = _accoutRepository.GetByUsername(account.Username);
            if (acc == null)
            {
                var add = _accoutRepository.AddAccount(account);
                if (!add)
                    return Ok(new
                    {
                        response_code = 00,
                        response_data = "Đăng ký không thành công"
                    });
                return Ok(new
                {
                    response_code = 200,
                    resposen_data = "Đăng ký thành công"
                });
            }
            return Ok(new
            {
                response_code = 300,
                response_data = "Tài khoản đã tôn tại"
            });
        }
        public class changePassword
        {
            public string newP { get; set; }
            public string oldP { get; set; }
        }
        [HttpPost("change-password")]
        public IActionResult ChangePassword(changePassword change, string username)
        {
            var acc = _accoutRepository.GetByUsername(username);
            if(acc != null)
            {
                if (acc.Password != change.oldP)
                    return Ok(new
                    {
                        response_code = 300,
                        response_data = "Sai mật khẩu cũ"
                    });
                acc.Password = change.newP;
                var add = _accoutRepository.UpdateAccount(acc, username);
                if (!add)
                    return Ok(new
                    {
                        response_code = 00,
                        response_data = "Lỗi khi đổi mật khẩu"
                    });
                return Ok(new
                {
                    response_code = 200,
                    response_data = "Đổi mật khẩu thành công"
                });
            }
            return Ok(new
            {
                response_code = 00,
                response_data = "Tài khoản không tồn tại"
            });
        }
        [HttpDelete("del-account")]
        public IActionResult DeleteAccount(string username)
        {
            var prof = _profileReository.GetByAcc(username);
            if (prof != null)
            {
                var del = _profileReository.Delete((int)prof.CustomerId);
            }
            var rs = _accoutRepository.DeleteAccount(username);
            if (rs)
            {
                return Ok(new
                {
                    response_code = 200,
                    response_data = "Xóa thành công"
                });
            }
            return Ok(new
            {
                response_code = 300,
                response_data = "Lỗi khi xóa tài khoản"
            });
        }
    }

}
