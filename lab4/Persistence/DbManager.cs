using lab4.Persistence.Schemas;

namespace lab4.Persistence
{
    public class DbManager {
        public List<User> allUser() {
            return DbCtx.Users.ToList();
        }

        public bool isUser(User user) {
            return DbCtx.Users.Select(u => u.UserName == user.UserName).Any();
        }

        public User addUser(User user) {
            return DbCtx.Users.Add(user).Entity;
        }

        private DbCtx DbCtx = new DbCtx();
    }
}