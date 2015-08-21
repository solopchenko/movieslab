using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesLab.Models
{
    public class Country
    {
        [Key]
        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Код страны должен состоять из 2 символов")]
        [Display(Name = "Код страны")]
        public string CountryId { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Наименование страны")]
        public string Name { get; set; }

        //В странах живет много людей
        public virtual ICollection<Person> Person { get; set; }

        //В странах снимают много фильмов
        public virtual ICollection<Movie> Movie { get; set; }
    }
}