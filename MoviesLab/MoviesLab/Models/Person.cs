using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesLab.Models
{
    public class Person
    {
        [Key]
        [Display(Name = "Id персоны")]
        public int PersonId { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Обязательное поле.")]
        public string Name { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Обязательное поле.")]
        public string Surname { get; set; }

        
        [Display(Name = "Краткая биография")]
        [DataType(DataType.MultilineText)]
        public string Biography { get; set; }

        [Display(Name = "Дата рождения")]
        [Required(ErrorMessage = "Обязательное поле.")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> Birthday { get; set; }

        [Display(Name = "Дата смерти")]
        public Nullable<System.DateTime> Obit { get; set; }

        [Display(Name = "Место рождения")]
        public string CountryId { get; set; }

        [Display(Name = "Удален")]
        [Required(ErrorMessage = "Обязательное поле.")]
        public bool Delete { get; set; }

        //Относится только к одной стране
        [ForeignKey("CountryId")]
        [Display(Name = "Место рождения")]
        public virtual Country Country { get; set; }

        //Может сыграть много ролей
        [Display(Name = "Роли в фильмах")]
        public virtual ICollection<Actor> Actor { get; set; }

        //Может иметь несколько должностей //этого не было
        [Display(Name = "Участие в фильмах")]
        public virtual ICollection<FilmCrew> FilmCrew { get; set; }

        [Display(Name = "Пользователи, добавившие персону в избранное")]
        public virtual ICollection<MoviesLabUser> User { get; set; }

        //Полное имя
        [Display(Name = "Полное имя")]
        public string Fullname
        {
            get
            {
                if (Patronymic == null)
                    return string.Format("{0} {1}", Name, Surname);
                else
                    return string.Format("{0} {1} {2}", Name, Patronymic, Surname);
            }
        }

        //Полное имя с годами
        public string FullnameWithYears
        {
            get
            {
                //Если нет отчества
                if (Patronymic == null)
                    if (Birthday != null & Obit == null)
                        return string.Format("{0} {1} ({2})", Name, Surname, Birthday.Value.Year);
                    else
                        if (Birthday != null & Obit != null)
                            return string.Format("{0} {1} ({2} - {3})", Name, Surname, Birthday.Value.Year, Obit.Value.Year);
                        else
                            if (Birthday == null & Obit != null)
                                return string.Format("{0} {1} ( - {2})", Name, Surname, Obit.Value.Year);
                            else
                                return string.Format("{0} {1}", Name, Surname);
                else
                    if (Birthday != null & Obit == null)
                        return string.Format("{0} {1} {2} ({3})", Name, Patronymic, Surname, Birthday.Value.Year);
                    else
                        if (Birthday != null & Obit != null)
                            return string.Format("{0} {1} {2} ({3} - {4})", Name, Patronymic, Surname, Birthday.Value.Year, Obit.Value.Year);
                        else
                            if (Birthday == null & Obit != null)
                                return string.Format("{0} {1} {2} ( - {3})", Name, Patronymic, Surname, Obit.Value.Year);
                            else
                                return string.Format("{0} {1} {2}", Name, Patronymic, Surname);
            }
        }


        //Возраст
        [Display(Name = "Возраст")]
        public int? Age 
        {
            get
            {
                if (Birthday != null & Obit == null)
                {
                    int age = DateTime.Now.Year - Birthday.Value.Year;
                    if (Birthday > DateTime.Now.AddYears(-age))
                        age--;
                    return age;
                }
                else
                    if (Birthday != null & Obit != null)
                    {
                        int age = Obit.Value.Year - Birthday.Value.Year;
                        if (Birthday > Obit.Value.AddYears(-age))
                            age--;
                        return age;
                    }
                    else
                        return null;
            }
        }
    }
}