using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using FBDE.POC.API.DAL.Model;

namespace FBDE.POC.API.DAL.ORM.EFCore;

public partial class FbdePocDbContext : DbContext
{
    public FbdePocDbContext()
    {
    }

    public FbdePocDbContext(DbContextOptions<FbdePocDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AutorisationUtilisateurModule> AutorisationUtilisateurModules { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Commande> Commandes { get; set; }

    public virtual DbSet<ContratTravail> ContratTravails { get; set; }

    public virtual DbSet<Employe> Employes { get; set; }

    public virtual DbSet<FactureChargeFbde> FactureChargeFbdes { get; set; }

    public virtual DbSet<FactureCommandeClient> FactureCommandeClients { get; set; }

    public virtual DbSet<FactureProjetClient> FactureProjetClients { get; set; }

    public virtual DbSet<Fonction> Fonctions { get; set; }

    public virtual DbSet<LigneCommande> LigneCommandes { get; set; }

    public virtual DbSet<Localisation> Localisations { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<NotificationPieceJointe> NotificationPieceJointes { get; set; }

    public virtual DbSet<PanierAchat> PanierAchats { get; set; }

    public virtual DbSet<PieceJointe> PieceJointes { get; set; }

    public virtual DbSet<Planning> Plannings { get; set; }

    public virtual DbSet<Produit> Produits { get; set; }

    public virtual DbSet<Projet> Projets { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=DESKTOP-9H3EM17;Database=FBDE_POC_DB;Integrated Security=False;TrustServerCertificate=true;User ID=sa;Password=01031207Fr@nckyx;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AutorisationUtilisateurModule>(entity =>
        {
            entity.ToTable("AutorisationUtilisateurModule");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Create).HasColumnName("create");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("created_on");
            entity.Property(e => e.Delete).HasColumnName("delete");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasColumnType("datetime")
                .HasColumnName("modified_on");
            entity.Property(e => e.Read).HasColumnName("read");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Update).HasColumnName("update");

            entity.HasOne(d => d.Module).WithMany(p => p.AutorisationUtilisateurModules)
                .HasForeignKey(d => d.ModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AutorisationUtilisateurModule_Module");

            entity.HasOne(d => d.Utilisateur).WithMany(p => p.AutorisationUtilisateurModules)
                .HasForeignKey(d => d.UtilisateurId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AutorisationUtilisateurModule_Utilisateur");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("Client");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("created_on");
            entity.Property(e => e.DateCreation)
                .HasColumnType("datetime")
                .HasColumnName("date_creation");
            entity.Property(e => e.Description)
                .HasMaxLength(4000)
                .HasColumnName("description");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(2000)
                .HasColumnName("imageURL");
            entity.Property(e => e.LocalisationId).HasColumnName("localisationId");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasColumnType("datetime")
                .HasColumnName("modified_on");
            entity.Property(e => e.RaisonSociale)
                .HasMaxLength(200)
                .HasColumnName("raison_sociale");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Type)
                .HasComment("Particulier;Entreprise")
                .HasColumnName("type");

            entity.HasOne(d => d.Localisation).WithMany(p => p.Clients)
                .HasForeignKey(d => d.LocalisationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Client_Localisation");
        });

        modelBuilder.Entity<Commande>(entity =>
        {
            entity.ToTable("Commande");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientId).HasComment("CDI;CDD;Stage;Alternance;Journalier");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("created_on");
            entity.Property(e => e.Description)
                .HasMaxLength(4000)
                .HasColumnName("description");
            entity.Property(e => e.Libelle)
                .HasMaxLength(100)
                .HasColumnName("libelle");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasColumnType("datetime")
                .HasColumnName("modified_on");
            entity.Property(e => e.Montant).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Client).WithMany(p => p.Commandes)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Commande_Client");
        });

        modelBuilder.Entity<ContratTravail>(entity =>
        {
            entity.ToTable("ContratTravail");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("created_on");
            entity.Property(e => e.Debut)
                .HasColumnType("datetime")
                .HasColumnName("debut");
            entity.Property(e => e.Description)
                .HasMaxLength(4000)
                .HasColumnName("description");
            entity.Property(e => e.Fin)
                .HasColumnType("datetime")
                .HasColumnName("fin");
            entity.Property(e => e.Libelle)
                .HasMaxLength(100)
                .HasColumnName("libelle");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasColumnType("datetime")
                .HasColumnName("modified_on");
            entity.Property(e => e.SalaireBrutMensuel)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("salaire_brut_mensuel");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Type)
                .HasComment("CDI;CDD;Stage;Alternance;Journalier")
                .HasColumnName("type");
        });

        modelBuilder.Entity<Employe>(entity =>
        {
            entity.ToTable("Employe");

            entity.HasIndex(e => e.Id, "IX_Employe");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("created_on");
            entity.Property(e => e.DateNaissance)
                .HasColumnType("datetime")
                .HasColumnName("date_naissance");
            entity.Property(e => e.Description)
                .HasMaxLength(4000)
                .HasColumnName("description");
            entity.Property(e => e.FonctionId).HasColumnName("fonctionId");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(2000)
                .HasColumnName("imageURL");
            entity.Property(e => e.LocalisationId).HasColumnName("localisationId");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasColumnType("datetime")
                .HasColumnName("modified_on");
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .HasColumnName("nom");
            entity.Property(e => e.Prenom)
                .HasMaxLength(200)
                .HasColumnName("prenom");
            entity.Property(e => e.ServiceId).HasColumnName("serviceId");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Fonction).WithMany(p => p.Employes)
                .HasForeignKey(d => d.FonctionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employe_Fonction");

            entity.HasOne(d => d.Localisation).WithMany(p => p.Employes)
                .HasForeignKey(d => d.LocalisationId)
                .HasConstraintName("FK_Employe_Localisation");

            entity.HasOne(d => d.Service).WithMany(p => p.Employes)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employe_Service");
        });

        modelBuilder.Entity<FactureChargeFbde>(entity =>
        {
            entity.ToTable("FactureChargeFBDE");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.ContratTravailId).HasColumnName("contratTravailId");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("created_on");
            entity.Property(e => e.Description)
                .HasMaxLength(4000)
                .HasColumnName("description");
            entity.Property(e => e.Libelle)
                .HasMaxLength(100)
                .HasColumnName("libelle");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasColumnType("datetime")
                .HasColumnName("modified_on");
            entity.Property(e => e.Montant)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("montant");
            entity.Property(e => e.ServiceId).HasColumnName("serviceId");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TypeCharge)
                .HasComment("Salaires;Electricité;Eau;Gaz;...")
                .HasColumnName("typeCharge");

            entity.HasOne(d => d.ContratTravail).WithMany(p => p.FactureChargeFbdes)
                .HasForeignKey(d => d.ContratTravailId)
                .HasConstraintName("FK_FactureChargeFBDE_ContratTravail");

            entity.HasOne(d => d.Service).WithMany(p => p.FactureChargeFbdes)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK_FactureChargeFBDE_Service");
        });

        modelBuilder.Entity<FactureCommandeClient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_FactureClient");

            entity.ToTable("FactureCommandeClient");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.CommandeId).HasColumnName("commandeId");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("created_on");
            entity.Property(e => e.Description)
                .HasMaxLength(4000)
                .HasColumnName("description");
            entity.Property(e => e.Libelle)
                .HasMaxLength(100)
                .HasColumnName("libelle");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasColumnType("datetime")
                .HasColumnName("modified_on");
            entity.Property(e => e.Montant)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("montant");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Commande).WithMany(p => p.FactureCommandeClients)
                .HasForeignKey(d => d.CommandeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FactureCommandeClient_Commande");
        });

        modelBuilder.Entity<FactureProjetClient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_FactureProjet");

            entity.ToTable("FactureProjetClient");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("created_on");
            entity.Property(e => e.Description)
                .HasMaxLength(4000)
                .HasColumnName("description");
            entity.Property(e => e.Libelle)
                .HasMaxLength(100)
                .HasColumnName("libelle");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasColumnType("datetime")
                .HasColumnName("modified_on");
            entity.Property(e => e.Montant)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("montant");
            entity.Property(e => e.ProjetId).HasColumnName("projetId");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Projet).WithMany(p => p.FactureProjetClients)
                .HasForeignKey(d => d.ProjetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FactureProjetClient_projet");
        });

        modelBuilder.Entity<Fonction>(entity =>
        {
            entity.ToTable("Fonction");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("created_on");
            entity.Property(e => e.Description)
                .HasMaxLength(4000)
                .HasColumnName("description");
            entity.Property(e => e.Libelle)
                .HasMaxLength(100)
                .HasColumnName("libelle");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasColumnType("datetime")
                .HasColumnName("modified_on");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<LigneCommande>(entity =>
        {
            entity.ToTable("LigneCommande");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CommandeId).HasColumnName("commandeId");
            entity.Property(e => e.ProduitId).HasColumnName("produitId");

            entity.HasOne(d => d.Commande).WithMany(p => p.LigneCommandes)
                .HasForeignKey(d => d.CommandeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LigneCommande_Commande");

            entity.HasOne(d => d.Produit).WithMany(p => p.LigneCommandes)
                .HasForeignKey(d => d.ProduitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LigneCommande_Produit");
        });

        modelBuilder.Entity<Localisation>(entity =>
        {
            entity.ToTable("Localisation");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Adresse1)
                .HasMaxLength(200)
                .HasColumnName("adresse1");
            entity.Property(e => e.Adresse2)
                .HasMaxLength(200)
                .HasColumnName("adresse2");
            entity.Property(e => e.Adresse3)
                .HasMaxLength(200)
                .HasColumnName("adresse3");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.CodePostal)
                .HasMaxLength(100)
                .HasColumnName("code_postal");
            entity.Property(e => e.Continent)
                .HasMaxLength(100)
                .HasColumnName("continent");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("created_on");
            entity.Property(e => e.Departement)
                .HasMaxLength(100)
                .HasColumnName("departement");
            entity.Property(e => e.LocalisationGps)
                .HasMaxLength(200)
                .HasColumnName("localisation_gps");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasColumnType("datetime")
                .HasColumnName("modified_on");
            entity.Property(e => e.Pays)
                .HasMaxLength(100)
                .HasColumnName("pays");
            entity.Property(e => e.Region)
                .HasMaxLength(100)
                .HasColumnName("region");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Ville)
                .HasMaxLength(100)
                .HasColumnName("ville");
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.ToTable("Module");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("created_on");
            entity.Property(e => e.Description)
                .HasMaxLength(4000)
                .HasColumnName("description");
            entity.Property(e => e.Libelle)
                .HasMaxLength(50)
                .HasColumnName("libelle");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasColumnType("datetime")
                .HasColumnName("modified_on");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.ToTable("Notification");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.Corps)
                .HasMaxLength(4000)
                .HasColumnName("corps");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("created_on");
            entity.Property(e => e.Destinataires)
                .HasMaxLength(4000)
                .HasColumnName("destinataires");
            entity.Property(e => e.Expediteur)
                .HasMaxLength(200)
                .HasComment("")
                .HasColumnName("expediteur");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasColumnType("datetime")
                .HasColumnName("modified_on");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Titre)
                .HasMaxLength(100)
                .HasColumnName("titre");
        });

        modelBuilder.Entity<NotificationPieceJointe>(entity =>
        {
            entity.ToTable("Notification_PieceJointe");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NotificationId).HasColumnName("notificationId");
            entity.Property(e => e.PieceJointeId).HasColumnName("pieceJointeId");

            entity.HasOne(d => d.Notification).WithMany(p => p.NotificationPieceJointes)
                .HasForeignKey(d => d.NotificationId)
                .HasConstraintName("FK_Notification_PieceJointe_Notification");

            entity.HasOne(d => d.PieceJointe).WithMany(p => p.NotificationPieceJointes)
                .HasForeignKey(d => d.PieceJointeId)
                .HasConstraintName("FK_Notification_PieceJointe_PieceJointe");
        });

        modelBuilder.Entity<PanierAchat>(entity =>
        {
            entity.ToTable("PanierAchat");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("created_on");
            entity.Property(e => e.Description)
                .HasMaxLength(4000)
                .HasColumnName("description");
            entity.Property(e => e.Libelle)
                .HasMaxLength(100)
                .HasColumnName("libelle");
            entity.Property(e => e.ListeProduitsId)
                .HasMaxLength(1000)
                .HasColumnName("listeProduitsId");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasColumnType("datetime")
                .HasColumnName("modified_on");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UtilisateurId).HasColumnName("utilisateurId");

            entity.HasOne(d => d.Utilisateur).WithMany(p => p.PanierAchats)
                .HasForeignKey(d => d.UtilisateurId)
                .HasConstraintName("FK_PanierAchat_Utilisateur");
        });

        modelBuilder.Entity<PieceJointe>(entity =>
        {
            entity.ToTable("PieceJointe");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.ContratTravailId).HasColumnName("contratTravailId");
            entity.Property(e => e.FactureId).HasColumnName("factureId");
            entity.Property(e => e.LienFtp)
                .HasMaxLength(2000)
                .HasColumnName("lien_ftp");
            entity.Property(e => e.LienLocal)
                .HasMaxLength(2000)
                .HasColumnName("lien_local");
            entity.Property(e => e.ProduitId).HasColumnName("produitId");
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .HasColumnName("type");

            entity.HasOne(d => d.Produit).WithMany(p => p.PieceJointes)
                .HasForeignKey(d => d.ProduitId)
                .HasConstraintName("FK_PieceJointe_Produit");
        });

        modelBuilder.Entity<Planning>(entity =>
        {
            entity.ToTable("Planning");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.Contenu)
                .HasMaxLength(4000)
                .HasColumnName("contenu");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("created_on");
            entity.Property(e => e.Debut)
                .HasColumnType("datetime")
                .HasColumnName("debut");
            entity.Property(e => e.Fin)
                .HasColumnType("datetime")
                .HasColumnName("fin");
            entity.Property(e => e.Invites)
                .HasMaxLength(4000)
                .HasColumnName("invites");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasColumnType("datetime")
                .HasColumnName("modified_on");
            entity.Property(e => e.Organisateur)
                .HasMaxLength(100)
                .HasColumnName("organisateur");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Titre)
                .HasMaxLength(200)
                .HasColumnName("titre");
            entity.Property(e => e.Type)
                .HasComment("Projet;Prospection;...")
                .HasColumnName("type");
        });

        modelBuilder.Entity<Produit>(entity =>
        {
            entity.ToTable("Produit");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("created_on");
            entity.Property(e => e.Description)
                .HasMaxLength(4000)
                .HasColumnName("description");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(2000)
                .HasColumnName("imageURL");
            entity.Property(e => e.Libelle)
                .HasMaxLength(100)
                .HasColumnName("libelle");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasColumnType("datetime")
                .HasColumnName("modified_on");
            entity.Property(e => e.PrixHt)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("prixHT");
            entity.Property(e => e.PrixTtc)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("prixTTC");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<Projet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_projet");

            entity.ToTable("Projet");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("clientId");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.Cout)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("cout");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("created_on");
            entity.Property(e => e.Debut)
                .HasColumnType("datetime")
                .HasColumnName("debut");
            entity.Property(e => e.Description)
                .HasMaxLength(4000)
                .HasColumnName("description");
            entity.Property(e => e.Fin)
                .HasColumnType("datetime")
                .HasColumnName("fin");
            entity.Property(e => e.Libelle)
                .HasMaxLength(100)
                .HasColumnName("libelle");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasColumnType("datetime")
                .HasColumnName("modified_on");
            entity.Property(e => e.ServiceId).HasColumnName("serviceId");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Client).WithMany(p => p.Projets)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Projet_Client");

            entity.HasOne(d => d.Service).WithMany(p => p.Projets)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_projet_Service");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.ToTable("Service");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("created_on");
            entity.Property(e => e.Description)
                .HasMaxLength(4000)
                .HasColumnName("description");
            entity.Property(e => e.Libelle)
                .HasMaxLength(50)
                .HasColumnName("libelle");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasColumnType("datetime")
                .HasColumnName("modified_on");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.ToTable("Utilisateur");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("clientId");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("created_on");
            entity.Property(e => e.Description)
                .HasMaxLength(4000)
                .HasColumnName("description");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .HasColumnName("email");
            entity.Property(e => e.EmployeId).HasColumnName("employeId");
            entity.Property(e => e.Login)
                .HasMaxLength(100)
                .HasColumnName("login");
            entity.Property(e => e.Mdp)
                .HasMaxLength(100)
                .HasColumnName("mdp");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasColumnType("datetime")
                .HasColumnName("modified_on");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Telephone)
                .HasMaxLength(100)
                .HasColumnName("telephone");

            entity.HasOne(d => d.Client).WithMany(p => p.Utilisateurs)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK_Utilisateur_Client");

            entity.HasOne(d => d.Employe).WithMany(p => p.Utilisateurs)
                .HasForeignKey(d => d.EmployeId)
                .HasConstraintName("FK_Utilisateur_Employe");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
