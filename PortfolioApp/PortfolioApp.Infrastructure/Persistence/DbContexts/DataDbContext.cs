using Microsoft.EntityFrameworkCore;
using PortfolioApp.Core.Entities;

namespace PortfolioApp.Infrastructure.Persistence.DbContexts;
public class DataDbContext : DbContext
{
    public DataDbContext(DbContextOptions<DataDbContext> options) : base(options) { }
    public DbSet<AboutMeEntity> AboutMe { get; set; }
    public DbSet<BlogPostEntity> BlogPosts { get; set; }
    public DbSet<EducationEntity> Educations { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }
    public DbSet<ExperienceEntity> Experiences { get; set; }
    public DbSet<PersonalInfoEntity> PersonalInfo { get; set; }
    public DbSet<ContactMessageEntity> ContactMessages { get; set; }
    public DbSet<ProjectEntity> ProjectEntities { get; set; }
    public DbSet<SocialLinksEntity> SocialLinks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
