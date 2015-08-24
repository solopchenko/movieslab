using System.ComponentModel.DataAnnotations;

namespace MoviesLab.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessage = "Обязательное поле.")]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        //[Display(Name = "Фамилия")]
        //[Required(ErrorMessage = "Обязательное поле.")]
        //public string Surname { get; set; }

        //[Display(Name = "Имя")]
        //[Required(ErrorMessage = "Обязательное поле.")]
        //public string Name { get; set; }

        [Required(ErrorMessage = "Обязательное поле.")]
        [DataType(DataType.Password)]
        [Display(Name = "Текущий пароль")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Обязательное поле.")]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение нового пароля")]
        [Compare("NewPassword", ErrorMessage = "Новый пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Обязательное поле.")]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Обязательное поле.")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Обязательное поле.")]
        public string Surname { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Обязательное поле.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обязательное поле.")]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Обязательное поле.")]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }
    }
}



//using System.ComponentModel.DataAnnotations;

//namespace MoviesLab.Models
//{
//    public class ExternalLoginConfirmationViewModel
//    {
//        [Required]
//        [Display(Name = "Имя пользователя")]
//        public string UserName { get; set; }
//    }

//public class ManageUserViewModel
//{
//    [Required]
//    [DataType(DataType.Password)]
//    [Display(Name = "Текущий пароль")]
//    public string OldPassword { get; set; }

//    [Required]
//    [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
//    [DataType(DataType.Password)]
//    [Display(Name = "Новый пароль")]
//    public string NewPassword { get; set; }

//    [DataType(DataType.Password)]
//    [Display(Name = "Подтверждение нового пароля")]
//    [Compare("NewPassword", ErrorMessage = "Новый пароль и его подтверждение не совпадают.")]
//    public string ConfirmPassword { get; set; }
//}

//public class LoginViewModel
//{
//    [Required]
//    [Display(Name = "Имя пользователя")]
//    public string UserName { get; set; }

//    [Required]
//    [DataType(DataType.Password)]
//    [Display(Name = "Пароль")]
//    public string Password { get; set; }

//    [Display(Name = "Запомнить меня")]
//    public bool RememberMe { get; set; }
//}

//public class RegisterViewModel
//{
//    [Required]
//    [Display(Name = "Имя пользователя")]
//    public string UserName { get; set; }

//    [Required]
//    [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
//    [DataType(DataType.Password)]
//    [Display(Name = "Пароль")]
//    public string Password { get; set; }

//    [DataType(DataType.Password)]
//    [Display(Name = "Подтверждение пароля")]
//    [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
//    public string ConfirmPassword { get; set; }
//}
//}
