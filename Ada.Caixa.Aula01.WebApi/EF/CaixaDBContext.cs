using Microsoft.EntityFrameworkCore;

public class CaixaDBContext : DbContext
{
    public CaixaDBContext(DbContextOptions<CaixaDBContext> options) : base(options)
    {
    }

    public DbSet<ContaBancaria> Contas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=caixa.db");
}