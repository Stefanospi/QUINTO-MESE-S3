using Microsoft.EntityFrameworkCore;

namespace PROGETTO_S3.Models
{
    public class DataContext:DbContext
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Ingridient> Ingridients { get; set; }
        public virtual DbSet<Order>Orders { get; set; }
        public virtual DbSet<OrderedProduct> OrderedProducts { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }


        public DataContext(DbContextOptions<DataContext> opt) : base(opt) 
        {

        }
    }
}
