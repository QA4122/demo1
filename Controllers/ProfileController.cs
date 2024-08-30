using Microsoft.AspNetCore.Mvc;
using Shopdemo1.Models;
using Shopdemo1.Repository;

namespace Shopdemo1.Controllers
{
    [ApiController]
    [Route("api/profile")]
    public class ProfileController : Controller
    {
        private readonly IProfileReository profileRepository;

        public ProfileController(IProfileReository profile)
        {
            this.profileRepository = profile;
        }
        [HttpGet]
        [Route("get-by-username")]
        public IActionResult GetByUserName(string userName)
        {
            try
            {
                var prof = profileRepository.GetByAcc(userName);
                return Ok(new
                {
                    response_code = 200,
                    response_data = prof
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    response_code = 300,
                    response_data = new Profile(),
                });
            }
        }
        [HttpPost]
        [Route("edit-profile")]
        public IActionResult AddProfile(string userName, Profile profile)
        {
            var checkExists = profileRepository.GetByAcc(userName);
            if (checkExists == null)
            {
                var add = profileRepository.Add(profile, userName);
                if (!add)
                {
                    return Ok(new
                    {
                        response_code = 300,
                        response_data = "Lỗi khi thêm hồ sơ"
                    });
                }
            }
            else
            {
                var update = profileRepository.Update(profile);
                if (!update)
                {
                    return Ok(new
                    {
                        response_code = 300,
                        response_data = "Lỗi khi thêm hồ sơ"
                    });
                }
            }

            return Ok(new
            {
                response_code = 200,
                response_data = "Sửa hồ sơ thành công"
            });
        }
        [HttpDelete]
        [Route("delete-profile")]
        public IActionResult DeleteProfile(string userName)
        {
            var prof = profileRepository.GetByAcc(userName);
            var del = profileRepository.Delete((int)prof.CustomerId);
            if (!del)
            {
                return Ok(new
                {
                    response_code = 300,
                    response_data = "Lỗi khi xóa hồ sơ"
                });
            }
            return Ok(new
            {
                response_code = 200,
                response_data = "Xóa hồ sơ thành công"
            });
        }
    }
}
