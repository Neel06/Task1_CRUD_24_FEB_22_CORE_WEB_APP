using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task1_CRUD_24_FEB_22_CORE_WEB_APP.Models;

namespace Task1_CRUD_24_FEB_22_CORE_WEB_APP.Interface
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUser();
        void AddUser(User user);
        void EditUser(User user);
        User GetUserById(int id);

    }
}
