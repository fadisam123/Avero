using Avero.Core.Entities;
using Avero.Infrastructure.Persistence.DBContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avero.Infrastructure.Persistence
{
    public static class DataSeeder
    {
        public static void seed(ApplicationDBContext context, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            if (!roleManager.RoleExistsAsync("WholeSealer").Result)
            {
                roleManager.CreateAsync(new IdentityRole { Name = "WholeSealer" });
                roleManager.CreateAsync(new IdentityRole { Name = "Retailer" });
            }

            if (!context.city.Any())
            {
                var cities = new City[]
                {
                    /*new City{name = "Damascus"},
                    new City{name = "Rif Dimashq"},
                    new City{name = "Aleppo"},
                    new City{name = "Idlib"},
                    new City{name = "Al-Raqqah"},
                    new City{name = "Latakia"},
                    new City{name = "Al-Hasakah"},
                    new City{name = "Tartus"},
                    new City{name = "Hama"},
                    new City{name = "Deir ez-Zor"},
                    new City{name = "Homs"},
                    new City{name = "Quneitra"},
                    new City{name = "Daraa"},
                    new City{name = "As-Suwayda"},*/

                    new City{name = "Al Hasakah"},
                    new City{name = "Al Ladhiqiyah"},
                    new City{name = "Al Qunaytirah"},
                    new City{name = "Ar Raqqah"},
                    new City{name = "As Suwayda"},
                    new City{name = "Damascus"},
                    new City{name = "Dar‘a"},
                    new City{name = "Dayr az Zawr"},
                    new City{name = "Halab"},
                    new City{name = "Hamah"},
                    new City{name = "Hims"},
                    new City{name = "Idlib"},
                    new City{name = "Tartus"},

                };
                foreach (City c in cities)
                {
                    context.city.Add(c);
                }
                context.SaveChanges();
            }

            if (!context.neighborhood.Any())
            {
                var Damascus = context.city.First(x => x.name == "Damascus");
                /*var refDamas = context.city.First(x => x.name == "Rif Dimashq");*/
                var Halab = context.city.First(x => x.name == "Halab");
                var Idlib = context.city.First(x => x.name == "Idlib");
                var raqqah = context.city.First(x => x.name == "Ar Raqqah");
                var latak = context.city.First(x => x.name == "Al Ladhiqiyah");
                var hasaka = context.city.First(x => x.name == "Al Hasakah");
                var Tartus = context.city.First(x => x.name == "Tartus");
                var Hamah = context.city.First(x => x.name == "Hamah");
                var deir = context.city.First(x => x.name == "Dayr az Zawr");
                var Hims = context.city.First(x => x.name == "Hims");
                var quneitra = context.city.First(x => x.name == "Al Qunaytirah");
                var daraa = context.city.First(x => x.name == "Dar‘a");
                var suwyda = context.city.First(x => x.name == "As Suwayda");

                var neighborhoods = new Neighborhood[]
                {
                    /*new Neighborhood{name = "domar", city = damas},
                    new Neighborhood{name = "al hama", city = damas},
                    new Neighborhood{name = "duma", city = damas},
                    new Neighborhood{name = "feed", city = alepp},
                    new Neighborhood{name = "malaab", city = alepp},*/


                    new Neighborhood{name = "Abu Qalqal", city = Halab},
                    new Neighborhood{name = "Abu az Zuhur", city = Idlib},
                    new Neighborhood{name = "Ad Daliyah", city = latak},
                    new Neighborhood{name = "Ad Dana", city = Idlib},
                    new Neighborhood{name = "Ad Darbasiyah", city = hasaka},
                    new Neighborhood{name = "Ad Dimas", city = Damascus},
                    new Neighborhood{name = "Ad Dumayr", city = Damascus},
                    new Neighborhood{name = "Ad Duraykish", city = Tartus},
                    new Neighborhood{name = "Akhtarin", city = Halab},
                    new Neighborhood{name = "Al Atarib", city = Halab},
                    new Neighborhood{name = "Al Bab", city = Halab},
                    new Neighborhood{name = "Al Bahluliyah", city = latak},
                    new Neighborhood{name = "Al Bariqiyah", city = Tartus},
                    new Neighborhood{name = "Al Busayrah", city = deir},
                    new Neighborhood{name = "Al Fakhurah", city = latak},
                    new Neighborhood{name = "Al Furqlus", city = Hims},
                    new Neighborhood{name = "Al Ghandurah", city = Halab},
                    new Neighborhood{name = "Al Ghariyah", city = suwyda},
                    new Neighborhood{name = "Al Ghizlaniyah", city = Damascus},
                    new Neighborhood{name = "Al Hadir", city = Halab},
                    new Neighborhood{name = "Al Haffah", city = latak},
                    new Neighborhood{name = "Al Hajar al Aswad", city = Damascus},
                    new Neighborhood{name = "Al Hajib", city = Halab},
                    new Neighborhood{name = "Al Hamidiyah", city = Tartus},
                    new Neighborhood{name = "Al Hamra", city = Hamah},
                    new Neighborhood{name = "Al Hawash", city = Hims},
                    new Neighborhood{name = "Al Hawl", city = hasaka},
                    new Neighborhood{name = "Al Hinadi", city = latak},
                    new Neighborhood{name = "Al Hirak", city = daraa},
                    new Neighborhood{name = "Al Jala", city = deir},
                    new Neighborhood{name = "Al Janudiyah", city = Idlib},
                    new Neighborhood{name = "Al Jarniyah", city = raqqah},
                    new Neighborhood{name = "Al Jawadiyah", city = hasaka},
                    new Neighborhood{name = "Al Jizah", city = daraa},
                    new Neighborhood{name = "Al Karamah", city = raqqah},
                    new Neighborhood{name = "Al Karimah", city = Tartus},
                    new Neighborhood{name = "Al Kasrah", city = deir},
                    new Neighborhood{name = "Al Khafsah", city = Halab},
                    new Neighborhood{name = "Al Kiswah", city = Damascus},
                    new Neighborhood{name = "Al Malikiyah", city = hasaka},
                    new Neighborhood{name = "Al Mansurah", city = raqqah},
                    new Neighborhood{name = "Al Mayadin", city = deir},
                    new Neighborhood{name = "Al Mazra‘ah", city = suwyda},
                    new Neighborhood{name = "Al Ma‘batli", city = Halab},
                    new Neighborhood{name = "Al Mulayhah", city = Damascus},
                    new Neighborhood{name = "Al Musayfirah", city = daraa},
                    new Neighborhood{name = "Al Mushannaf", city = suwyda},
                    new Neighborhood{name = "Al Muzayrib", city = daraa},
                    new Neighborhood{name = "Al Muzayri‘ah", city = latak},
                    new Neighborhood{name = "Al Qabw", city = Hims},
                    new Neighborhood{name = "Al Qadmus", city = Tartus},
                    new Neighborhood{name = "Al Qahtaniyah", city = hasaka},
                    new Neighborhood{name = "Al Qamishli", city = hasaka},
                    new Neighborhood{name = "Al Qamsiyah", city = Tartus},
                    new Neighborhood{name = "Al Qardahah", city = latak},
                    new Neighborhood{name = "Al Qaryatayn", city = Hims},
                    new Neighborhood{name = "Al Qurayya", city = suwyda},
                    new Neighborhood{name = "Al Qusayr", city = Hims},
                    new Neighborhood{name = "Al Qutayfah", city = Damascus},
                    new Neighborhood{name = "Al Qutaylibiyah", city = latak},
                    new Neighborhood{name = "Al Ya‘rubiyah", city = hasaka},
                    new Neighborhood{name = "Al ‘Annazah", city = Tartus},
                    new Neighborhood{name = "Al ‘Arimah", city = Halab},
                    new Neighborhood{name = "Al ‘Asharah", city = deir},
                    new Neighborhood{name = "Albu Kamal", city = deir},
                    new Neighborhood{name = "An Nabk", city = Damascus},
                    new Neighborhood{name = "An Nashabiyah", city = Damascus},
                    new Neighborhood{name = "An Nasirah", city = Hims},
                    new Neighborhood{name = "Ar Rastan", city = Hims},
                    new Neighborhood{name = "Ar Rawdah", city = Tartus},
                    new Neighborhood{name = "Ar Ra‘i", city = Halab},
                    new Neighborhood{name = "Ar Riqama", city = Hims},
                    new Neighborhood{name = "Ar Ruhaybah", city = Damascus},
                    new Neighborhood{name = "Ariha", city = Idlib},
                    new Neighborhood{name = "Armanaz", city = Idlib},
                    new Neighborhood{name = "Arwad", city = Tartus},
                    new Neighborhood{name = "As Sabkhah", city = raqqah},
                    new Neighborhood{name = "As Sab‘ Biyar", city = Damascus},
                    new Neighborhood{name = "As Safirah", city = Halab},
                    new Neighborhood{name = "As Safsafah", city = Tartus},
                    new Neighborhood{name = "As Salamiyah", city = Hamah},
                    new Neighborhood{name = "As Sanamayn", city = daraa},
                    new Neighborhood{name = "As Sawda", city = Tartus},
                    new Neighborhood{name = "As Sisniyah", city = Tartus},
                    new Neighborhood{name = "As Si‘in", city = Hamah},
                    new Neighborhood{name = "As Sukhnah", city = Hims},
                    new Neighborhood{name = "As Suqaylibiyah", city = Hamah},
                    new Neighborhood{name = "As Surah as Saghirah", city = suwyda},
                    new Neighborhood{name = "As Susah", city = deir},
                    new Neighborhood{name = "As Suwar", city = deir},
                    new Neighborhood{name = "Ash Shaddadah", city = hasaka},
                    new Neighborhood{name = "Ash Shajarah", city = daraa},
                    new Neighborhood{name = "Ash Shaykh Badr", city = Tartus},
                    new Neighborhood{name = "At Tall", city = Damascus},
                    new Neighborhood{name = "At Tamani‘ah", city = Idlib},
                    new Neighborhood{name = "At Tawahin", city = Tartus},
                    new Neighborhood{name = "At Tibni", city = deir},
                    new Neighborhood{name = "Ath Thawrah", city = raqqah},
                    new Neighborhood{name = "Az Zabadani", city = Damascus},
                    new Neighborhood{name = "Az Zarbah", city = Halab},
                    new Neighborhood{name = "Az Ziyarah", city = Hamah},
                    new Neighborhood{name = "Babila", city = Damascus},
                    new Neighborhood{name = "Banan", city = Halab},
                    new Neighborhood{name = "Baniyas", city = Tartus},
                    new Neighborhood{name = "Barri ash Sharqi", city = Hamah},
                    new Neighborhood{name = "Bayt Yashut", city = latak},
                    new Neighborhood{name = "Bdama", city = Idlib},
                    new Neighborhood{name = "Binnish", city = Idlib},
                    new Neighborhood{name = "Bir al Hulw al Wardiyah", city = hasaka},
                    new Neighborhood{name = "Brummanat al Mashayikh", city = Tartus},
                    new Neighborhood{name = "Bulbul", city = Halab},
                    new Neighborhood{name = "Busra ash Sham", city = daraa},
                    new Neighborhood{name = "Darat ‘Izzah", city = Halab},
                    new Neighborhood{name = "Darayya", city = Damascus},
                    new Neighborhood{name = "Darkush", city = Idlib},
                    new Neighborhood{name = "Dayr Hafir", city = Halab},
                    new Neighborhood{name = "Dayr ‘Atiyah", city = Damascus},
                    new Neighborhood{name = "Da‘il", city = daraa},
                    new Neighborhood{name = "Dhiban", city = deir},
                    new Neighborhood{name = "Dhibbin", city = suwyda},
                    new Neighborhood{name = "Duma", city = Damascus},
                    new Neighborhood{name = "Duwayr Raslan", city = Tartus},
                    new Neighborhood{name = "Ghabaghib", city = daraa},
                    new Neighborhood{name = "Hadidah", city = Hims},
                    new Neighborhood{name = "Hajin", city = deir},
                    new Neighborhood{name = "Hammam Wasil", city = Tartus},
                    new Neighborhood{name = "Harasta", city = Damascus},
                    new Neighborhood{name = "Harbinafsah", city = Hamah},
                    new Neighborhood{name = "Harf al Musaytirah", city = latak},
                    new Neighborhood{name = "Harim", city = Idlib},
                    new Neighborhood{name = "Harran al ‘Awamid", city = Damascus},
                    new Neighborhood{name = "Himmin", city = Tartus},
                    new Neighborhood{name = "Hish", city = Idlib},
                    new Neighborhood{name = "Hisya", city = Hims},
                    new Neighborhood{name = "Huraytan", city = Halab},
                    new Neighborhood{name = "Ihsim", city = Idlib},
                    new Neighborhood{name = "Izra", city = daraa},
                    new Neighborhood{name = "I‘zaz", city = Halab},
                    new Neighborhood{name = "Jablah", city = latak},
                    new Neighborhood{name = "Jarabulus", city = Halab},
                    new Neighborhood{name = "Jaramana", city = Damascus},
                    new Neighborhood{name = "Jasim", city = daraa},
                    new Neighborhood{name = "Jawbat Burghal", city = latak},
                    new Neighborhood{name = "Jayrud", city = Damascus},
                    new Neighborhood{name = "Jindayris", city = Halab},
                    new Neighborhood{name = "Jisr ash Shughur", city = Idlib},
                    new Neighborhood{name = "Jubb Ramlah", city = Hamah},
                    new Neighborhood{name = "Jubb al Jarrah", city = Hims},
                    new Neighborhood{name = "Junaynat Raslan", city = Tartus},
                    new Neighborhood{name = "Kafr Batna", city = Damascus},
                    new Neighborhood{name = "Kafr Nubl", city = Idlib},
                    new Neighborhood{name = "Kafr Takharim", city = Idlib},
                    new Neighborhood{name = "Kafr Zayta", city = Hamah},
                    new Neighborhood{name = "Kassab", city = latak},
                    new Neighborhood{name = "Khan Arnabah", city = quneitra},
                    new Neighborhood{name = "Khan Shaykhun", city = Idlib},
                    new Neighborhood{name = "Khanasir", city = Halab},
                    new Neighborhood{name = "Khirbat Ghazalah", city = daraa},
                    new Neighborhood{name = "Khirbat Tin Nur", city = Hims},
                    new Neighborhood{name = "Khirbat al Ma‘azzah", city = Tartus},
                    new Neighborhood{name = "Khusham", city = deir},
                    new Neighborhood{name = "Kinnsibba", city = latak},
                    new Neighborhood{name = "Kurnaz", city = Hamah},
                    new Neighborhood{name = "Kuwayris Sharqi", city = Halab},
                    new Neighborhood{name = "Madaya", city = Damascus},
                    new Neighborhood{name = "Mahin", city = Hims},
                    new Neighborhood{name = "Malah", city = suwyda},
                    new Neighborhood{name = "Manbij", city = Halab},
                    new Neighborhood{name = "Mari‘", city = Halab},
                    new Neighborhood{name = "Markadah", city = hasaka},
                    new Neighborhood{name = "Mashta al Hulw", city = Tartus},
                    new Neighborhood{name = "Maskanah", city = Halab},
                    new Neighborhood{name = "Masyaf", city = Hamah},
                    new Neighborhood{name = "Mazra‘at Bayt Jinn", city = Damascus},
                    new Neighborhood{name = "Ma‘arrat an Nu‘man", city = Idlib},
                    new Neighborhood{name = "Ma‘arratmisrin", city = Idlib},
                    new Neighborhood{name = "Ma‘dan", city = raqqah},
                    new Neighborhood{name = "Ma‘lula", city = Damascus},
                    new Neighborhood{name = "Mismiyah", city = daraa},
                    new Neighborhood{name = "Muh Hasan", city = deir},
                    new Neighborhood{name = "Muhambal", city = Idlib},
                    new Neighborhood{name = "Muhradah", city = Hamah},
                    new Neighborhood{name = "Mukharram al Fawqani", city = Hims},
                    new Neighborhood{name = "Nawa", city = daraa},
                    new Neighborhood{name = "Nubl", city = Halab},
                    new Neighborhood{name = "Qadsayya", city = Damascus},
                    new Neighborhood{name = "Qal‘at al Madiq", city = Hamah},
                    new Neighborhood{name = "Qastal Ma‘af", city = latak},
                    new Neighborhood{name = "Qatana", city = Damascus},
                    new Neighborhood{name = "Qurqina", city = Idlib},
                    new Neighborhood{name = "Rabi‘ah", city = latak},
                    new Neighborhood{name = "Raju", city = Halab},
                    new Neighborhood{name = "Rankus", city = Damascus},
                    new Neighborhood{name = "Ras al Khashufah", city = Tartus},
                    new Neighborhood{name = "Ras al ‘Ayn", city = hasaka},
                    new Neighborhood{name = "Rasm al Harmal", city = Halab},
                    new Neighborhood{name = "Sabbah", city = Tartus},
                    new Neighborhood{name = "Sabburah", city = Hamah},
                    new Neighborhood{name = "Sadad", city = Hims},
                    new Neighborhood{name = "Safita", city = Tartus},
                    new Neighborhood{name = "Sahnaya", city = Damascus},
                    new Neighborhood{name = "Salkhad", city = suwyda},
                    new Neighborhood{name = "Salqin", city = Idlib},
                    new Neighborhood{name = "Saraqib", city = Idlib},
                    new Neighborhood{name = "Sarmin", city = Idlib},
                    new Neighborhood{name = "Saydnaya", city = Damascus},
                    new Neighborhood{name = "Sa‘sa‘", city = Damascus},
                    new Neighborhood{name = "Shahba", city = suwyda},
                    new Neighborhood{name = "Shaqqa", city = suwyda},
                    new Neighborhood{name = "Sharan", city = Halab},
                    new Neighborhood{name = "Shathah", city = Hamah},
                    new Neighborhood{name = "Shaykh Miskin", city = daraa},
                    new Neighborhood{name = "Shaykh al Hadid", city = Halab},
                    new Neighborhood{name = "Shin", city = Hims},
                    new Neighborhood{name = "Shuyukh Tahtani", city = Halab},
                    new Neighborhood{name = "Sinjar", city = Idlib},
                    new Neighborhood{name = "Sirghaya", city = Damascus},
                    new Neighborhood{name = "Sirrin ash Shamaliyah", city = Halab},
                    new Neighborhood{name = "Slinfah", city = latak},
                    new Neighborhood{name = "Suluk", city = raqqah},
                    new Neighborhood{name = "Suran", city = Halab},
                    new Neighborhood{name = "Suran", city = Hamah},
                    new Neighborhood{name = "Tadif", city = Halab},
                    new Neighborhood{name = "Tadmur", city = Hims},
                    new Neighborhood{name = "Tafas", city = daraa},
                    new Neighborhood{name = "Taftanaz", city = Idlib},
                    new Neighborhood{name = "Talin", city = Tartus},
                    new Neighborhood{name = "Tall Abyad", city = raqqah},
                    new Neighborhood{name = "Tall Hamis", city = hasaka},
                    new Neighborhood{name = "Tall Rif‘at", city = Halab},
                    new Neighborhood{name = "Tall Salhab", city = Hamah},
                    new Neighborhood{name = "Tall Tamr", city = hasaka},
                    new Neighborhood{name = "Tall ad Daman", city = Halab},
                    new Neighborhood{name = "Tallbisah", city = Hims},
                    new Neighborhood{name = "Talldaww", city = Hims},
                    new Neighborhood{name = "Tallkalakh", city = Hims},
                    new Neighborhood{name = "Tasil", city = daraa},
                    new Neighborhood{name = "Wadi al ‘Uyun", city = Hamah},
                    new Neighborhood{name = "Yabrud", city = Damascus},
                    new Neighborhood{name = "‘Afrin", city = Halab},
                    new Neighborhood{name = "‘Amuda", city = hasaka},
                    new Neighborhood{name = "‘Ariqah", city = suwyda},
                    new Neighborhood{name = "‘Arishah", city = hasaka},
                    new Neighborhood{name = "‘Assal al Ward", city = Damascus},
                    new Neighborhood{name = "‘Awaj", city = Hamah},
                    new Neighborhood{name = "‘Ayn Halaqim", city = Hamah},
                    new Neighborhood{name = "‘Ayn Shiqaq", city = latak},
                    new Neighborhood{name = "‘Ayn al Bayda", city = latak},
                    new Neighborhood{name = "‘Ayn al Fijah", city = Damascus},
                    new Neighborhood{name = "‘Ayn al ‘Arab", city = Halab},
                    new Neighborhood{name = "‘Ayn an Nasr", city = Hims},
                    new Neighborhood{name = "‘Ayn ash Sharqiyah", city = latak},
                    new Neighborhood{name = "‘Ayn at Tinah", city = latak},
                    new Neighborhood{name = "‘Ayn ‘Isa", city = raqqah},
                    new Neighborhood{name = "‘Irbin", city = Damascus},
                    new Neighborhood{name = "‘Uqayribat", city = Hamah},



                };
                foreach (Neighborhood n in neighborhoods)
                {
                    context.neighborhood.Add(n);
                }
                context.SaveChanges();
            }

            if (!context.catagory.Any())
            {

                var catagories = new Catagory[]
                {
                    new Catagory{name = "Sport"},
                    new Catagory{name = "Food"},
                    new Catagory{name = "Phone"},
                    new Catagory{name = "Other"},
                };
                foreach (Catagory n in catagories)
                {
                    context.catagory.Add(n);
                }
                context.SaveChanges();
            }


        }


    }
}
