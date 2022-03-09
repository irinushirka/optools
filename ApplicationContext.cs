using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace IrynaSkurkoOptoolProgram
{
    class ApplicationContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Operator> Operator { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<DocumentType> DocumentType { get; set; }
        public DbSet<VerificationType> VerificationType { get; set; }
        public DbSet<DocumentStatus> DocumentStatus { get; set; }
        public DbSet<Document> Document { get; set; }
        public DbSet<DocumentImage> DocumentImage { get; set; }

        public ApplicationContext() : base("DefaultConnection") { }
    }
}
