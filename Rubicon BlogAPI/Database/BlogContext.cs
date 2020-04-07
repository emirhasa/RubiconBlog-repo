using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rubicon_BlogAPI.Database
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostTag> PostTags { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.PostId);
                entity.HasIndex(e => e.Slug).IsUnique();

                entity.HasData(
                    
                    new Post {
                        Title = "Đonko Šadžočkić piše novosti o ćevapima",
                        Slug = "donko-sadzockic-pise-novosti-o-cevapima",
                        Description = "Peču ćevapčiće putem Android Đaplikacije",
                        Body = "Ćevapi, poznati i kao ćevapčići, su jelo od mljevenog mesa, popularno u Bosni i Hercegovini i drugim zemljama Balkana još od doba Osmanskog Carstva.",
                        CreatedAt = DateTime.Now.ToUniversalTime(),
                        UpdatedAt = DateTime.Now.ToUniversalTime(),
                        PostId = 1
                    },
                    new Post
                    {
                        Title = "Baščaršija ima buregdžinice",
                        Slug = "bascarsija-ima-buregdzinice",
                        Description = "Peču željanice putem iOS Đaplikacije",
                        Body = "Pitama rijetko ko može odoljeti, a čini se da podjednako volimo sve: od sira, zelja, krompira, mesa...Osim onih u zvrku, sa domaćom jufkom, možete napraviti i polagane, koje su podjednako ukusne.",
                        CreatedAt = DateTime.Now.ToUniversalTime(),
                        UpdatedAt = DateTime.Now.ToUniversalTime(),
                        PostId = 2
                    },
                    new Post
                    {
                        Title = "Đonko Šadžočkić o Sarajevskoj raji i mahalanju",
                        Slug = "donko-sadzockic-o-sarajevskoj-raji-i-mahalanju",
                        Description = "Sarajevska mahala predstavlja endemsko okruzenje specifično za BiH, treba je čuvati",
                        Body = "Sarajevo je formirano tako što je u dolini izgrađena Baščaršija, sjedište zanatstva i trgovine, dok su rezidencijalni dijelovi starog Sarajeva, sarajevske mahale, građene na padinama okolnih brda.",
                        CreatedAt = DateTime.Now.ToUniversalTime(),
                        UpdatedAt = DateTime.Now.ToUniversalTime(),
                        PostId = 3
                    },
                    new Post
                    {
                        Title = "Roštiljdžija čami nad pljeskavicama",
                        Slug = "rostiljdzija-cami-nad-pljeskavicama",
                        Description = "Ćevapi, poznati i kao ćevapčići, su jelo od mljevenog mesa",
                        Body = "Ćevapi, poznati i kao ćevapčići, su jelo od mljevenog mesa popularno u Bosni i Hercegovini i drugim zemljama Balkana još od doba Osmanskog Carstva.",
                        CreatedAt = DateTime.Now.ToUniversalTime(),
                        UpdatedAt = DateTime.Now.ToUniversalTime(),
                        PostId = 4
                    },
                    new Post
                    {
                        Title = "Šdžaferova odmarališta sve poznatija",
                        Slug = "sdzaferova-odmaralista-sve-poznatija",
                        Description = "Peču ćevapčiće putem Android Đaplikacije",
                        Body = "Ćevapi, poznati i kao ćevapčići, su jelo od mljevenog mesa, popularno u Bosni i Hercegovini i drugim zemljama Balkana još od doba Osmanskog Carstva.",
                        CreatedAt = DateTime.Now.ToUniversalTime(),
                        UpdatedAt = DateTime.Now.ToUniversalTime(),
                        PostId = 5
                    },
                    new Post
                    {
                        Title = "Augmented Reality iOS Application",
                        Slug = "augmented-reality-ios-application",
                        Description = "Rubicon Software Development and Gazzda furniture are proud to launch an augmented reality app.",
                        Body = "The app is simple to use, and will help you decide on your best furniture fit.",
                        CreatedAt = DateTime.Now.ToUniversalTime(),
                        UpdatedAt = DateTime.Now.ToUniversalTime(),
                        PostId = 6
                    }
                    );
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(e => e.Name);
                entity.HasData(
                        new Tag
                        {
                            Name = "Sarajevo"
                        },
                        new Tag
                        {
                            Name = "Android"
                        },
                        new Tag
                        {
                            Name= "iOS"
                        },
                        new Tag
                        {
                            Name = "IT"
                        },
                        new Tag
                        {
                            Name = "Rubicon"
                        },
                        new Tag
                        {
                            Name = "Informatika"
                        }
                    );
            });

            modelBuilder.Entity<PostTag>(entity =>
            {
                entity.HasKey(new string[] { "PostId", "TagId" });
                entity.HasOne(e => e.Post).WithMany(e => e.PostTags).HasForeignKey(e => e.PostId);
                entity.HasOne(e => e.Tag).WithMany(e => e.TagPosts).HasForeignKey(e => e.TagId);

                entity.HasData(
                    new PostTag
                    {
                        PostId = 1,
                        TagId = "Sarajevo"
                    },
                    new PostTag
                    {
                        PostId = 1,
                        TagId = "Android"
                    },
                    new PostTag
                    {
                        PostId = 2,
                        TagId = "Sarajevo"
                    },
                    new PostTag
                    {
                        PostId = 2,
                        TagId = "Android"
                    },
                    new PostTag
                    {
                        PostId = 3,
                        TagId = "IT"
                    },
                    new PostTag
                    {
                        PostId = 4,
                        TagId = "Android"
                    },
                    new PostTag
                    {
                        PostId = 5,
                        TagId = "iOS"
                    },
                    new PostTag
                    {
                        PostId = 6,
                        TagId = "Rubicon"
                    }
                );
            });

        }
    }
}
