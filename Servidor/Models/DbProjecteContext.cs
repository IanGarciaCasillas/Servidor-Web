using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Servidor.Models;

public partial class DbProjecteContext : DbContext
{
    public DbProjecteContext()
    {
    }

    public DbProjecteContext(DbContextOptions<DbProjecteContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AlbaraCompra> AlbaraCompras { get; set; }

    public virtual DbSet<AlbaraCompraDetall> AlbaraCompraDetalls { get; set; }

    public virtual DbSet<AlbaraVendaDetall> AlbaraVendaDetalls { get; set; }

    public virtual DbSet<AlbaraVendum> AlbaraVenda { get; set; }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ComandaCompra> ComandaCompras { get; set; }

    public virtual DbSet<ComandaCompraDetall> ComandaCompraDetalls { get; set; }

    public virtual DbSet<ComandaVendaDetall> ComandaVendaDetalls { get; set; }

    public virtual DbSet<ComandaVendum> ComandaVenda { get; set; }

    public virtual DbSet<Empl> Empls { get; set; }

    public virtual DbSet<FacturaCompra> FacturaCompras { get; set; }

    public virtual DbSet<PreuArticleProveidor> PreuArticleProveidors { get; set; }

    public virtual DbSet<Proveidor> Proveidors { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketDetall> TicketDetalls { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user id=root;database=db_projecte;allowuservariables=True;ConvertZeroDateTime=True", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.27-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<AlbaraCompra>(entity =>
        {
            entity.HasKey(e => e.IdAlbaraCompra).HasName("PRIMARY");

            entity.ToTable("albara_compra");

            entity.HasIndex(e => e.IdComandaCompra, "FK_AlbaraCompra_ComandaCompra");

            entity.HasIndex(e => e.IdProveidor, "FK_AlbaraCompra_Proveidor");

            entity.Property(e => e.IdAlbaraCompra).HasColumnType("int(255)");
            entity.Property(e => e.Data).HasColumnType("datetime");
            entity.Property(e => e.IdAlbaraProveidor).HasColumnType("int(255)");
            entity.Property(e => e.IdComandaCompra).HasColumnType("int(255)");
            entity.Property(e => e.IdProveidor).HasColumnType("int(255)");

            entity.HasOne(d => d.IdComandaCompraNavigation).WithMany(p => p.AlbaraCompras)
                .HasForeignKey(d => d.IdComandaCompra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlbaraCompra_ComandaCompra");

            entity.HasOne(d => d.IdProveidorNavigation).WithMany(p => p.AlbaraCompras)
                .HasForeignKey(d => d.IdProveidor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlbaraCompra_Proveidor");
        });

        modelBuilder.Entity<AlbaraCompraDetall>(entity =>
        {
            entity.HasKey(e => new { e.IdAlbaraCompra, e.IdArticle })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("albara_compra_detalls");

            entity.HasIndex(e => e.IdArticle, "FK_AlbaraCompraDetalls_Articles");

            entity.Property(e => e.IdAlbaraCompra).HasColumnType("int(255)");
            entity.Property(e => e.IdArticle).HasColumnType("int(255)");
            entity.Property(e => e.Quantitat).HasColumnType("double(10,2)");

            entity.HasOne(d => d.IdAlbaraCompraNavigation).WithMany(p => p.AlbaraCompraDetalls)
                .HasForeignKey(d => d.IdAlbaraCompra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlbaraCompraDetalls_AlbaraCompra");

            entity.HasOne(d => d.IdArticleNavigation).WithMany(p => p.AlbaraCompraDetalls)
                .HasForeignKey(d => d.IdArticle)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlbaraCompraDetalls_Articles");
        });

        modelBuilder.Entity<AlbaraVendaDetall>(entity =>
        {
            entity.HasKey(e => new { e.IdAlbaraVenda, e.IdArticle })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("albara_venda_detalls");

            entity.HasIndex(e => e.IdArticle, "FK_AlbaraVendaDetalls_Articles");

            entity.Property(e => e.IdAlbaraVenda).HasColumnType("int(255)");
            entity.Property(e => e.IdArticle).HasColumnType("int(255)");
            entity.Property(e => e.Quantitat).HasColumnType("double(10,2)");

            entity.HasOne(d => d.IdAlbaraVendaNavigation).WithMany(p => p.AlbaraVendaDetalls)
                .HasForeignKey(d => d.IdAlbaraVenda)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlbaraVendaDetalls_AlbaraVenda");

            entity.HasOne(d => d.IdArticleNavigation).WithMany(p => p.AlbaraVendaDetalls)
                .HasForeignKey(d => d.IdArticle)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AlbaraVendaDetalls_Articles");
        });

        modelBuilder.Entity<AlbaraVendum>(entity =>
        {
            entity.HasKey(e => e.IdAlbara).HasName("PRIMARY");

            entity.ToTable("albara_venda");

            entity.Property(e => e.IdAlbara).HasColumnType("int(255)");
            entity.Property(e => e.Data).HasColumnType("datetime");
        });

        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.IdArticle).HasName("PRIMARY");

            entity.ToTable("articles");

            entity.HasIndex(e => e.IdCategoria, "FK_Articles_Categoria");

            entity.HasIndex(e => e.IdProveidorHabitual, "FK_Articles_Proveidor");

            entity.Property(e => e.IdArticle).HasColumnType("int(255)");
            entity.Property(e => e.AutoStock).HasPrecision(10, 4);
            entity.Property(e => e.DescripcioArticle).HasMaxLength(255);
            entity.Property(e => e.IdCategoria).HasColumnType("int(255)");
            entity.Property(e => e.IdProveidorHabitual).HasColumnType("int(255)");
            entity.Property(e => e.IvaAplicar).HasColumnType("double(10,2)");
            entity.Property(e => e.MinimStock).HasPrecision(10, 4);
            entity.Property(e => e.NomArticle).HasMaxLength(255);
            entity.Property(e => e.NumVenda).HasColumnType("int(255)");
            entity.Property(e => e.PreuVenta).HasPrecision(10, 2);
            entity.Property(e => e.Stock).HasPrecision(10, 4);
            entity.Property(e => e.TipusUnitat).HasMaxLength(255);

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Articles)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK_Articles_Categoria");

            entity.HasOne(d => d.IdProveidorHabitualNavigation).WithMany(p => p.Articles)
                .HasForeignKey(d => d.IdProveidorHabitual)
                .HasConstraintName("FK_Articles_Proveidor");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PRIMARY");

            entity.ToTable("categoria");

            entity.Property(e => e.IdCategoria).HasColumnType("int(255)");
            entity.Property(e => e.NomCategoria).HasMaxLength(255);
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient).HasName("PRIMARY");

            entity.ToTable("client");

            entity.Property(e => e.IdClient).HasColumnType("int(255)");
            entity.Property(e => e.CodicPostal).HasMaxLength(255);
            entity.Property(e => e.Cognom1Client).HasMaxLength(255);
            entity.Property(e => e.Cognom2Client).HasMaxLength(255);
            entity.Property(e => e.ContrasenyaClient).HasMaxLength(255);
            entity.Property(e => e.CorreuClient).HasMaxLength(255);
            entity.Property(e => e.DireccioClient).HasMaxLength(255);
            entity.Property(e => e.Dni).HasMaxLength(255);
            entity.Property(e => e.NomClient).HasMaxLength(255);
            entity.Property(e => e.TelefonClient).HasMaxLength(255);
            entity.Property(e => e.Token).HasMaxLength(255);
        });

        modelBuilder.Entity<ComandaCompra>(entity =>
        {
            entity.HasKey(e => e.IdComandaCompra).HasName("PRIMARY");

            entity.ToTable("comanda_compra");

            entity.Property(e => e.IdComandaCompra).HasColumnType("int(255)");
            entity.Property(e => e.DataComandaCompra).HasColumnType("datetime");
            entity.Property(e => e.Estat).HasMaxLength(255);
        });

        modelBuilder.Entity<ComandaCompraDetall>(entity =>
        {
            entity.HasKey(e => new { e.IdComandaCompra, e.IdArticle })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("comanda_compra_detalls");

            entity.HasIndex(e => e.IdArticle, "FK_ComandaCompraDetalls_Articles");

            entity.HasIndex(e => e.IdProveidor, "FK_ComandaCompraDetalls_Proveidor");

            entity.Property(e => e.IdComandaCompra).HasColumnType("int(255)");
            entity.Property(e => e.IdArticle).HasColumnType("int(255)");
            entity.Property(e => e.IdProveidor).HasColumnType("int(255)");
            entity.Property(e => e.QuantitatDemanada).HasColumnType("double(10,2)");
            entity.Property(e => e.QuantitatServida).HasColumnType("double(10,2)");

            entity.HasOne(d => d.IdArticleNavigation).WithMany(p => p.ComandaCompraDetalls)
                .HasForeignKey(d => d.IdArticle)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ComandaCompraDetalls_Articles");

            entity.HasOne(d => d.IdComandaCompraNavigation).WithMany(p => p.ComandaCompraDetalls)
                .HasForeignKey(d => d.IdComandaCompra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ComandaCompraDetalls_ComandaCompra");

            entity.HasOne(d => d.IdProveidorNavigation).WithMany(p => p.ComandaCompraDetalls)
                .HasForeignKey(d => d.IdProveidor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ComandaCompraDetalls_Proveidor");
        });

        modelBuilder.Entity<ComandaVendaDetall>(entity =>
        {
            entity.HasKey(e => new { e.IdComandaVenda, e.IdArticle })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("comanda_venda_detalls");

            entity.HasIndex(e => e.IdArticle, "FK_ComandaVendaDetalls_Articles");

            entity.Property(e => e.IdComandaVenda).HasColumnType("int(255)");
            entity.Property(e => e.IdArticle).HasColumnType("int(255)");
            entity.Property(e => e.QuantitatDemanada).HasColumnType("double(10,2)");
            entity.Property(e => e.QuantitatServida).HasColumnType("double(10,2)");

            entity.HasOne(d => d.IdArticleNavigation).WithMany(p => p.ComandaVendaDetalls)
                .HasForeignKey(d => d.IdArticle)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ComandaVendaDetalls_Articles");

            entity.HasOne(d => d.IdComandaVendaNavigation).WithMany(p => p.ComandaVendaDetalls)
                .HasForeignKey(d => d.IdComandaVenda)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ComandaVendaDetalls_ComandaVenda");
        });

        modelBuilder.Entity<ComandaVendum>(entity =>
        {
            entity.HasKey(e => e.IdComanda).HasName("PRIMARY");

            entity.ToTable("comanda_venda");

            entity.HasIndex(e => e.IdClient, "FK_ComandaVenda_Client");

            entity.Property(e => e.IdComanda).HasColumnType("int(255)");
            entity.Property(e => e.DataComanda).HasColumnType("datetime");
            entity.Property(e => e.EstatComandaVenda).HasMaxLength(255);
            entity.Property(e => e.IdClient).HasColumnType("int(255)");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.ComandaVenda)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ComandaVenda_Client");
        });

        modelBuilder.Entity<Empl>(entity =>
        {
            entity.HasKey(e => e.IdEmpl).HasName("PRIMARY");

            entity.ToTable("empl");

            entity.HasIndex(e => e.IdRol, "FK_Empl_Rol");

            entity.Property(e => e.IdEmpl).HasColumnType("int(255)");
            entity.Property(e => e.Cognom1Empl).HasMaxLength(255);
            entity.Property(e => e.Cognom2Empl).HasMaxLength(255);
            entity.Property(e => e.DataAltaEmpl).HasColumnType("datetime");
            entity.Property(e => e.DataBaixaEmpl).HasColumnType("datetime");
            entity.Property(e => e.DataNaixEmpl).HasColumnType("datetime");
            entity.Property(e => e.DireccioEmpl).HasMaxLength(255);
            entity.Property(e => e.DniEmpl).HasMaxLength(255);
            entity.Property(e => e.IdRol).HasColumnType("int(255)");
            entity.Property(e => e.JornadaEmpl).HasMaxLength(255);
            entity.Property(e => e.NomEmpl).HasMaxLength(255);
            entity.Property(e => e.NssEmpl).HasMaxLength(255);
            entity.Property(e => e.SexeEmpl).HasMaxLength(1);
            entity.Property(e => e.TelefonEmpl).HasColumnType("int(255)");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Empls)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Empl_Rol");
        });

        modelBuilder.Entity<FacturaCompra>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("PRIMARY");

            entity.ToTable("factura_compra");

            entity.HasIndex(e => e.IdProveidor, "FK_FacturaCompra_Proveidor");

            entity.Property(e => e.IdFactura).HasColumnType("int(255)");
            entity.Property(e => e.Data).HasColumnType("datetime");
            entity.Property(e => e.IdProveidor).HasColumnType("int(255)");

            entity.HasOne(d => d.IdProveidorNavigation).WithMany(p => p.FacturaCompras)
                .HasForeignKey(d => d.IdProveidor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FacturaCompra_Proveidor");

            entity.HasMany(d => d.IdAlbaraCompras).WithMany(p => p.IdFacturaCompras)
                .UsingEntity<Dictionary<string, object>>(
                    "FacturaCompraDetall",
                    r => r.HasOne<AlbaraCompra>().WithMany()
                        .HasForeignKey("IdAlbaraCompra")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_FacturaCompraDetalls_AlbaraCompra"),
                    l => l.HasOne<FacturaCompra>().WithMany()
                        .HasForeignKey("IdFacturaCompra")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_FacturaCompraDetalls_FacturaCompra"),
                    j =>
                    {
                        j.HasKey("IdFacturaCompra", "IdAlbaraCompra")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("factura_compra_detalls");
                        j.HasIndex(new[] { "IdAlbaraCompra" }, "FK_FacturaCompraDetalls_AlbaraCompra");
                        j.IndexerProperty<int>("IdFacturaCompra").HasColumnType("int(255)");
                        j.IndexerProperty<int>("IdAlbaraCompra").HasColumnType("int(255)");
                    });
        });

        modelBuilder.Entity<PreuArticleProveidor>(entity =>
        {
            entity.HasKey(e => new { e.IdArticle, e.IdProveidor })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("preu_article_proveidor");

            entity.HasIndex(e => e.IdProveidor, "FK_PreuArticleProveidor_Proveidor");

            entity.Property(e => e.IdArticle).HasColumnType("int(255)");
            entity.Property(e => e.IdProveidor).HasColumnType("int(255)");
            entity.Property(e => e.PreuCompra).HasColumnType("double(10,2)");

            entity.HasOne(d => d.IdArticleNavigation).WithMany(p => p.PreuArticleProveidors)
                .HasForeignKey(d => d.IdArticle)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PreuArticleProveidor_Articles");

            entity.HasOne(d => d.IdProveidorNavigation).WithMany(p => p.PreuArticleProveidors)
                .HasForeignKey(d => d.IdProveidor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PreuArticleProveidor_Proveidor");
        });

        modelBuilder.Entity<Proveidor>(entity =>
        {
            entity.HasKey(e => e.IdProveidor).HasName("PRIMARY");

            entity.ToTable("proveidor");

            entity.Property(e => e.IdProveidor).HasColumnType("int(255)");
            entity.Property(e => e.DireccioProveidor).HasMaxLength(255);
            entity.Property(e => e.NomProveidor).HasMaxLength(255);
            entity.Property(e => e.PreuCompra).HasPrecision(10, 2);
            entity.Property(e => e.TelefonProveidor).HasMaxLength(255);
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PRIMARY");

            entity.ToTable("rol");

            entity.Property(e => e.IdRol)
                .ValueGeneratedNever()
                .HasColumnType("int(255)");
            entity.Property(e => e.NomRol).HasMaxLength(255);
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => new { e.IdTicket, e.NumDocument })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("ticket");

            entity.HasIndex(e => e.IdAlbara, "FK_Ticket_AlbaraVenda");

            entity.HasIndex(e => e.IdClient, "FK_Ticket_Client");

            entity.HasIndex(e => e.IdComanda, "FK_Ticket_ComandaVenda");

            entity.Property(e => e.IdTicket).HasColumnType("int(255)");
            entity.Property(e => e.NumDocument).HasColumnType("int(255)");
            entity.Property(e => e.DataTicket).HasColumnType("datetime");
            entity.Property(e => e.IdAlbara).HasColumnType("int(255)");
            entity.Property(e => e.IdClient).HasColumnType("int(255)");
            entity.Property(e => e.IdComanda).HasColumnType("int(255)");

            entity.HasOne(d => d.IdAlbaraNavigation).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.IdAlbara)
                .HasConstraintName("FK_Ticket_AlbaraVenda");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("FK_Ticket_Client");

            entity.HasOne(d => d.IdComandaNavigation).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.IdComanda)
                .HasConstraintName("FK_Ticket_ComandaVenda");
        });

        modelBuilder.Entity<TicketDetall>(entity =>
        {
            entity.HasKey(e => new { e.IdTicket, e.NumDocument, e.IdArticle })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity.ToTable("ticket_detalls");

            entity.HasIndex(e => e.IdArticle, "FK_TicketDetalls_Articles");

            entity.Property(e => e.IdTicket).HasColumnType("int(255)");
            entity.Property(e => e.NumDocument).HasColumnType("int(255)");
            entity.Property(e => e.IdArticle).HasColumnType("int(255)");
            entity.Property(e => e.Descompte).HasColumnType("double(10,2)");
            entity.Property(e => e.IvaAplicar).HasColumnType("double(10,2)");
            entity.Property(e => e.PreuArticle).HasColumnType("double(10,2)");
            entity.Property(e => e.Quantitat).HasColumnType("double(10,2)");


            entity.HasOne(d => d.IdArticleNavigation).WithMany(p => p.TicketDetalls)
                .HasForeignKey(d => d.IdArticle)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TicketDetalls_Articles");
            
            
            entity.HasOne(d => d.Ticket).WithMany(p => p.TicketDetalls)
                .HasForeignKey(d => new { d.IdTicket, d.NumDocument })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TicketDetalls_Ticket");
            
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
