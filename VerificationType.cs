using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IrynaSkurkoOptoolProgram
{
    class VerificationType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int verification_type_id { get; set; }
        public string verification_type { get; set; }

        public VerificationType() { }
        public VerificationType(string veriication_type)
        {
            this.verification_type = verification_type;
        }
    }
}
