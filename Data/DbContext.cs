using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EduProject.Models;

    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbContext (DbContextOptions<DbContext> options)
            : base(options)
        {
        }

        public DbSet<EduProject.Models.Sınav> Sınav { get; set; }

        public DbSet<EduProject.Models.ForumBaslik> ForumBaslik { get; set; }

        public DbSet<EduProject.Models.ForumComment> ForumComment { get; set; }

        public DbSet<EduProject.Models.Ilan> Ilan { get; set; }

        public DbSet<EduProject.Models.DersNotu> DersNotu { get; set; }

        public DbSet<EduProject.Models.Ders> Ders { get; set; }

        public DbSet<EduProject.Models.Duyuru> Duyuru { get; set; }

        public DbSet<EduProject.Models.User> User { get; set; }


    }
