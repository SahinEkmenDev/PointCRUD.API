
using Microsoft.AspNetCore.Mvc;

using BaşarSoftDeneme.Interfaces;

using BaşarSoftDeneme.Models;


namespace BaşarSoftDeneme.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointController : ControllerBase
    {
        private readonly IPointService<Point> _pointService;

        public PointController(IPointService<Point> pointService)
        {
            _pointService = pointService;
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                Console.WriteLine($"GetById çağrıldı. ID: {id}");

                var response = await _pointService.GetByIdAsync(id);

                Console.WriteLine(response == null ? "Point bulunamadı." : "Point başarıyla bulundu.");

                if (response != null)
                    return Ok(new Response<Point> { Status = true, Value = response, Message = "Point başarıyla alındı." });

                return NotFound(new Response { Status = false, Message = "Point bulunamadı." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetById sırasında hata: {ex.Message}");
                return StatusCode(500, new Response { Status = false, Message = "GetById işlemi sırasında bir hata oluştu." });
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await _pointService.GetAllAsync();
                return Ok(new Response<IEnumerable<Point>> { Status = true, Value = response, Message = "Points başarıyla alındı." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetAll sırasında hata: {ex.Message}");
                return StatusCode(500, new Response { Status = false, Message = "GetAll işlemi sırasında bir hata oluştu." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(Point point)
        {
            try
            {
                if (point == null)
                    return BadRequest(new Response { Status = false, Message = "Point boş olamaz." });

                var createdPoint = await _pointService.AddAsync(point);
                return CreatedAtAction(nameof(GetById), new { id = createdPoint.Id }, new Response<Point> { Status = true, Value = createdPoint, Message = "Point başarıyla oluşturuldu." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Add sırasında hata: {ex.Message}");
                return StatusCode(500, new Response { Status = false, Message = "Add işlemi sırasında bir hata oluştu." });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, Point point)
        {
            try
            {
                Console.WriteLine($"URL'den gelen ID: {id}, Point nesnesindeki ID: {point.Id}");

                if (id != point.Id)
                {
                    Console.WriteLine($"ID uyuşmazlığı. URL'den gelen ID: {id}, Point nesnesindeki ID: {point.Id}");
                    return BadRequest(new Response { Status = false, Message = $"ID uyuşmazlığı. URL'den gelen ID: {id}, Point nesnesindeki ID: {point.Id}" });
                }

                var existingPoint = await _pointService.GetByIdAsync(id);
                if (existingPoint == null)
                {
                    Console.WriteLine("Point bulunamadı.");
                    return NotFound(new Response { Status = false, Message = "Point bulunamadı." });
                }

                Console.WriteLine("Güncelleme işlemi başlıyor.");
                await _pointService.UpdateAsync(point);
                Console.WriteLine("Point başarıyla güncellendi.");
                return Ok(new Response { Status = true, Message = "Point başarıyla güncellendi." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Güncelleme sırasında hata: {ex.Message}");
                return StatusCode(500, new Response { Status = false, Message = "Güncelleme sırasında bir hata oluştu." });
            }
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var existingPoint = await _pointService.GetByIdAsync(id);
                if (existingPoint == null)
                    return NotFound(new Response { Status = false, Message = "Point bulunamadı." });

                await _pointService.DeleteAsync(id);
                return Ok(new Response { Status = true, Message = "Point başarıyla silindi." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Delete sırasında hata: {ex.Message}");
                return StatusCode(500, new Response { Status = false, Message = "Delete işlemi sırasında bir hata oluştu." });
            }
        }
    }
}
