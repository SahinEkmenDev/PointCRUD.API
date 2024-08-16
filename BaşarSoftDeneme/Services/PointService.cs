﻿using BaşarSoftDeneme.Interfaces;
using BaşarSoftDeneme.Controllers;
using BaşarSoftDeneme.Models;
using Microsoft.EntityFrameworkCore;





namespace BaşarSoftDeneme.Services
{
    public class PointService : IPointService<Point>
    {
        private readonly dbContext _context;

        public PointService(dbContext context)
        {
            _context = context;
        }

        public async Task<Point> GetByIdAsync(long id)
        {
            return await _context.Points.FindAsync(id);
        }

        public async Task<IEnumerable<Point>> GetAllAsync()
        {
            return await _context.Points.ToListAsync();
        }

        public async Task<Point> AddAsync(Point point)
        {
            _context.Points.Add(point);
            await _context.SaveChangesAsync();
            return point;
        }

        public async Task UpdateAsync(Point point)
        {
            _context.Points.Update(point);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var point = await _context.Points.FindAsync(id);
            if (point != null)
            {
                _context.Points.Remove(point);
                await _context.SaveChangesAsync();
            }
        }

    }
}
