using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IrynaSkurkoOptoolProgram
{
    class DocumentType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int document_type_id { get; set; }
        public string document_type { get; set; }

        public DocumentType() { }
        public DocumentType(string document_type)
        {
            this.document_type = document_type;
        }
    }
}
