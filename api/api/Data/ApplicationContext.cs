using api.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;

namespace api.Data;

public class ApplicationContext : DbContext
{
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Award> Awards { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Technology> Technologies { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<WorkArrangement> WorkArrangements { get; set; }
    public DbSet<AuthRecord> AuthRecords { get; set; }
    public DbSet<ProjectUser> ProjectUsers { get; set; }
    public DbSet<ProjectSolution> ProjectSolutions { get; set; }
    public DbSet<AwardUser> AwardUsers { get; set; }
    public DbSet<Socials> Socials { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<FriendRequest> FriendRequests { get; set; }
    public DbSet<Otp> Otps { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>()
            .HasMany(e => e.Candidates)
            .WithMany(e => e.ProjectsAppliedTo)
            .UsingEntity<ProjectUser>();

        modelBuilder.Entity<Project>()
            .HasOne(e => e.Recruiter);

        modelBuilder.Entity<ProjectUser>()
            .HasKey(pu => new { pu.UserId, pu.ProjectId });

        modelBuilder.Entity<User>()
            .HasMany(u => u.Awards)
            .WithMany(a => a.Users)
            .UsingEntity<AwardUser>();

        modelBuilder.Entity<AwardUser>()
            .HasKey(au => new { au.AwardId, au.UserId });

        modelBuilder.Entity<User>()
            .HasOne(u => u.Socials)
            .WithOne(s => s.User)
            .HasForeignKey<Socials>(s => s.UserId);
    }
}