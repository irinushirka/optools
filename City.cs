using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IrynaSkurkoOptoolProgram
{
    class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int city_code { get; set; }
        public string city { get; set; }

        public City() { }
        public City(string city)
        {
            this.city = city;
        }
    }
}
