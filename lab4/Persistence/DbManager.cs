using lab4.Persistence.Schemas;

namespace lab4.Persistence
{
    public class DbManager {
        public List<User> AllUsers() {
            return DbCtx.Users.ToList();
        }

        //public bool IsUser(User user) {
        //    return DbCtx.Users.Select(u => u.UserName == user.UserName).Any();
        //}

        public User GetUser(User user) {
            return DbCtx.Users.Find(user);
        }

        public User AddUser(User user) {
            return DbCtx.Users.Add(user).Entity;
        }

        private DbCtx DbCtx = new DbCtx();
    }
}