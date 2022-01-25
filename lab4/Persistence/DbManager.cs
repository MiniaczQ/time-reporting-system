using lab4.Persistence.Schemas;

namespace lab4.Persistence
{
    public class DbManager
    {
        public List<User> AllUsers()
        {
            return DbCtx.Users.ToList();
        }

        public bool IsUser(User user)
        {
            return DbCtx.Users.Select(u => u.UserName == user.UserName).Any();
        }

        public User GetUser(string userName)
        {
            return DbCtx.Users.Find(userName);
        }

        public void AddUser(User user)
        {
            DbCtx.Users.Add(user);
            DbCtx.SaveChanges();
        }

        private DbCtx DbCtx = new DbCtx();
    }
}