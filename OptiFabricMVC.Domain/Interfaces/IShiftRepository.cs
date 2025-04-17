using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Domain.Interfaces;

public interface IShiftRepository
{
    //void StartShiftData(Shift shift);
    Task StartShiftData(Shift shift);
    Task EndShiftData(Shift shift);
    //void EndShiftData(Shift shift);
    IQueryable<Shift> GetAllShifts();
}