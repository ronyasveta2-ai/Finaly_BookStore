using Library_BookStore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library_BookStore.Service
{
    public class UserService
    {
        private readonly BookStoreContext _db;

        public UserService(BookStoreContext db)
        {
            _db = db;
        }

        // LOGIN
        public User? Login(string userName, string password)
        {
            return _db.Users
                .FirstOrDefault(u => u.Username == userName && u.Password == password);
        }

        // CHECK IF ADMIN (login == "admin")
        public bool IsAdmin(User user)
        {
            return user != null && user.Username.ToLower() == "admin";
        }
    }
}
