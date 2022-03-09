using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IrynaSkurkoOptoolProgram
{
    class Document
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int document_id { get; set; }

        public int document_type_id { get; set; }
        public int status_id { get; set; }
        public string date_uploaded { get; set; }
        public int client_id { get; set; }
        public string document_number { get; set; }
        public string date_verified { get; set; }
        public string expiry_date { get; set; }
        public int operator_id { get; set; }
        public string document_issuer { get; set; }
        public int verification_type_id { get; set; }

        public Document() { }
        public Document(int document_type_id, string date_uploaded, int client_id, int verification_type_id, int operator_id,
                        int status_id, string document_number = "", string date_verified = "", 
                        string expiry_date = "", string document_issuer = "")
        {
            this.document_type_id = document_type_id;
            this.status_id = status_id;
            this.date_uploaded = date_uploaded;
            this.client_id = client_id;
            this.document_number = document_number;
            this.date_verified = date_verified;
            this.expiry_date = expiry_date;
            this.operator_id = operator_id;
            this.document_issuer = document_issuer;
            this.verification_type_id = verification_type_id;
        }
    }
}
