using System.Numerics;
using Microsoft.EntityFrameworkCore;
using OptiFabricMVC.Domain.Interfaces;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Infrastructure.Repositories;

public class ShiftRepository : IShiftRepository
{
    private readonly Context _context;

    public ShiftRepository(Context context)
    {
        _context = context;
    }

    public async Task StartShiftData(Shift shift)
    {
        var employee = await _context.ApplicationUsers.FirstOrDefaultAsync(e => e.Id == shift.UserId);
        shift.ApplicationUser = employee;
        _context.Add(shift);
        await _context.SaveChangesAsync();
    }

    public async Task EndShiftData(Shift shift)
    {
        var activeShift = await _context.Shifts.FirstOrDefaultAsync(s => s.UserId == shift.UserId && s.isActive == true);
        if (activeShift != null)
        {
            activeShift.EndTime = shift.EndTime;
            activeShift.isActive = false;
        }
        await _context.SaveChangesAsync();
    }

    public IQueryable<Shift> GetAllShifts()
    {
        return _context.Shifts.AsQueryable();
    }
}