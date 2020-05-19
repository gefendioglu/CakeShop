using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Login(LoginDto user);
        IDataResult<User> Register(RegisterDto registerDto);
        IResult UserExists(string email);
        IDataResult<AccesToken> CreateAccessToken(User user);

    }
}
