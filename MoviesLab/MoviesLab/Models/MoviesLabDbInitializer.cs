using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MoviesLab.Models
{
    public class MoviesLabDbInitializer : DropCreateDatabaseAlways<MoviesLabDbContext>
    {
        protected override void Seed(MoviesLabDbContext db)
        {

            //ДОБАВЛЕНИЕ ЖАНРОВ
            Genre g1 = new Genre { Name = "Семейный" };
            Genre g2 = new Genre { Name = "Комедия" };
            Genre g3 = new Genre { Name = "Приключения" };
            Genre g4 = new Genre { Name = "Боевик" };
            Genre g5 = new Genre { Name = "Мелодрама" };
            Genre g6 = new Genre { Name = "Документальный" };
            Genre g7 = new Genre { Name = "Детский" };
            Genre g8 = new Genre { Name = "Мультфильм" };
            Genre g9 = new Genre { Name = "Фантастика" };
            Genre g10 = new Genre { Name = "Триллер" };
            Genre g11 = new Genre { Name = "Ужасы" };
            Genre g12 = new Genre { Name = "Военный" };
            Genre g13 = new Genre { Name = "Биография" };
            Genre g14 = new Genre { Name = "Короткометражка" };
            Genre g15 = new Genre { Name = "Детектив" };
            Genre g16 = new Genre { Name = "Драма" };
            Genre g17 = new Genre { Name = "Фэнтази" };

            db.Genres.Add(g1);
            db.Genres.Add(g2);
            db.Genres.Add(g3);
            db.Genres.Add(g4);
            db.Genres.Add(g5);
            db.Genres.Add(g6);
            db.Genres.Add(g7);
            db.Genres.Add(g8);
            db.Genres.Add(g9);
            db.Genres.Add(g10);
            db.Genres.Add(g11);
            db.Genres.Add(g12);
            db.Genres.Add(g13);
            db.Genres.Add(g14);
            db.Genres.Add(g15);
            db.Genres.Add(g16);
            db.Genres.Add(g17);

            //ДОБАВЛЕНИЕ СТРАН
            Country RU = new Country { Name = "Россия", CountryId = "RU" };
            Country US = new Country { Name = "США", CountryId = "US" };
            Country FR = new Country { Name = "Франция", CountryId = "FR" };
            Country DE = new Country { Name = "Германия", CountryId = "DE" };
            Country IT = new Country { Name = "Италия", CountryId = "IT" };
            Country CA = new Country { Name = "Канада", CountryId = "CA" };
            Country JP = new Country { Name = "Япония", CountryId = "JP" };
            Country BE = new Country { Name = "Бельгия", CountryId = "BE" };
            Country HU = new Country { Name = "Венгрия", CountryId = "HU" };
            Country GR = new Country { Name = "Греция", CountryId = "GR" };
            Country DK = new Country { Name = "Дания", CountryId = "DK" };
            Country EG = new Country { Name = "Египет", CountryId = "EG" };
            Country IE = new Country { Name = "Ирландия", CountryId = "IE" };
            Country KZ = new Country { Name = "Казахстан", CountryId = "KZ" };
            Country LV = new Country { Name = "Латвия", CountryId = "LV" };
            Country ES = new Country { Name = "Испания", CountryId = "ES" };
            Country LU = new Country { Name = "Люксембург", CountryId = "LU" };
            Country CH = new Country { Name = "Китай", CountryId = "CH" };
            Country MX = new Country { Name = "Мексика", CountryId = "MX" };
            Country UK = new Country { Name = "Великобритания", CountryId = "UK" };
            Country AU = new Country { Name = "Австралия", CountryId = "AU" };

            db.Countries.Add(RU);
            db.Countries.Add(US);
            db.Countries.Add(FR);
            db.Countries.Add(DE);
            db.Countries.Add(IT);
            db.Countries.Add(CA);
            db.Countries.Add(JP);
            db.Countries.Add(BE);
            db.Countries.Add(HU);
            db.Countries.Add(GR);
            db.Countries.Add(DK);
            db.Countries.Add(EG);
            db.Countries.Add(IE);
            db.Countries.Add(KZ);
            db.Countries.Add(LV);
            db.Countries.Add(ES);
            db.Countries.Add(LU);
            db.Countries.Add(CH);
            db.Countries.Add(MX);
            db.Countries.Add(UK);
            db.Countries.Add(AU);


            //ДОБАВЛЕНИЕ ДОЛЖНОСТЕЙ СЪЕМОЧНОЙ ГРУППЫ
            CrewPosition cp1 = new CrewPosition { Name = "Режиссёр" };
            CrewPosition cp2 = new CrewPosition { Name = "Сценарист" };
            CrewPosition cp3 = new CrewPosition { Name = "Продюсер" };
            CrewPosition cp4 = new CrewPosition { Name = "Оператор" };
            CrewPosition cp5 = new CrewPosition { Name = "Композитор" };
            CrewPosition cp6 = new CrewPosition { Name = "Художник" };
            CrewPosition cp7 = new CrewPosition { Name = "Монтаж" };

            db.CrewPositions.Add(cp1);
            db.CrewPositions.Add(cp2);
            db.CrewPositions.Add(cp3);
            db.CrewPositions.Add(cp4);
            db.CrewPositions.Add(cp5);
            db.CrewPositions.Add(cp6);
            db.CrewPositions.Add(cp7);


            //ДОБАВЛЕНИЕ ПЕРСОН
            Person DL = new Person { Name = "Дженнифер", Surname = "Лоуренс", Birthday = new DateTime(1990, 08, 15), Delete = false };
            db.People.Add(DL);
            Person DH = new Person { Name = "Джош", Surname = "Хатчерсон", Birthday = new DateTime(1992, 10, 12), Biography = "Полное имя — Джошуа Райан Хатчерсон (Joshua Ryan Hutcherson).", Delete = false };
            db.People.Add(DH);

            Person SS = new Person { Name = "Сергей", Patronymic = "Юрьевич", Surname = "Светлаков", Birthday = new DateTime(1977, 12, 12), Delete = false };
            db.People.Add(SS);
            Person IU = new Person { Name = "Иван", Patronymic = "Андреевич", Surname = "Ургант", Birthday = new DateTime(1978, 04, 16), Delete = true };
            db.People.Add(IU);
            Person LP = new Person { Name = "Любовь", Patronymic = "Григорьевна", Surname = "Полищук", Birthday = new DateTime(1949, 05, 21), Obit = new DateTime(2006, 11, 28), Biography = "Родилась в Омске, в семье рабочих. Ещё во время учебы в школе она занималась пением. В 1967 окончила Всероссийские творческие мастерские эстрадного искусства. Стала актрисой разговорного жанра, несколько лет вела театрализованные программы, работала в Омской филармонии артисткой разговорного жанра, исполняла монологи, которые писал для нее Марьян Беленький, потом была приглашена в Московский мюзик-холл. В 1985 году заочно окончила ГИТИС. С 1990 по 1997 играла в театре «Школа современной пьесы». Скончалась после тяжёлой болезни (как позже стало известно — рака позвоночника) утром 28 ноября 2006 года в Москве.", Country = RU, Delete = false };
            db.People.Add(LP);
            Person TB = new Person { Name = "Тимур", Surname = "Бекмамбетов", Birthday = new DateTime(1961, 06, 25) };
            db.People.Add(LP);

            //ГОЛОДНЫЕ ИГРЫ
            Movie gi1 = new Movie { Title = "Голодные игры", Duration = 142, Date = new DateTime(2012, 03, 22), Genre = new List<Genre>() { g1, g3, g5, g9 }, Country = new List<Country>() { US }, Description = "Будущее. Деспотичное государство ежегодно устраивает показательные игры на выживание, за которыми в прямом эфире следит весь мир. Жребий участвовать в Играх выпадает юной Китнисс и тайно влюбленному в нее Питу. Они знакомы с детства, но теперь должны стать врагами. Ведь по нерушимому закону Голодных игр победить может только один из 24 участников. Судьям не важно кто выиграет, главное — зрелище. И на этот раз зрелище будет незабываемым.", Delete = false };
            db.Movies.Add(gi1);
            Movie gi2 = new Movie { Title = "Голодные игры: И вспыхнет пламя", Genre = new List<Genre>() { g1, g3, g5, g9 }, Country = new List<Country>() { US }, Duration = 146, Date = new DateTime(2013, 11, 21), Description = "Сумев выжить на безжалостных Голодных играх, Китнисс Эвердин и Пит Мелларк возвращаются домой. Но теперь они в еще большей опасности, так как своим отказом играть по правилам на Арене бросили вызов Капитолию. По традиции следующие, юбилейные, Голодные игры должны стать особенными, и в этот раз в них участвуют только победители прошлых лет. Китнисс и Пит вынуждены вновь выйти на Арену, где будут соперничать с сильнейшими. Правила игры меняются. Арена еще опасней, масштаб еще больше, ставки еще выше!", Delete = false };
            db.Movies.Add(gi2);
            Movie gi3 = new Movie { Title = "Голодные игры: Сойка-пересмешница. Часть I", Genre = new List<Genre>() { g1, g3, g5, g9 }, Country = new List<Country>() { US }, Duration = 123, Date = new DateTime(2014, 11, 20), Description = "75-ые Голодные игры изменили все. Китнисс нарушила правила, и непоколебимое до той поры деспотичное правление Капитолия пошатнулось. У людей появилась надежда, и ее символ — Сойка-пересмешница. Теперь, чтобы освободить захваченного в плен Пита и защитить своих близких, Китнисс придется сражаться в настоящих битвах и стать еще сильнее, чем на арене игр.", Delete = false };
            db.Movies.Add(gi2);
            db.Actors.Add(new Actor { Movie = gi1, Person = DL, Character = "Китнисс Эвердин" });
            db.Actors.Add(new Actor { Movie = gi2, Person = DL, Character = "Китнисс Эвердин" });
            db.Actors.Add(new Actor { Movie = gi3, Person = DL, Character = "Китнисс Эвердин" });
            db.Actors.Add(new Actor { Movie = gi1, Person = DH, Character = "Пит Мелларк" });
            db.Actors.Add(new Actor { Movie = gi2, Person = DH, Character = "Пит Мелларк" });
            db.Actors.Add(new Actor { Movie = gi3, Person = DH, Character = "Пит Мелларк" });



            //ТАЧКИ
            db.Movies.Add(new Movie { Title = "Тачки", Description = "Неукротимый в своем желании всегда и во всем побеждать гоночный автомобиль «Молния» Маккуин вдруг обнаруживает, что сбился с пути и застрял в маленьком захолустном городке Радиатор-Спрингс, что находится где-то на трассе 66 в Калифорнии. Участвуя в гонках на Кубок Поршня, где ему противостояли два очень опытных соперника, Маккуин совершенно не ожидал, что отныне ему придется общаться с персонажами совсем иного рода. Это, например, Салли — шикарный Порше 2002-го года выпуска, Док Хадсон — легковушка модели «Хадсон Хорнет», 1951-го года выпуска или Метр — ржавый грузовичок-эвакуатор. И все они помогают Маккуину понять, что в мире существуют некоторые более важные вещи, чем слава, призы и спонсоры…", Duration = 112, Date = new DateTime(2006, 06, 15), Delete = false });
            db.Movies.Add(new Movie { Title = "Тачки 2", Description = "Молния МакКуин и его друг Мэтр отправляются в международное путешествие — когда МакКуин получает шанс участвовать в соревнованиях для самых быстрых машин в мире, Мировом Гран-При. Этапы этих престижных гонок заведут друзей в Токио, на набережные Парижа, на побережье Италии, и на улицы туманного Лондона.", Duration = 112, Date = new DateTime(2011, 06, 18), Delete = false });

            //ЁЛКИ
            Movie e1 = new Movie { Title = "Ёлки", Duration = 90, Date = new DateTime(2010, 12, 16), Genre = new List<Genre>() { g1, g2 }, Country = new List<Country>() { RU }, Description = "События самой новогодней комедии ЁЛКИ происходят в 11 городах: Калининграде, Казани, Перми, Уфе, Бавлах, Екатеринбурге, Красноярске, Якутске, Новосибирске, Санкт-Петербурге и Москве. Герои фильма — таксист и поп-дива, бизнесмен и актер, сноубордист и лыжник, студент и пенсионерка, пожарный и директриса, вор и милиционер, гастарбайтер и президент России. Все они оказываются в самый канун Нового года в очень непростой ситуации, выйти из которой им поможет только чудо… или Теория шести рукопожатий, согласно которой каждый человек на земле знает другого через шесть знакомых.", Delete = false };
            db.Movies.Add(e1);
            Movie e2 = new Movie { Title = "Ёлки 2", Duration = 96, Date = new DateTime(2011, 12, 15), Genre = new List<Genre>() { g1, g2 }, Country = new List<Country>() { RU }, Description = "Незадолго до Нового года Боря теряет память, и единственной зацепкой, которая может помочь, является надпись «З. Г.», которую он обнаруживает на руке. В это же время капитан полиции пытается решить проблемы личного характера и разлучить родную дочь и ее молодого человека, два экстремала и их любимая бабушка из подъезда снова готовятся к членовредительским подвигам, а Вера Брежнева опять сводит с ума сильную половину человечества, а конкретно — рядового Бондарева, бывшего таксиста.", Delete = false };
            db.Movies.Add(e2);
            Movie e3 = new Movie { Title = "Ёлки 3", Duration = 100, Date = new DateTime(2013, 12, 26), Genre = new List<Genre>() { g1, g2 }, Country = new List<Country>() { RU }, Description = "Спустя два года они снова с нами: любимые герои «Ёлок» в самых невероятных новогодних историях. Боря и Женя, чьи годовалые дети в канун праздника доведут друзей до психушки. Маленькая девочка Настя, чьи родители разлучат ее влюбленных друг в друга собак. Лыжник и сноубордист в самой экстремальной в их жизни гонке — от военкома. И профессор из Екатеринбурга Андрей, чья любвеобильность вновь не доведет его до добра, а только до проруби в минус 30.", Delete = false };
            db.Movies.Add(e3);
            Movie e1914 = new Movie { Title = "Ёлки 1914", Duration = 106, Date = new DateTime(2014, 12, 25), Genre = new List<Genre>() { g1, g2 }, Country = new List<Country>() { RU }, Description = "100 лет назад, Российская империя… Канун Рождества. Декабрьские пробки, праздничные гулянья, роскошные балы и скромные праздники, титулованные дворяне и обычные крестьяне, царская семья и солдаты первой мировой войны, прогрессивные поэты и первые фигуристы — все было по-другому, за исключением … праздника. Люди готовились, жили, верили, мечтали и ждали настоящего чуда — Рождества!", Delete = true };
            db.Movies.Add(e1914);
            db.Actors.Add(new Actor { Movie = e1, Person = IU, Character = "Боря" });
            db.Actors.Add(new Actor { Movie = e2, Person = IU, Character = "Боря" });
            db.Actors.Add(new Actor { Movie = e3, Person = IU, Character = "Боря" });
            db.Actors.Add(new Actor { Movie = e1914, Person = IU, Character = "Боря" });
            db.Actors.Add(new Actor { Movie = e1, Person = SS, Character = "Женя" });
            db.Actors.Add(new Actor { Movie = e2, Person = SS, Character = "Женя" });
            db.Actors.Add(new Actor { Movie = e3, Person = SS, Character = "Женя" });
            db.Actors.Add(new Actor { Movie = e1914, Person = SS, Character = "Женя" });
            db.FilmCrew.Add(new FilmCrew { Movie = e1, Person = TB, CrewPosition = cp1 });
            db.FilmCrew.Add(new FilmCrew { Movie = e1, Person = TB, CrewPosition = cp2 });
            db.FilmCrew.Add(new FilmCrew { Movie = e1, Person = TB, CrewPosition = cp3 });
            db.FilmCrew.Add(new FilmCrew { Movie = e2, Person = TB, CrewPosition = cp3 });
            db.FilmCrew.Add(new FilmCrew { Movie = e3, Person = TB, CrewPosition = cp3 });
            db.FilmCrew.Add(new FilmCrew { Movie = e1914, Person = TB, CrewPosition = cp1 });
            db.FilmCrew.Add(new FilmCrew { Movie = e1914, Person = TB, CrewPosition = cp3 });


            //ВЫКРУТАСЫ
            Movie v = new Movie { Title = "Выкрутасы", Duration = 97, Date = new DateTime(2011, 02, 17), Genre = new List<Genre>() { g1, g2, g5 }, Country = new List<Country>() { RU }, Description = "Слава Колотилов, простой школьный учитель из сонного приморского городка с небанальным названием «Пальчики», приехал покорять Москву с рукописью романа в руках, а покорил… красавицу Надю. Уже близится свадьба, ресторан заказан и гости приглашены, но цепкие Пальчики не дают Славе вырваться к суженой, подстраивая череду «непреодолимых обстоятельств». Вот и приходится Славе выкручиваться, рассказывая Наде по телефону небылицы о страшных происшествиях и катастрофах. А в далёкой Москве в это время Надя отбивает атаки своего бывшего ухажера Дани, готового пойти на любые выкрутасы, лишь бы вернуть себе невесту…", Delete = false };
            db.Movies.Add(v);
            db.Actors.Add(new Actor { Movie = v, Person = IU, Character = "Даня" });
            db.FilmCrew.Add(new FilmCrew { Movie = v, Person = TB, CrewPosition = cp3 });


            //БЕЗУМНЫЙ МАКС
            db.Movies.Add(new Movie { Title = "Безумный Макс: Дорога ярости", Description = "Преследуемый призраками беспокойного прошлого, Макс уверен, что лучший способ выжить — скитаться в одиночестве. Несмотря на это, он присоединяется к бунтарям, бегущим через всю пустыню на боевой фуре, под предводительством императрицы Фуриозы. Они пытаются сбежать из Цитадели, страдающей от тирании Несмертного Джо, у которого они забрали кое-что очень ценное. Разъярённый военачальник бросает все свои силы на погоню за мятежниками, ступая на дорогу войны — дорогу ярости.", Duration = 120, Date = new DateTime(2015, 05, 14), Genre = new List<Genre>() { g3, g4, g9 }, Country = new List<Country>() { US, AU }, Delete = false });


            //ДОБАВЛЕНИЕ АКТЕРОВ



            base.Seed(db);
        }
    }
}