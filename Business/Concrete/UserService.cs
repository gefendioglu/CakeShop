using Business.Abstract;
using Core.Constants;
using Core.Entities.Concrete;
using Core.Entities.Concrete.Enum;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserDal _userDal;

        private ApplicationDbContext _db;

        public UserService(IUserDal userDal)
        {
            _userDal = userDal;
            _db = new ApplicationDbContext();
        }

        public IDataResult<List<User>> GetActive()
        {
            var list = _db.Set<User>().Where(x => x.Status == Status.Active).ToList();
            return new DataResult<List<User>>(list, true);

        }

        public IDataResult<List<User>> GetAll()
        {
            var list = _userDal.GetList();
            return new DataResult<List<User>>(list, true);

        }

        public IDataResult<User> GetByDefault(Expression<Func<User, bool>> exp)
        {
            var getByDefault = _db.Set<User>().FirstOrDefault(exp);
            if (getByDefault != null)
                return new DataResult<User>(getByDefault, true);
            return new DataResult<User>(null, false);
        }

        public IDataResult<User> GetById(int id)
        {
            var user = _userDal.Get(i => i.Id == id);
            if (user != null)
                return new DataResult<User>(user, true, Messages.UserFound);
            return new DataResult<User>(null, false, Messages.UserNotFound);
        }

        public IDataResult<User> GetByMail(string mail)
        {
            var user = _userDal.Get(i => i.Email == mail);
            if (user != null)
                return new DataResult<User>(user, true, Messages.UserFound);
            return new DataResult<User>(null, false, Messages.UserNotFound);
        }

        public IDataResult<List<User>> GetDefault(Expression<Func<User, bool>> exp)
        {
            var getDefault = _db.Set<User>().Where(exp).ToList();
            if (getDefault != null)
                return new DataResult<List<User>>(getDefault, true);
            return new DataResult<List<User>>(null, false);
        }

        public List<OperationClaim> GetUserClaims(User user)
        {
            return _userDal.GetClaims(user);
        }


        public IDataResult<User> Add(User model)
        {
            _userDal.Add(model);
            return new DataResult<User>(model, true);
        }

        public IDataResult<User> Update(User model)
        {
            _userDal.Update(model);
            return new DataResult<User>(model, true);
        }

        public IDataResult<User> HardDelete(User model)
        {
            _userDal.Delete(model);
            return new DataResult<User>(model, true);
        }

        public IDataResult<User> SoftDelete(User model)
        {
            model.IsDeleted = true;
            model.DeletedDate = DateTime.Now;

            _userDal.Update(model);
            return new DataResult<User>(model, true);
        }

        public IDataResult<List<User>> DeleteAll(Expression<Func<User, bool>> exp)
        {
            var list = GetDefault(exp).Data;

            //return list.ForEach(item => {});

            foreach (var item in list)
            {
                item.Status = Status.Deleted;
                Update(item);
            }
            //TODO:
            return new DataResult<List<User>>(list, true);

        }


    }
}
