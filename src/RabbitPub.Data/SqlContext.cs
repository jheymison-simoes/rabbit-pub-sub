using RabbitPub.Domain.Models;
using Marques.EFCore.SnakeCase;
using Microsoft.EntityFrameworkCore;

namespace RabbitPub.Data;

public class SqlContext : DbContext
{
    public SqlContext(DbContextOptions<SqlContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SqlContext).Assembly);
        modelBuilder.ToSnakeCase();

        base.OnModelCreating(modelBuilder);
    }
}