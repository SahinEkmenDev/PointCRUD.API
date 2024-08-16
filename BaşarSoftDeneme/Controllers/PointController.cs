
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
            var response = await _pointService.GetByIdAsync(id);
            if (response==null)
                return Ok(new Response<Point> { Status = true, Value = response, Message = "Point retrieved successfully." });
            return NotFound(new Response { Status = false, Message = "Point not found." });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _pointService.GetAllAsync();
            return Ok(new Response<IEnumerable<Point>> { Status = true, Value = response, Message = "Points retrieved successfully." });
        }

        [HttpPost]
        public async Task<IActionResult> Add(Point point)
        {
            if (point == null)
                return BadRequest(new Response { Status = false, Message = "Point cannot be null." });

            var createdPoint = await _pointService.AddAsync(point);
            return CreatedAtAction(nameof(GetById), new { id = createdPoint.Id }, new Response<Point> { Status = true, Value = createdPoint, Message = "Point created successfully." });
        }

        [HttpPut]
        public async Task<IActionResult> Update(long id, Point point)
        {
            if (id != point.Id)
                return BadRequest(new Response { Status = false, Message = "Point ID mismatch." });

            var existingPoint = await _pointService.GetByIdAsync(id);
            if (existingPoint == null)
                return NotFound(new Response { Status = false, Message = "Point not found." });

            await _pointService.UpdateAsync(point);
            return Ok(new Response { Status = true, Message = "Point updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var existingPoint = await _pointService.GetByIdAsync(id);
            if (existingPoint == null)
                return NotFound(new Response { Status = false, Message = "Point not found." });

            await _pointService.DeleteAsync(id);
            return Ok(new Response { Status = true, Message = "Point deleted successfully." });
        }
    }
}
