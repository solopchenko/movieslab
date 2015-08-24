using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesLab.Models
{
    public class Movie
    {
        [Key]
        [Display(Name = "Id фильма")]
        public int MovieId { get; set; }
        
        [Display(Name = "Название")]
        [Required(ErrorMessage = "Обязательное поле.")]
        public string Title { get; set; }
        
        [Display(Name = "Описание")]
        [DataType (DataType.MultilineText)]
        public string Description { get; set; }
       
        [Display(Name = "Дата выхода")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> Date { get; set; }

        [Display(Name = "Длительность")]
        [Required(ErrorMessage = "Обязательное поле.")]
        [Range(0, int.MaxValue, ErrorMessage="Отрицательные числа не допускаются")]
        public int Duration { get; set; }

        [Display(Name = "Удален")]
        [Required(ErrorMessage = "Обязательное поле.")]
        public bool Delete { get; set; }


        //В фильмах играют много ролей
        [Display(Name = "Актеры")]
        public virtual ICollection<Actor> Actor { get; set; }
        //В фильмах участвет много людей
        [Display(Name = "Съемочная группа")]
        public virtual ICollection<FilmCrew> FilmCrew { get; set; }
        
        //Фильм может относиться к нескольким жанрам
        [Display(Name = "Жанры")]
        public virtual ICollection<Genre> Genre { get; set; }
        //Съёмки фильма могут происходить в нескольких странах
        [Display(Name = "Страны")]
        public virtual ICollection<Country> Country { get; set; }

        [Display(Name = "Пользователи, добавившие фильм в избранное")]
        public virtual ICollection<MoviesLabUser> User { get; set; }

        //Название фильма с годом выхода
        public string TitleWithYear
        {
            get
            {
                if (Date == null)
                    return Title;
                else
                    return string.Format("{0} ({1})", Title, Date.Value.Year);
            }
        }

        //Перечень жанров
        [Display(Name = "Жанры")]
        public string Genres
        {
            get
            {
                if (Genre.Count > 0)
                {
                    string str = string.Empty;

                    var listOfGenres = Genre.ToList();

                    int numbersOfGenres = listOfGenres.Count();

                    for (int i = 0; i < numbersOfGenres - 1; i++)
                    {
                        str = str + listOfGenres[i].Name + ", ";
                    }

                    str = str + listOfGenres[numbersOfGenres - 1].Name;

                    return str;
                }
                else
                    return String.Empty;
            }
        }

        //Перечень стран
        [Display(Name = "Страны")]
        public string Countries
        {
            get
            {
                if (Country.Count > 0)
                {
                    string str = string.Empty;

                    var listOfCountries = Country.ToList();

                    int numbersOfCountries = listOfCountries.Count();

                    for (int i = 0; i < numbersOfCountries - 1; i++)
                    {
                        str = str + listOfCountries[i].Name + ", ";
                    }

                    str = str + listOfCountries[numbersOfCountries - 1].Name;

                    return str;
                }
                else
                    return String.Empty;
            }
        }
    }
}