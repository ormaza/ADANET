using Microsoft.EntityFrameworkCore;

public class CaixaDbContext : DbContext
{
    public CaixaDbContext(DbContextOptions<CaixaDbContext> options) : base(options)
    {
    }

    public DbSet<ContaBancaria> Contas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=caixa.db");
}