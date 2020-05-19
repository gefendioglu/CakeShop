using Business.Abstract;
using Core.Constants;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AuthService : IAuthService
    {
        IUserService _userService;
        ITokenHelper _tokenHelper;
        public AuthService(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<User> Login(LoginDto user)
        {
            var userCheck = _userService.GetByMail(user.UserName);
            if (userCheck == null)
                return new DataResult<User>(null, false, Messages.UserNotFound);

            var passwordCheck = HashingHelper.VerifyPasswordHash(user.Password, userCheck.Data.PasswordHash, userCheck.Data.PasswordSalt);
            if (passwordCheck == false)
                return new DataResult<User>(null, false, Messages.PasswordError);

            return new DataResult<User>(userCheck.Data, true, Messages.LoginsSuccesfull);
        }

        public IDataResult<User> Register(RegisterDto registerDto)
        {
            if (UserExists(registerDto.Email).Success == false)
                return new DataResult<User>(null, false, Messages.UserAllreadyExists);

            HashingHelper.CreatePasswordHash(registerDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            User user = new User
            {
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            return _userService.Add(user);
        }

        public IResult UserExists(string email)
        {
            var userCheck = _userService.GetByMail(email);
            if (userCheck.Success == true)
                return new Result(false, Messages.UserAllreadyExists);
            return new Result(true);
        }

        public IDataResult<AccesToken> CreateAccessToken(User user)
        {
            var operationClaims = _userService.GetUserClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, operationClaims);

            return new DataResult<AccesToken>(accessToken, true, Messages.TokenCreated);

        }




    }
}
