using MagicVilla_VillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options){ }
        public DbSet<Villa> Villas{ get; set; }
        public DbSet<VillaNumber> VillaNumbers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    Id = 1,
                    Name = "Royal Villa",
                    Details = "Ocean View by Exceptional Villas is a lovely 3 bedroom villa set on 2 levels and located on the prestigious Royal Westmoreland Golf Club. Guests staying at here have panoramic view of the Caribbean Sea while overlooking a beautiful pond and fountain which is home to local birds. This is a very well designed villa and has a homely feel to it. It can be comfortably rented as a one, two or three bedroom and has a communal swimming pool and golf access payable locally.",
                    ImageUrl = "",
                    Occupancy = 5,
                    Rate = 200,
                    Sqft = 550,
                    Amenity = "",
                    CreatedDate = DateTime.Now
                });
        }
    }
}
