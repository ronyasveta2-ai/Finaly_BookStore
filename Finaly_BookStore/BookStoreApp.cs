using Library_BookStore.Model;
using Library_BookStore.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finaly_BookStore
{
    public class BookStoreApp
    {
        private readonly BookStoreContext _db;
        private readonly UserService _userService;

        public BookStoreApp()
        {
            _db = new BookStoreContext();
            _userService = new UserService(_db);
        }

        public void Start()
        {
            var loginMenu = new LoginMenu(_userService);
            var user = loginMenu.Start();

            if (user == null)
                return;

            if (_userService.IsAdmin(user))
            {
                var adminMenu = new MainMenuAdmin(_db);
                adminMenu.Start();
            }
            else
            {
                var customerMenu = new MainMenuCustomer(_db, user);
                customerMenu.Start();
            }
        }
    }
}
