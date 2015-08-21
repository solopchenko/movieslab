using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesLab.Models
{
    public class Actor
    {
        //Id персоны
        [Key, Column(Order = 0)]
        [Display(Name = "Персона")]
        public int PersonId { get; set; }

        //Id фильма
        [Key, Column(Order = 1)]
        [Display(Name = "Фильм")]
        public int MovieId { get; set; }

        //Роль
        [Key, Column(Order = 2)]
        [Required(ErrorMessage = "Обязательное поле.")]
        [Display(Name = "Роль в фильме")]
        public string Character { get; set; }

        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }

        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }
    }
}