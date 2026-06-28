using aspnet.Models;
using Microsoft.EntityFrameworkCore;

// Le contexte de base de données d'EF Core. En héritant de DbContext, cette classe
// devient le pont entre nos objets C# (les entités) et les tables SQLite.
public class AppDbContext : DbContext
{
    // Les options (fournisseur, chaîne de connexion) sont passées par la base via ": base(options)".
    // Elles proviennent de la configuration faite dans Program.cs (AddDbContext + UseSqlite).
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Chaque DbSet<T> représente une table : EF Core la mappe sur la classe d'entité correspondante.
    // On interroge ces propriétés comme des collections (LINQ) et EF traduit en requêtes SQL.
    public DbSet<CarBrand> CarBrands { get; set; }
    public DbSet<CarModel> CarModels { get; set; }

    public DbSet<Car> Cars { get; set; }

    // Méthode redéfinie (override) de DbContext : on personnalise le mapping objet-relationnel.
    // Ici on impose le nom des tables ("CarBrand" et "CarModel" au singulier).
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarBrand>().ToTable("CarBrand");
        modelBuilder.Entity<CarModel>().ToTable("CarModel");
    }
}
