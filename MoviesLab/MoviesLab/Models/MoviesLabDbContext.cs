using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;



namespace MoviesLab.Models
{
    public class MoviesLabDbContext : IdentityDbContext<MoviesLabUser>
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<FilmCrew> FilmCrew { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<CrewPosition> CrewPositions { get; set; }

        public MoviesLabDbContext()
            : base("MoviesLabConnection")
        {
        }
    }
}