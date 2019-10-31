using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MyRepository.Control;
using MyRepository.Models;
namespace MyRepository.Context
{
    class ContextInitializer : DropCreateDatabaseAlways<HotelContext>
    {
        protected override void Seed(HotelContext context)
        {
            Apartment a1 = new Apartment
            {
                Name = "Гостевой номер \"Гранд Делюкс\"",
                PricePerNight = 300,
                AmountOfPeople = 4,
                Description = "Кровать King-size,Вид на озеро,Завтрак включен",
                Class = "Econom",
                Href = "~/Content/Photos/Photo1.jpg"
            };
            Apartment a2 = new Apartment
            {
                Name = "Полулюкс с кроватью размера \"king - size\"",
                PricePerNight = 100,
                AmountOfPeople = 6,
                Description = "Кровать King-size,Вид на озеро,Завтрак не включен",
                Class = "Junior Suite",
                Href = "~/Content/Photos/Photo2.jpg"
            };
            Apartment a3 = new Apartment
            {
                Name = "Executive Suites",
                PricePerNight = 150,
                AmountOfPeople = 2,
                Description = "Услуги дворецкого по запросу,ВИП Заезд / выезд,24 часовой сервис комнаты",
                Class = "Lux",
                Href = "~/Content/Photos/Photo3.jpg"
            };
            Apartment a4 = new Apartment
            {
                Name = "Home spa",
                PricePerNight = 310,
                AmountOfPeople = 5,
                Description = "Банные принадлежности и аксессуары класса Премиум,Махровые халаты и тапочки,Подушки на выбор",
                Class = "Junior Suite",
                Href = "~/Content/Photos/Photo4.jpg"
            };
            Apartment a5 = new Apartment
            {
                Name = "Гостевой номер \"Гранд Делюкс\"",
                PricePerNight = 200,
                AmountOfPeople = 4,
                Description = "Дистанционное управление матрасами и занавесками,Подогреваемые полы,Аэро спа ванна и паровая ванная кабина",
                Class = "Lux",
                Href = "~/Content/Photos/Photo5.jpg"
            };



            var apartmentList = new List<Apartment> { a1,a2,a3,a4,a5 };
            apartmentList.ForEach(art => context.Apartments.Add(art));
            
            context.SaveChanges();
        }
    }
}
