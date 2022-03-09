using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrynaSkurkoOptoolProgram
{
    class UserViewModel
    {
        public int user_id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string email { get; set; }
        public string access { get; set; }

        public UserViewModel() { }
        public UserViewModel(User user)
        {
            this.user_id = user.user_id;
            this.login = user.login;
            this.password = user.password;
            if (user.role_id == 1)
                this.role = "Оператор";
            else
                this.role = "Администратор";
            this.email = user.email;
            if (user.is_blocked == 0)
                this.access = "Есть";
            else
                this.access = "Нет";
        }
    }
}
