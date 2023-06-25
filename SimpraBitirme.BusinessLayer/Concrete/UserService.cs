using AutoMapper;
using SimpraBitirme.BusinessLayer.Abstract;
using SimpraBitirme.BusinessLayer.Response.BaseResponse;
using SimpraBitirme.DataAccessLayer.Abstract;
using SimpraBitirme.EntityLayer.Concrete;
using SimpraBitirme.EntityLayer.Dto.Authenticate;
using SimpraBitirme.EntityLayer.Dto.User;
using System.Security.Cryptography;
using System.Text;
using System.Transactions;

namespace SimpraBitirme.BusinessLayer.Concrete
{
    public class UserService : IUserService
    {
        IUserDal _userDal;
        IBasketDal _basketDal;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessorService _httpContextAccessor;

        public UserService(IUserDal userDal, IMapper mapper, IBasketDal basketDal, IHttpContextAccessorService httpContextAccessorService)
        {
            _userDal = userDal;
            _mapper = mapper;
            _basketDal = basketDal;
            _httpContextAccessor = httpContextAccessorService;
        }
        private static string HashPassword(string password)
        {
            byte[] cardBytes = Encoding.UTF8.GetBytes(password);

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(cardBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
        public ApiResponse Add(UserRequest userRequest)
        {
            ApiResponse apiResponse = new ApiResponse();
            apiResponse.Success = false;

            if (userRequest == null)
            {
                apiResponse.Message = "İşlem sırasında bir hata meydana gelmiştir.";
                return apiResponse;
            }

            var anyUser = _userDal.Find(x => x.Email == userRequest.Email);

            if(anyUser != null)
            {
                apiResponse.Message = "Böyle bir kullanıcı zaten mevcut";
                return apiResponse;
            }

            userRequest.Password = HashPassword(userRequest.Password);
            var user = _mapper.Map<User>(userRequest);
            user.PointBalance = 0;
            using (var transaction = new TransactionScope())
            {
                try
                {
                    var response = _userDal.InsertIdResponse(user);
                    if (response < 1)
                    {
                        apiResponse.Message = "İşlem sırasında bir hata meydana gelmiştir.";
                        return apiResponse;
                    }
                    Basket basket = new Basket();
                    basket.UserId = response;
                    var basketResponse = _basketDal.Insert(basket);
                    if (basketResponse > 0)
                    {
                        apiResponse.Message = "Kullanıcı başarılı bir şekilde eklenmiştir.";
                        apiResponse.Success = true;
                        transaction.Complete();
                        return apiResponse;
                    }
                }
                catch
                {
                    transaction.Dispose();            
                }
                apiResponse.Message = "İşlem sırasında bir hata meydana gelmiştir.";
                return apiResponse;
            }
    
        }


        public ApiResponse UpdateUserPoint(PointBalanceRequest pointBalanceRequest)
        {
            var apiResponse = new ApiResponse();
            apiResponse.Success = false;

            var response = _userDal.Find(x=>x.Id == pointBalanceRequest.Id);
            if(response == null)
            {
                apiResponse.Message = "Böyle bir kullanıcı bulunamamıştır.";
                return apiResponse;
            }

            response.PointBalance = response.PointBalance + pointBalanceRequest.PointBalance;
            var updateResponse = _userDal.Update(response);
            if(updateResponse > 0)
            {
                apiResponse.Message = "Puan başarılı bir şekilde eklenmiştir.";
                apiResponse.Success = true;
                return apiResponse;
            }

            apiResponse.Message = "İşlem sırasında bir hata meydana gelmiştir.";
            return apiResponse;
        }
        public ApiResponse<double> UseUserPoint(PointBalanceRequest pointBalanceRequest)
        {
            ApiResponse<double> apiResponse = new ApiResponse<double>(0);
            apiResponse.Success = false;

            var response = _userDal.Find(x => x.Id == pointBalanceRequest.Id);
            if (response == null)
            {
                apiResponse.Message = "Böyle bir kullanıcı bulunamamıştır.";
                apiResponse.Response = 0;
                return apiResponse;
            }

            if(pointBalanceRequest.PointBalance <= response.PointBalance)
            {
                response.PointBalance = response.PointBalance - pointBalanceRequest.PointBalance;
                pointBalanceRequest.PointBalance = 0;
            }
            else
            {
                pointBalanceRequest.PointBalance = pointBalanceRequest.PointBalance - response.PointBalance;
                response.PointBalance = 0;
            }

            var updateResponse = _userDal.Update(response);
            if(updateResponse > 0)
            {
                apiResponse.Message = "İşlem başarılı bir şekilde gerçekleştirilmiştir.";
                apiResponse.Response = pointBalanceRequest.PointBalance;
                apiResponse.Success = true;
                return apiResponse;
            }
            
            apiResponse.Message = "İşlem sırasında bir hata meydana gelmiştir.";
            return apiResponse;
        }

        public bool Delete(int id)
        {
            var response = _userDal.Find(x => x.Id == id);
            response.IsDelete = true;
            var deleteResponse = _userDal.Update(response);
            if(deleteResponse > 0)
            {
                return true;
            }
            return false;
        }

        public UserResponse GetByID(int id)
        {
            var user = _userDal.Find(x => x.Id == id);
            var mappedUser = _mapper.Map<UserResponse>(user);
            return mappedUser;
        }

        public ApiResponse<UserToken> Login(AuthenticateRequest user)
        {
            var users = _userDal.Find(x => x.Email == user.Email);

            if (users == null)
                return new ApiResponse<UserToken>("Böyle bir kullanıcı bulunamadı. Email adresinizi veya şifrenizi kontrol ederek tekrardan deneyiniz.");

            var checkPassword = HashPassword(user.Password);

            if(users.Password != checkPassword)
            {
                return new ApiResponse<UserToken>("Lütfen kullanıcı adınızı ve şifrenizi kontrol ediniz.");
            }

            return new ApiResponse<UserToken>(_mapper.Map<UserToken>(users));
        }

        public ApiResponse Update(UserRequest userRequest)
        {
            throw new NotImplementedException();
        }

        public UserToken GetUserTokenByID(int id)
        {
            var user = _userDal.Find(x => x.Id == id);
            var mapped = _mapper.Map<UserToken>(user);
            return mapped;
        }
    }
}