using BaşarSoftDeneme.Models;
using BaşarSoftDeneme.Interfaces;



namespace BaşarSoftDeneme.Interfaces
{
    public interface IPointService
    {
        Point GetById(long id);
        IEnumerable<Point> GetAll();
        Point Create(Point point);
        Point Update(Point point);
        bool Delete(long id);
       
    }
}
