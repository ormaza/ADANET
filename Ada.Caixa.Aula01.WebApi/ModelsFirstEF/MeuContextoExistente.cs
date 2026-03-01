using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Ada.Caixa.Aula01.WebApi.ModelsFirstEF;

public partial class MeuContextoExistente : DbContext
{
    public MeuContextoExistente()
    {
    }

    public MeuContextoExistente(DbContextOptions<MeuContextoExistente> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source=MeuBancoFirst.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasIndex(e => e.Email, "IX_Clientes_Email").IsUnique();

            entity.Property(e => e.DataCriacao).HasDefaultValueSql("current_timestamp");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasOne(d => d.Cliente).WithMany(p => p.Pedidos).HasForeignKey(d => d.ClienteId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
