using Microsoft.EntityFrameworkCore;

namespace routing
{
    public class DbConetxt : DbContext
    {
        public DbConetxt(DbContextOptions<DbConetxt> options) : base(options)
        {
            
        }

        public DbSet<Rota> Rotas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Funcionalidade> Funcionalidades { get; set; }
        public DbSet<RotaFuncionalidade> RotasFuncionalidades { get; set; }
        public DbSet<UsuarioRotaFuncionalidade> UsuariosRotasFuncionalidades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RotaFuncionalidade>().HasKey(rf => rf.Id);
            modelBuilder.Entity<RotaFuncionalidade>()
            .HasOne(rf => rf.Rota)
            .WithMany(rf => rf.RotasFuncionalidades)
            .HasForeignKey(rf => rf.IdRota);
            modelBuilder.Entity<RotaFuncionalidade>()
            .HasOne(rf => rf.Funcionalidade)
            .WithMany(rf => rf.RotasFuncionalidades)
            .HasForeignKey(rf => rf.IdFuncionalidade);

            modelBuilder.Entity<UsuarioRotaFuncionalidade>().HasKey(urf => urf.Id);
            modelBuilder.Entity<UsuarioRotaFuncionalidade>()
            .HasOne(urf => urf.RotaFuncionalidade)
            .WithMany(urf => urf.UsuariosRotasFuncionalidades)
            .HasForeignKey(urf => urf.IdRotaFuncionalidade);
            modelBuilder.Entity<UsuarioRotaFuncionalidade>()
            .HasOne(urf => urf.Usuario)
            .WithMany(urf => urf.UsuarioRotaFuncionalidades)
            .HasForeignKey(urf => urf.IdUsuario);
        }
    }
}