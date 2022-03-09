using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IrynaSkurkoOptoolProgram
{
    class DocumentImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int image_id { get; set; }
        public int document_id { get; set; }
        public byte[] image_url { get; set; }
        public string image_tag { get; set; }

        public DocumentImage() { }
        public DocumentImage(int document_id, byte[] image_url, string image_tag) 
        {
            this.document_id = document_id;
            this.image_url = image_url;
            this.image_tag = image_tag;
        }
    }
}
