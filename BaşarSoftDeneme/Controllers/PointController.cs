
using Microsoft.AspNetCore.Mvc;

using BaşarSoftDeneme.Interfaces;

using BaşarSoftDeneme.Models;


namespace BaşarSoftDeneme.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointController : ControllerBase
    {
        private readonly IRepositoryService<Point> _pointService;

        public PointController(IRepositoryService<Point> pointService)
        {
            _pointService = pointService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var response = await _pointService.GetByIdAsync(id);

                if (response != null)
                    return Ok(new Response<Point> { Status = true, Value = response, Message = "Point başarıyla alındı." });

                return NotFound(new Response { Status = false, Message = "Point bulunamadı." });
            }
            catch (Exception ex)
            {
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
                return StatusCode(500, new Response { Status = false, Message = "GetAll işlemi sırasında bir hata oluştu." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(Point point)
        {
            try
            {
                if (point == null || string.IsNullOrEmpty(point.WKT))
                    return BadRequest(new Response { Status = false, Message = "Point veya WKT boş olamaz." });

                var createdPoint = await _pointService.AddAsync(point);
                return CreatedAtAction(nameof(GetById), new { id = createdPoint.Id }, new Response<Point> { Status = true, Value = createdPoint, Message = "Point başarıyla oluşturuldu." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response { Status = false, Message = "Add işlemi sırasında bir hata oluştu." });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, Point point)
        {
            try
            {
                if (id != point.Id)
                {
                    return BadRequest(new Response { Status = false, Message = $"ID uyuşmazlığı. URL'den gelen ID: {id}, Point nesnesindeki ID: {point.Id}" });
                }

                var existingPoint = await _pointService.GetByIdAsync(id);
                if (existingPoint == null)
                {
                    return NotFound(new Response { Status = false, Message = "Point bulunamadı." });
                }
                await _pointService.UpdateAsync(point);
                return Ok(new Response { Status = true, Message = "Point başarıyla güncellendi." });
            }
            catch (Exception ex)
            {
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
                return StatusCode(500, new Response { Status = false, Message = "Delete işlemi sırasında bir hata oluştu." });
            }
        }
    }
    
}
