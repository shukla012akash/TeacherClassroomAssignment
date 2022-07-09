using ApiDbLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiDbLayer
{
    public class DemoDbContext : DbContext
    {
        public DemoDbContext()
        {

        }
        public DemoDbContext(DbContextOptions options)
       : base(options)
        {

        }
       
        public DbSet<Teacher> Teachers{ get; set; }
        public DbSet<Classroom> Classrooms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer(@"Data Source=DESKTOP-0LKSRK2;Initial Catalog=TeacherClassroom;Integrated Security=True");
        }
    }
}
