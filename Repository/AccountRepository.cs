using Shopdemo1.Data;
using Shopdemo1.Models;

namespace Shopdemo1.Repository
{
    public interface IAccoutRepository
    {
        public dynamic GetAll(FilterModel model);
        public Account GetByUsername(string username);
        public bool AddAccount(Account account);
        public bool UpdateAccount(Account account, string username);
        public bool DeleteAccount(string username);
        public bool Save();
    }
    public class AccountRepository : IAccoutRepository
    {
        private readonly DataContext applicationDbContext;

        public AccountRepository(DataContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public bool AddAccount(Account account)
        {
            applicationDbContext.accounts.Add(account);
            return Save();
        }

        public bool DeleteAccount(string username)
        {
            var account = applicationDbContext.accounts.Where(c => c.Username == username).FirstOrDefault();
            applicationDbContext.Remove(account);
            return Save();
        }

        public dynamic GetAll(FilterModel model)
        {
            var accounts = applicationDbContext.accounts.ToList();
            var profile = applicationDbContext.profiles.ToList();
            List<dynamic> prof_account = new List<dynamic>();
            foreach (var i in accounts)
            {
                var p = applicationDbContext.profiles.Where(c => c.AccountId == i.Id).FirstOrDefault();
                if (p == null)
                {
                    var temp = new
                    {
                        userName = i.Username,
                        user = "",
                        type = i.Type,
                    };
                    prof_account.Add(temp);
                }
                else
                {
                    var temp = new
                    {
                        userName = i.Username,
                        user = p.FullName,
                        type = i.Type,
                    };
                    prof_account.Add(temp);
                }
            }
            if (model.SearchString != null)
            {
                prof_account = prof_account.Where(c => c.userName.Contains(model.SearchString) || c.user.Contains(model.SearchString)).ToList();
            }
            int length = prof_account.Count();
            prof_account = prof_account.Skip((model.PageNumber - 1) * model.PageSize).Take(model.PageSize).ToList();
            return new
            {
                data = prof_account,
                length = length,
            };
        }

        public Account GetByUsername(string username)
        {
            return applicationDbContext.accounts.Where(c => c.Username == username).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = applicationDbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateAccount(Account account, string username)
        {
            account.Username = username;
            applicationDbContext.Update(account);
            return Save();
        }
    }
}
