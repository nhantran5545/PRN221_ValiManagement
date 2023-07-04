using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRN221_ASM2.Models;
using PRN221_ASM2.Models.Authentication;
using X.PagedList;

namespace PRN221_ASM2.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Authentication]

    [Route("admin/homeadmin")]
    public class HomeAdminController : Controller
    {
        QlbanVaLiContext db = new QlbanVaLiContext();

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("danhmucsanpham")]
        public IActionResult DanhMucSanPham(string searchString, int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var query = db.TDanhMucSps.AsNoTracking();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(x => x.TenSp.Contains(searchString));
            }

            var lstSanpham = query.OrderBy(x => x.TenSp);
            PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(lstSanpham, pageNumber, pageSize);

            ViewBag.SearchString = searchString; // Pass the searchString to the view

            return View(lst);
        }
        

        /*--------------------------Them San Pham Moi----------------------------*/
        [Route("AddProduct")]
        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewBag.MaChatLieu = new SelectList(db.TChatLieus.ToList(), "MaChatLieu", "ChatLieu");
            ViewBag.MaHangSx = new SelectList(db.THangSxes.ToList(), "MaHangSx", "HangSx");
            ViewBag.MaNuocSx = new SelectList(db.TQuocGia.ToList(), "MaNuoc", "TenNuoc");
            ViewBag.MaLoai = new SelectList(db.TLoaiSps.ToList(), "MaLoai", "Loai");
            ViewBag.MaDt = new SelectList(db.TLoaiDts.ToList(), "MaDt", "TenLoai");
            return View();
        }
        [Route("AddProduct")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProduct(TDanhMucSp sanPham)
        {
            if(ModelState.IsValid)
            {
                db.TDanhMucSps.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham");
            }
            return View(sanPham);
        }
        /*--------------------------Sua San Pham----------------------------*/
        [Route("EditProduct")]
        [HttpGet]
        public IActionResult EditProduct( string maSanPham)
        {
            ViewBag.MaChatLieu = new SelectList(db.TChatLieus.ToList(), "MaChatLieu", "ChatLieu");
            ViewBag.MaHangSx = new SelectList(db.THangSxes.ToList(), "MaHangSx", "HangSx");
            ViewBag.MaNuocSx = new SelectList(db.TQuocGia.ToList(), "MaNuoc", "TenNuoc");
            ViewBag.MaLoai = new SelectList(db.TLoaiSps.ToList(), "MaLoai", "Loai");
            ViewBag.MaDt = new SelectList(db.TLoaiDts.ToList(), "MaDt", "TenLoai");
            var sanPham = db.TDanhMucSps.Find(maSanPham);
            return View(sanPham);
        }
        [Route("EditProduct")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProduct(TDanhMucSp sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Update(sanPham);
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham", "HomeAdmin");
            }
            return View(sanPham);
        }

        /*--------------------------Xoa San Pham----------------------------*/
        [Route("DeleteProduct ")]
        [HttpGet]
        public IActionResult DeleteProduct(string maSanPham)
        {
            TempData["Message"] = "";
            var chiTietSanPhams = db.TChiTietSanPhams.Where(x => x.MaSp == maSanPham).ToList();
            if(chiTietSanPhams.Count() >0)
            {
                TempData["Message"] = "Không xóa được sản phẩm này";
                return RedirectToAction("DanhMucSanPham", "HomeAdmin");
            }
            var anhSamPhams = db.TAnhSps.Where(x => x.MaSp == maSanPham);
            if (anhSamPhams.Any()) db.RemoveRange(anhSamPhams);
            db.Remove(db.TDanhMucSps.Find(maSanPham));
            db.SaveChanges();
            TempData["Message"] = "Sản phẩm đã được xóa";
            return RedirectToAction("DanhMucSanPham", "HomeAdmin");
        }

        [Route("SearchProduct")]
        [HttpGet]
        public IActionResult SearchProduct(string searchString, int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var query = db.TDanhMucSps.AsNoTracking();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(x => x.TenSp.Contains(searchString));
            }

            var lstSanpham = query.OrderBy(x => x.TenSp);
            PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(lstSanpham, pageNumber, pageSize);

            ViewBag.SearchString = searchString; // Pass the searchString to the view

            return View("DanhMucSanPham", lst); // Return the same view as the original one
        }

        /*---------------------------------------------------Danh mục khách hàng---------------------------------------------------------------*/

        [Route("danhmuckhachhang")]
        public IActionResult DanhMucKhachHang(string searchString, int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var query = db.TKhachHangs.AsNoTracking();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(x => x.TenKhachHang.Contains(searchString));
            }

            var lstKhachHangs = query.OrderBy(x => x.TenKhachHang);
            PagedList<TKhachHang> lst = new PagedList<TKhachHang>(lstKhachHangs, pageNumber, pageSize);

            ViewBag.SearchString = searchString; // Pass the searchString to the view

            return View(lst);
        }
        /*--------------------------Them Khach Hang---------------------------------*/
        [Route("AddCustomer")]
        [HttpGet]
        public IActionResult AddCustomer()
        {
            ViewBag.Username = new SelectList(db.TUsers.ToList(), "Username", "Username");
            return View();
        }
        [Route("AddCustomer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCustomer(TKhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                db.TKhachHangs.Add(khachHang);
                db.SaveChanges();
                return RedirectToAction("DanhMucKhachHang");
            }
            return View(khachHang);
        }
        /*--------------------------Sua Khach Hang---------------------------------*/
        [Route("EditCustomer")]
        [HttpGet]
        public IActionResult EditCustomer(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return NotFound();
            }

            var khachHang = db.TKhachHangs.FirstOrDefault(x => x.Username == username);
            if (khachHang == null)
            {
                return NotFound();
            }

            ViewBag.Username = new SelectList(db.TUsers.ToList(), "Username", "Username");
            return View(khachHang);
        }

        [Route("EditCustomer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCustomer(string username, TKhachHang khachHang)
        {
            if (username != khachHang.Username)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                db.Update(khachHang);
                db.SaveChanges();
                return RedirectToAction("DanhMucKhachHang");
            }

            ViewBag.Username = new SelectList(db.TUsers.ToList(), "Username", "Username");
            return View(khachHang);
        }
        /*--------------------------Xoa Khach Hang---------------------------------*/

        [Route("DeleteCustomer")]
        [HttpGet]
        public IActionResult DeleteCustomer(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return NotFound();
            }

            var khachHang = db.TKhachHangs.FirstOrDefault(x => x.Username == username);
            if (khachHang == null)
            {
                return NotFound();
            }

            db.Remove(khachHang);
            db.SaveChanges();
            return RedirectToAction("DanhMucKhachHang");
        }




    }
}
