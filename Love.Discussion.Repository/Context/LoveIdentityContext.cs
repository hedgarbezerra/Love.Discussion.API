using Love.Discussion.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Love.Discussion.Repository.Context
{
    public class LoveIdentityContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        public LoveIdentityContext(DbContextOptions<LoveIdentityContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region mapeamento da entidade Meeting 
            builder.Entity<Meeting>(map =>
            {
                map.ToTable("TbMeetings");
                map.HasKey(m => m.Id).IsClustered();
                map.Property(m => m.Id).ValueGeneratedOnAdd();

                map.HasMany(g => g.Complains)
                .WithOne(s => s.Meeting)
                .HasForeignKey(s => s.IdMeeting);

            });

            #endregion


            #region Mapeamento da entidade Complain
            builder.Entity<Complain>(map =>
            {
                map.ToTable("TbComplains");
                map.HasKey(m => m.Id).IsClustered();
                map.Property(m => m.Id).ValueGeneratedOnAdd();

                map.Property(p => p.IsPositive)
                .HasColumnType("bit");

                map.Property(p => p.Extra)
                .HasColumnType("varchar")
                .HasMaxLength(5000)
                .IsRequired(false);

                map.Property(p => p.Title)
                .HasColumnType("varchar")
                .HasMaxLength(12);

                map.Property(p => p.Text)
                .HasColumnType("varchar")
                .HasMaxLength(500);
            });



            #endregion

        }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Complain> Complains { get; set; }
    }
}
