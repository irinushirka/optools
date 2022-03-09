using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IrynaSkurkoOptoolProgram
{
    class Operator
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int operator_id { get; set; }
        public string first_name { get; set; }
        public string second_name { get; set; }
        public string last_name { get; set; }
        public string accumulated_tenure { get; set; }
        public int account_id { get; set; }

        public Operator() { }
        public Operator(int account_id, string first_name, string second_name, string last_name, string accumulated_tenure = "")
        {
            this.account_id = account_id;
            this.first_name = first_name;
            this.second_name = second_name;
            this.last_name = last_name;
            this.accumulated_tenure = accumulated_tenure;
        }
    }
}
