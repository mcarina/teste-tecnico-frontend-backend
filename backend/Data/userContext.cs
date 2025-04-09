namespace User.Data
{
    using Microsoft.EntityFrameworkCore;
    using User.Models;

    public class UserContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }

        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        public override int SaveChanges()
        {
            UpdateTimestamps(); // Atualiza timestamps antes de salvar
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps(); // Atualiza timestamps antes de salvar (versão async)
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is User.Models.UserModel &&
                            (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                var entity = (User.Models.UserModel)entityEntry.Entity;
                entity.UpdatedAt = DateTime.UtcNow;

                if (entityEntry.State == EntityState.Added)
                {
                    entity.CreatedAt = DateTime.UtcNow;
                }
            }
        }
    }
}
