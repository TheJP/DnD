using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DnD.Data;

namespace DnD.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20160719152224_AttributeRework")]
    partial class AttributeRework
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DnD.Models.Adventure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DungeonMasterId")
                        .IsRequired();

                    b.Property<string>("Name");

                    b.Property<int?>("NextId");

                    b.Property<int?>("PreviousId");

                    b.HasKey("Id");

                    b.HasIndex("DungeonMasterId");

                    b.HasIndex("NextId")
                        .IsUnique();

                    b.ToTable("Adventures");
                });

            modelBuilder.Entity("DnD.Models.AdventureParticipation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AdventureId");

                    b.Property<int>("AdventurerId");

                    b.HasKey("Id");

                    b.HasIndex("AdventureId");

                    b.HasIndex("AdventurerId");

                    b.ToTable("AdventureParticipations");
                });

            modelBuilder.Entity("DnD.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("DisplayName")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("DnD.Models.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Gender")
                        .IsRequired();

                    b.Property<int>("InitialLife");

                    b.Property<int>("InitialMana");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("OwnerId")
                        .IsRequired();

                    b.Property<int>("RaceId");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("RaceId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("DnD.Models.DnDAttribute", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Order");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 3);

                    b.Property<int>("Special")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<int>("Type")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(1);

                    b.HasKey("Id");

                    b.ToTable("Attributes");
                });

            modelBuilder.Entity("DnD.Models.Experience", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CharacterId");

                    b.Property<string>("Description");

                    b.Property<int?>("LootFromId");

                    b.Property<int>("Value");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.HasIndex("LootFromId");

                    b.ToTable("Experience");
                });

            modelBuilder.Entity("DnD.Models.Gold", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CharacterId");

                    b.Property<string>("Description");

                    b.Property<int?>("LootFromId");

                    b.Property<int>("Value");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.HasIndex("LootFromId");

                    b.ToTable("Gold");
                });

            modelBuilder.Entity("DnD.Models.Race", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDragon");

                    b.Property<string>("Lore");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Races");
                });

            modelBuilder.Entity("DnD.Models.RaceAttribute", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AttributeId");

                    b.Property<int>("RaceId");

                    b.Property<int>("Value");

                    b.HasKey("Id");

                    b.HasIndex("AttributeId");

                    b.HasIndex("RaceId");

                    b.ToTable("RaceAttributes");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("DnD.Models.Adventure", b =>
                {
                    b.HasOne("DnD.Models.ApplicationUser", "DungeonMaster")
                        .WithMany("Adventures")
                        .HasForeignKey("DungeonMasterId");

                    b.HasOne("DnD.Models.Adventure", "Next")
                        .WithOne("Previous")
                        .HasForeignKey("DnD.Models.Adventure", "NextId");
                });

            modelBuilder.Entity("DnD.Models.AdventureParticipation", b =>
                {
                    b.HasOne("DnD.Models.Adventure", "Adventure")
                        .WithMany("Adventurers")
                        .HasForeignKey("AdventureId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DnD.Models.Character", "Adventurer")
                        .WithMany("Adventures")
                        .HasForeignKey("AdventurerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DnD.Models.Character", b =>
                {
                    b.HasOne("DnD.Models.ApplicationUser", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DnD.Models.Race", "Race")
                        .WithMany("Characters")
                        .HasForeignKey("RaceId");
                });

            modelBuilder.Entity("DnD.Models.Experience", b =>
                {
                    b.HasOne("DnD.Models.Character", "Character")
                        .WithMany("Experience")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DnD.Models.AdventureParticipation", "LootFrom")
                        .WithMany("ExperienceLoot")
                        .HasForeignKey("LootFromId");
                });

            modelBuilder.Entity("DnD.Models.Gold", b =>
                {
                    b.HasOne("DnD.Models.Character", "Character")
                        .WithMany("Gold")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DnD.Models.AdventureParticipation", "LootFrom")
                        .WithMany("GoldLoot")
                        .HasForeignKey("LootFromId");
                });

            modelBuilder.Entity("DnD.Models.RaceAttribute", b =>
                {
                    b.HasOne("DnD.Models.DnDAttribute", "Attribute")
                        .WithMany()
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DnD.Models.Race", "Race")
                        .WithMany("Attributes")
                        .HasForeignKey("RaceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DnD.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DnD.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DnD.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
