using BaşarSoftDeneme.Interfaces;
using BaşarSoftDeneme.Controllers;
using BaşarSoftDeneme.Models;
using Microsoft.EntityFrameworkCore;




namespace BaşarSoftDeneme.Models
{
    public class PointService : IPointService
    {
        private readonly dbContext _context;

        public PointService(dbContext context)
        {
            _context = context;
        }

        public Point? GetById(long id)
        {

            return _context.Points
                .FromSqlRaw("SELECT * FROM \"points\" WHERE \"id\" = {0}", id)
                .FirstOrDefault();
        }

        public IEnumerable<Point> GetAll()
        {

            return _context.Points
                .FromSqlRaw("SELECT * FROM \"points\"")
                .ToList();
        }

        public Point Create(Point point)
        {

            _context.Database.ExecuteSqlRaw(
                "INSERT INTO \"points\" (\"pointx\", \"pointy\", \"name\") VALUES ({0}, {1}, {2}) RETURNING \"id\"",
                point.PointX, point.PointY, point.Name
            );
            return point;
        }

        public Point Update(Point point)
        {
            
            _context.Database.ExecuteSqlRaw(
                "UPDATE \"points\" SET \"pointx\" = {0}, \"pointy\" = {1}, \"name\" = {2} WHERE \"id\" = {3}",
                point.PointX, point.PointY, point.Name, point.Id
            );

            
            return point;
        }

        public bool Delete(long id)
        {
            
            var result = _context.Database.ExecuteSqlRaw(
                "DELETE FROM \"points\" WHERE \"id\" = {0}", id
            );

            
            return result > 0;
        }

    }
}
