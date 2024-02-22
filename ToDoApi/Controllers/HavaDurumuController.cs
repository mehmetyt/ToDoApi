using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Dtos;

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HavaDurumuController : ControllerBase
    {
        readonly Random _rnd= new();
        readonly string[] _durumlar = [ "Açık", "Parçalı Bulutlu", "Çok Bulutlu", "Sisli", "Puslu", "Yağmurlu", "Sağanak Yağışlı", "Kar Yağışlı", "Karla Karışık Yağmur", "Dolu", "Sıcak", "Soğuk", "Rüzgarlı", "Fırtına", "Kasırga" ];

        [HttpGet]
        public List<HavaDto> Get()
        {

            var tahminler= new List<HavaDto>();

            for (int i = 0; i < 5; i++)
            {
                tahminler.Add(new HavaDto()
                {
                    Tarih=DateOnly.FromDateTime(DateTime.Today.AddDays(i)),
                    Sicaklik= _rnd.Next(-5, 30),
                    Aciklama=_durumlar[_rnd.Next(0, _durumlar.Length)]
                });
            }
            return tahminler;
        }
    }
}
