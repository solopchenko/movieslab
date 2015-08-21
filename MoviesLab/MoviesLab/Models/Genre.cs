using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesLab.Models
{
    public class Genre
    {
        [Key]
        [Display(Name = "Id жанра")]
        public int GenreId { get; set; }

        [Display(Name = "Имя жанра")]
        public string Name { get; set; }

        //Один жанр может быть у нескольких фильмов
        public virtual ICollection<Movie> Movie { get; set; }
    }
}