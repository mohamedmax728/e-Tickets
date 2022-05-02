using eTickets.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
                
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor_Movie>().HasKey(am =>new
            {
                am.ActorId,
                am.MovieId
            });
            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Movie).WithMany(am => am.Actor_Movies).HasForeignKey(
                am=>am.MovieId);
            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Actor).WithMany(am => am.Actor_Movies).HasForeignKey(
                am => am.ActorId);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Actor>actors  { get; set; }
        public DbSet<Cinema> cinemas { get; set; }
        public DbSet<Producer> producers { get; set; }
        public DbSet<Movie> movies { get; set; }
        public DbSet<Actor_Movie> actor_Movies { get; set; }


        //Orders related Tables 
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCartItem> shoppingCartItems { get; set; }
    }
}
