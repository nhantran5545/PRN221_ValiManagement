using PRN221_ASM2.Models;

namespace PRN221_ASM2.Repository
{
    public interface ICategory
    {
        TLoaiSp Add(TLoaiSp loaiSp);

        TLoaiSp Update(TLoaiSp loaiSp); 
        TLoaiSp Delete(String maloaiSp); 
        TLoaiSp GetCategory(String maloaiSp);
        IEnumerable<TLoaiSp> GetAllCategory();  
    }
}
