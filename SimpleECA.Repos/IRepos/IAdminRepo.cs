using SimpleECA.Models.Admin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleECA.Repos
{
    public interface IAdminRepo
    {
        Task<bool> CreateBrand(BrandViewModel model);
        Task<BrandViewModel> GetBrandById(int brandid);
    }
}
