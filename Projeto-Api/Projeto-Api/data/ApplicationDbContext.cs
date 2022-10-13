using Avaliacao_loja_interativa_c.Models;
using Flunt.Notifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Projeto_Api.Domain.Concenssao;
using Projeto_Api.Domain.Documento;
using Projeto_Api.Domain.Tipos;
using System.Reflection.Emit;

namespace IWantApp.Infra.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Documento> Documentos { get; set; }
    public DbSet<Tipo> Tipos { get; set; }
    public DbSet<Concessao> Concessoes { get; set; }
    public DbSet<User> Users { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt) :
           base(opt) => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseMySql(
               "Host=localhost;Port=3306;Pooling=true;Database=gestorDoc2;User Id=root;Password=admin;",
               new MySqlServerVersion(new Version(8, 0)),
                options => options.EnableRetryOnFailure());
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasMany(u => u.Documentos)
            .WithOne(d => d.Usuario);

        builder.Ignore<Notification>();

        builder.Entity<User>().HasIndex(u => u.Email).IsUnique();


        builder.Entity<Documento>().HasIndex(d => d.Descricao).HasPrefixLength(3);

        builder.Entity<Tipo>().HasIndex(d => d.Nome).IsUnique();
        builder.Entity<Tipo>().HasIndex(d => d.Nome).HasPrefixLength(3);


        builder.Entity<Tipo>().HasIndex(d => d.Sigla).IsUnique();
        builder.Entity<Tipo>().HasIndex(d => d.Sigla).HasPrefixLength(3);
        builder.Entity<Tipo>().Property(d => d.Sigla).HasMaxLength(3);


        builder.Entity<Concessao>().HasIndex(d => d.Nome).HasPrefixLength(3);
        builder.Entity<Concessao>().HasIndex(d => d.Nome).IsUnique();

        builder.Entity<Concessao>().HasIndex(d => d.Sigla).IsUnique();
        builder.Entity<Concessao>().HasIndex(d => d.Sigla).HasPrefixLength(3);
        builder.Entity<Concessao>().Property(d => d.Sigla).HasMaxLength(3);

        /*
     builder.Entity<Product>()
         .Property(p => p.Name).IsRequired();
     builder.Entity<Product>()
         .Property(p => p.Description).HasMaxLength(255);
     builder.Entity<Product>()
         .Property(p => p.Price).HasColumnType("decimal(10,2)").IsRequired();

     builder.Entity<Category>()
         .Property(c => c.Name).IsRequired();

     builder.Entity<Order>()
         .Property(o => o.ClientId).IsRequired();
     builder.Entity<Order>()
         .Property(o => o.DeliveryAddress).IsRequired();
     builder.Entity<Order>()
         .HasMany(o => o.Products)
         .WithMany(p => p.Orders)
         .UsingEntity(x => x.ToTable("OrderProducts"));*/
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configuration)
    {
        configuration.Properties<string>()
            .HaveMaxLength(100);
    }
}
