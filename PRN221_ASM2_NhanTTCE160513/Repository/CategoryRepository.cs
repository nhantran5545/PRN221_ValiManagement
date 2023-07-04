using PRN221_ASM2.Models;

namespace PRN221_ASM2.Repository
{
    public class CategoryRepository : ICategory
    {
        private readonly QlbanVaLiContext _context;

        public CategoryRepository(QlbanVaLiContext context)
        {
            _context = context;
        }

        public TLoaiSp Add(TLoaiSp loaiSp)
        {
            _context.TLoaiSps.Add(loaiSp);
            _context.SaveChanges();
            return loaiSp;
        }

        public TLoaiSp Delete(String maloaiSp)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TLoaiSp> GetAllCategory()
        {
            return _context.TLoaiSps;
        }

        public TLoaiSp GetCategory(String maloaiSp)
        {
            return _context.TLoaiSps.Find(maloaiSp);
        }

        public TLoaiSp Update(TLoaiSp loaiSp)
        {
            _context.Update(loaiSp);
            _context.SaveChanges();
            return loaiSp;
        }
    }
}
