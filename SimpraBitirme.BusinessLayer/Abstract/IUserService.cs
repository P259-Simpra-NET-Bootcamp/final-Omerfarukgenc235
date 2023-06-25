using SimpraBitirme.BusinessLayer.Response.BaseResponse;
using SimpraBitirme.EntityLayer.Concrete;
using SimpraBitirme.EntityLayer.Dto.Authenticate;
using SimpraBitirme.EntityLayer.Dto.Categories;
using SimpraBitirme.EntityLayer.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpraBitirme.BusinessLayer.Abstract
{
    public interface IUserService
    {
        ApiResponse Add(UserRequest userRequest);
        ApiResponse UpdateUserPoint(PointBalanceRequest pointBalanceRequest);
        ApiResponse<double> UseUserPoint(PointBalanceRequest pointBalanceRequest);

        UserResponse GetByID(int id);
        UserToken GetUserTokenByID(int id);
        ApiResponse Update(UserRequest userRequest);
        bool Delete(int id);
        ApiResponse<UserToken> Login(AuthenticateRequest user);
    }
}
