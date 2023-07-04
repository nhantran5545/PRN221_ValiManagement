using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PRN221_ASM2.Models;

namespace PRN221_ASM2.Controllers
{
    public class AccessController : Controller
    {
        QlbanVaLiContext db = new QlbanVaLiContext();

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public IActionResult Login(TUser user)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                var u = db.TUsers.Where(x => x.Username.Equals(user.Username) && x.Password.Equals
                (user.Password)).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("UserName", u.Username.ToString());
                    var loaiUser = u.LoaiUser;
                    if (loaiUser == 1) // Kiểm tra nếu là admin
                    {
                        return RedirectToAction("Index", "HomeAdmin", new { area = "admin" }); // Chuyển hướng đến trang admin
                    }

                    return RedirectToAction("Index", "Home"); // Chuyển hướng đến trang chính nếu không phải admin
                } else
                {
                    ModelState.AddModelError(string.Empty, "User is not exists");
                }

            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Login", "Access");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(TUser user)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem người dùng đã tồn tại trong CSDL chưa
                var existingUser = db.TUsers.FirstOrDefault(x => x.Username.Equals(user.Username));
                if (existingUser == null)
                {

                    // Gán loại người dùng mặc định là 0 (người dùng)
                    user.LoaiUser = 0;

                    // Lưu người dùng mới vào CSDL
                    db.TUsers.Add(user);
                    db.SaveChanges();

                    // Chuyển hướng đến trang đăng nhập
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Username already exists");
                }
            }

            return View(user);
        }

    }
}
