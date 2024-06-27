using Microsoft.EntityFrameworkCore;
using TicketManagement.Model;

namespace DemoVnPay.Model
{
    public class DataContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<VnPayInfo> vnPayInfos { get; set; }
        //private static DataContext _Instance;
        //public static DataContext GetInstance()
        //{
        //    if (_Instance == null)
        //    {
        //        _Instance = new DataContext();
        //    }
        //    return _Instance;
        //}

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            //optionsBuilder.UseSqlServer("Data Source=DESKTOP-4IUHJ9T\\SQLEXPRESS;Initial Catalog=VehicleTicket;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=True;");
        }
    }
}
