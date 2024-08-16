
using Microsoft.AspNetCore.Mvc;

using BaşarSoftDeneme.Interfaces;

using BaşarSoftDeneme.Models;


namespace BaşarSoftDeneme.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointController : ControllerBase
    {
        private readonly IPointService _pointService;

        public PointController(IPointService pointService)
        {
            _pointService = pointService;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var result = _pointService.GetById(id);
            if (result == null)
            {
                return NotFound(new Response<Point> { ValueStatus = false, Message = "Id Bulunamadı" });
            }
            return Ok(new Response<Point> { Value = result, ValueStatus = true, Message = "Başarılı" });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var results = _pointService.GetAll();
            return Ok(new Response<IEnumerable<Point>> { Value = results, ValueStatus = true, Message = "Başarılı" });
        }

        [HttpPost]
        public IActionResult Create([FromBody] Point point)
        {
            var result = _pointService.Create(point);
            return Created("", new Response<Point> { Value = result, ValueStatus = true, Message = "Point Oluşturuldu" });
        }

        [HttpPut]
        public IActionResult Update([FromBody] Point point)
        {
            var result = _pointService.Update(point);
            return Ok(new Response<Point> { Value = result, ValueStatus = true, Message = "Point Güncellendi" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var result = _pointService.Delete(id);
            if (!result)
            {
                return NotFound(new Response<bool> { ValueStatus = false, Message = "Id Bulunamadı" });
            }
            return Ok(new Response<bool> { ValueStatus = true, Message = "Point Silindi" });
        }
    }
}
