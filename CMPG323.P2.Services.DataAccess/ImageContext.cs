using CMPG323.P2.Services.Library.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMPG323.P2.Services.DataAccess
{
    public class ImageContext : DbContext
    {

        #region Constructors

        public ImageContext(DbContextOptions<ImageContext> options)
            : base(options)
        {
        }

        #endregion

        #region Setup Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Can be used to convert a list of string values to a single semi-colon delimited string
            ValueConverter splitStringConverter = new ValueConverter<List<string>, string>(v => string.Join(";", v), v => v.Split(new[] { ';' }).ToList());
            ValueComparer splitStringComparer = new ValueComparer<List<string>>((c1, c2) => c1.SequenceEqual(c2), c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())), c => c.ToList());

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Username)
                    .IsUnique();

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Password)
                    .IsRequired();
            });

            modelBuilder.Entity<FileItem>(entity =>
            {
                entity.HasIndex(e => e.Guid)
                    .IsUnique();

                entity.Property(e => e.DateCaptured)
                    .HasColumnType("datetime");

                entity.Property(e => e.ContentType)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Extension)
                    .IsRequired()
                    .HasMaxLength(8);

                entity.Property(e => e.Guid)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Tags)
                  .HasConversion(splitStringConverter, splitStringComparer);


            });
        }

        #endregion

        #region DbSets

        public DbSet<FileItem> FileItems { get; set; }

        public DbSet<User> Users { get; set; }

        #endregion

    }
}
