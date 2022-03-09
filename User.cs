using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IrynaSkurkoOptoolProgram
{
    class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int user_id { get; set; }
        public string login { get; set; }
        public string password { get; set;}
        public string email { get; set;}
        public int is_blocked { get; set; }
        public int role_id { get; set; }

        public User() { }
        public User(string login, string password, int role_id, string email="", int is_blocked=1)
        {
            this.login = login;
            this.password = password;
            this.role_id = role_id;
            this.email = email;
            this.is_blocked = is_blocked;
        }
        public User(int user_id, string login, string password, int role_id, string email = "", int is_blocked = 1)
        {
            this.user_id = user_id;
            this.login = login;
            this.password = password;
            this.role_id = role_id;
            this.email = email;
            this.is_blocked = is_blocked;
        }
    }
}
