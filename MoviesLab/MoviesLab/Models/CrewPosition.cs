using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesLab.Models
{
    public class CrewPosition
    {
        [Key]
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Id должности")]
        public int PositionId { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Должность")]
        public string Name { get; set; }
    }
}