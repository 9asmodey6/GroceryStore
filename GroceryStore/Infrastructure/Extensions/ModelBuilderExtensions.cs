namespace GroceryStore.Database.Extensions;

using System.Linq;
//using Entities.Identity;
//using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

public static class ModelBuilderExtensions
{
    /*public void AddIdentityTableNames()
    {
        modelBuilder.Entity<User>(i => { i.ToTable("Users"); });
        modelBuilder.Entity<Role>(i => { i.ToTable("Roles"); });
        modelBuilder.Entity<UserRole>(i => { i.ToTable("UserRoles"); });
        modelBuilder.Entity<UserLogin>(i => { i.ToTable("UserLogins"); });
        modelBuilder.Entity<RoleClaim>(i => { i.ToTable("RoleClaims"); });
        modelBuilder.Entity<UserClaim>(i => { i.ToTable("UserClaims"); });
        modelBuilder.Entity<UserToken>(i => { i.ToTable("UserTokens"); });
    }*/

    public static void AddPostgreSqlRules(this ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            if (entity.BaseType == null)
            {
                entity.SetTableName(entity.GetTableName()?.ToSnakeCase());
            }

            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(property.Name.ToSnakeCase());
            }

            foreach (var key in entity.GetKeys())
            {
                key.SetName(key.GetName()?.ToSnakeCase());
            }

            foreach (var key in entity.GetForeignKeys())
            {
                key.SetConstraintName(key.GetConstraintName()?.ToSnakeCase());
            }

            foreach (var index in entity.GetIndexes())
            {
                index.SetDatabaseName(index.GetDatabaseName()?.ToSnakeCase());
            }
        }
    }

    public static void OnDeleteRestrictRules(this ModelBuilder modelBuilder)
    {
        var cascadeFKs = modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetForeignKeys())
            .Where(fk => fk is { IsOwnership: false, DeleteBehavior: DeleteBehavior.Cascade });

        foreach (var fk in cascadeFKs)
        {
            fk.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}