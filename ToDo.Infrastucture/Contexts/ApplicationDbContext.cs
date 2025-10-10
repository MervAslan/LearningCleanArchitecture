using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDo.Domain.Entities;


namespace ToDo.Infrastucture.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);// kendi ayarlarımızı eklesek de efnin temel işlevleri çalışmaya devam etsin diye bunu
                                               // her zaman ekleriiz.eğer bunu yazmazsak efnin kendi içinde tanımlı bazı kuralları devre dışı kalabilir örneğin;
                                               // otomatik Primary Key belirleme
        }                                      // ilişkilerde (Foreign Key) otomatik davranışlar
    }                                          // varsayılan tablo isimlendirmeleri
}
