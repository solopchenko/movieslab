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
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class MoviesLabUser : IdentityUser
    {
        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Обязательное поле.")]
        public string Surname { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Обязательное поле.")]
        public string Name { get; set; }

        [Display(Name = "Избранные фильмы")]
        public virtual ICollection<Movie> FavMovie { get; set; }

        [Display(Name = "Избранные персоны")]
        public virtual ICollection<Person> FavPerson { get; set; }

        public string FullName
        {
            get
            {
                return Name + " " + Surname;
            }
        }

        public bool isAdmin
        {
            get
            {
                if (Roles.Count > 0)
                {
                    foreach (var r in Roles)
                    {
                        if (r.Role.Name == "Администратор")
                            return true;
                    }
                }

                return false;
            }
        }

        //public bool ChangeName(string newName, string newSurname)
        //{
        //    if ((newName != String.Empty) & (newSurname != String.Empty))
        //    {
        //        Name = newName;
        //        Surname = newSurname;
        //        return true;
        //    }
        //    else
        //        return false;
        //}
    }
}