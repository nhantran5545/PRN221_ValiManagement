﻿using System;
using System.Collections.Generic;

namespace PRN221_ASM2.Models;

public partial class TKichThuoc
{
    public string MaKichThuoc { get; set; } = null!;

    public string? KichThuoc { get; set; }

    public virtual ICollection<TChiTietSanPham> TChiTietSanPhams { get; set; } = new List<TChiTietSanPham>();
}
