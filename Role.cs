using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IrynaSkurkoOptoolProgram
{
    class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int role_id { get; set; }
        public string role_name { get; set; }

        public Role() { }
        public Role(string role_name)
        {
            this.role_name = role_name;
        }
    }
}
