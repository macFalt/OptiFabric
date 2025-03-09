using System.Numerics;
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

    public void StartShiftData(Shift shift)
    {
        var employee = _context.ApplicationUsers.FirstOrDefault(e => e.Id == shift.UserId);
        shift.ApplicationUser = employee;
        _context.Add(shift);
        _context.SaveChanges();
    }

    public void EndShiftData(Shift shift)
    {
        var activeShift = _context.Shifts.FirstOrDefault(s => s.UserId == shift.UserId && s.isActive == true);
        activeShift.EndTime = shift.EndTime;
        activeShift.isActive = false;
        _context.SaveChanges();
    }

    public IQueryable<Shift> GetAllShifts()
    {
        return _context.Shifts.AsQueryable();
    }
}