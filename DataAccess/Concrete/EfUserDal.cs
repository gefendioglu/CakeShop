using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete
{
    public class EfUserDal : EFEntityRepositoryBase<User, ApplicationDbContext>, IUserDal
    {

        public List<OperationClaim> GetAllClaims()
        {
            using (var db = new ApplicationDbContext())
            {
                return db.OperationClaims.ToList();
            }
        }

        public List<OperationClaim> GetClaims(User user)
        {
            using (var db = new ApplicationDbContext())
            {

                var result = from oc in db.OperationClaims
                             join uoc in db.UserOperationClaims on oc.Id equals uoc.OperationClaimId
                             where uoc.UserId == user.Id
                             select new OperationClaim { Id = oc.Id, Name = oc.Name };

                return result.ToList();

            }
        }

        public bool AddUserClaim(long userid, long claimid)
        {
            using (var db = new ApplicationDbContext())
            {
                db.UserOperationClaims.Add(new UserOperationClaim
                {
                    UserId = userid,
                    OperationClaimId = claimid
                });

                return db.SaveChanges() > 0 ? true : false;
            }
        }

        public bool RemoveUserClaim(long id)
        {
            using (var db = new ApplicationDbContext())
            {
                var uoc = db.UserOperationClaims.Find(id);
                if (uoc != null)
                    db.UserOperationClaims.Remove(uoc);
                return db.SaveChanges() > 0 ? true : false;

            }
        }
    }
}
