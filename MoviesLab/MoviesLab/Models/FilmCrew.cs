using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesLab.Models
{
    public class FilmCrew
    {
        //Id персоны
        [Key, Column(Order = 0)]
        public int PersonId { get; set; }

        //Id фильма
        [Key, Column(Order = 1)]
        public int MovieId { get; set; }

        //Должность
        [Key, Column(Order = 2)]
        public int PositionId { get; set; }

        [ForeignKey("PositionId")]
        public virtual CrewPosition CrewPosition { get; set; }

        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }

        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }
    }
}