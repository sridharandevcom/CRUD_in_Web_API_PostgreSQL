using Microsoft.EntityFrameworkCore;
using SampleWebAPI.Models;

namespace SampleWebAPI.DAL
{
    public class UserRepository:DbContext   
    {
        public UserRepository(DbContextOptions options) : base(options) {
        }

        public DbSet<UsersModel> Users { get; set; }
    }
}
