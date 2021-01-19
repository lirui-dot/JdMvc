using Microsoft.EntityFrameworkCore;

namespace JdMvc.Models{
    public class UserContext:DbContext{
        public UserContext(DbContextOptions<UserContext> options)
        :base(options)
        {
        }
        public DbSet<User> Users{get;set;}
        public DbSet<Province> Provinces{get;set;}
        public DbSet<Personal> Personals{get;set;}
        public DbSet<Hobby> Hobbies{get;set;}
        public DbSet<Image> Images{get;set;}
        public DbSet<Address> addredds{get;set;}
    }
}