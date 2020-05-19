using Core.Entities.Concrete;
using Core.Service;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService : IBaseService<User>
    {
        IDataResult<User> GetByMail(string mail);

        // User Roles 
        List<OperationClaim> GetUserClaims(User user);
    }
}
