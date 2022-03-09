using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IrynaSkurkoOptoolProgram
{
    class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int client_id { get; set; }
        public string first_name { get; set; }
        public string second_name { get; set; }
        public string last_name { get; set; }
        public int city_code { get; set; }
        public string date_of_birth { get; set; }

        public Client() { }
        public Client(string first_name, string second_name, string last_name, int city_code=-1, string date_of_birth=null)
        {
            this.first_name = first_name;
            this.second_name = second_name;
            this.last_name = last_name;
            this.city_code = city_code;
            this.date_of_birth = date_of_birth;
        }
    }
}
