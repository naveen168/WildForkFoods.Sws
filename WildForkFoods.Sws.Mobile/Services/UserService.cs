using WildForkFoods.Sws.Mobile.Models;

namespace WildForkFoods.Sws.Mobile.Services
{
    public class UserService
    {
        private readonly List<User> _users = new List<User>()
        {
            new User { UserName = "admin", Name = "Sarah Mitchell", Email = "sarah.mitchell@wildforkfoods.com", Role = "Administrator", StoreId = "" },
            new User { UserName = "sb_manager", Name = "Michael Chen", Email = "michael.chen@jbssacontractor.com", Role = "Store Manager", StoreId = "1094" },
            new User { UserName = "sb_associate1", Name = "Jessica Thompson", Email = "jessica.thompson@wildforkfoods.com", Role = "Store Associate", StoreId = "1100" },
            new User { UserName = "sb_associate2", Name = "David Rodriguez", Email = "david.rodriguez@wildforkfoods.com", Role = "Store Associate", StoreId = "1099" },
        };

        public Task<User?> GetByUserNameAsync(string userName)
        {
            var p = _users.FirstOrDefault(x => x.UserName == userName);
            return Task.FromResult(p);
        }
    }
}
