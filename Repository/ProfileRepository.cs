using Shopdemo1.Data;
using Shopdemo1.Models;

namespace Shopdemo1.Repository
{
    public interface IProfileReository
    {
        Profile GetById(int id);
        List<Profile> GetAll(FilterModel filter);
        bool Add(Profile profile, string username);
        bool Update(Profile profile);
        bool Delete(int id);
        bool Save();
        Profile GetByAcc(string username);

    }
    public class ProfileRepository : IProfileReository
    {
        private readonly DataContext context;

        public ProfileRepository(DataContext context)
        {
            this.context = context;
        }
        public bool Add(Profile profile, string u)
        {
            var account = context.accounts.Where(c => c.Username == u).FirstOrDefault();
            profile.AccountId = account.Id;
            context.profiles.Add(profile);
            return Save();
        }

        public bool Delete(int id)
        {
            var profile = context.profiles.Where(c => c.CustomerId == id).FirstOrDefault();
            if (profile != null)
            {
                context.profiles.Remove(profile);
            }
            return Save();
        }

        public List<Profile> GetAll(FilterModel filter)
        {
            return context.profiles.Skip((filter.PageNumber - 1) * filter.PageSize)
        .Take(filter.PageSize).ToList();
        }

        public Profile GetByAcc(string username)
        { var a = context.accounts.Where(c=>c.Username==username).FirstOrDefault();
            return context.profiles.Where(c => c.AccountId == a.Id).FirstOrDefault();
        }

        public Profile GetById(int id)
        {
            return context.profiles.Where(c => c.CustomerId == id).FirstOrDefault();
        }

        public bool Save()
        {
            var save = context.SaveChanges();
            return save > 0 ? true : false;
        }

        public bool Update(Profile profile)
        {
            context.profiles.Update(profile);
            return Save();
        }
    }
}
