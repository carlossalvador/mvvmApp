using app.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Model
{
    class Users
    {
        public event EventHandler usersChanged;
        List<User> users { get; set; }
        UsersTable usersData = new UsersTable();

        public Users()
        {
            users = usersData.Select();
        }

        public List<User> All()
        {
            return users;
        }

        void OnUsersChanged()
        {
            if (usersChanged != null)
                usersChanged(this, null);
        }
    }
}
