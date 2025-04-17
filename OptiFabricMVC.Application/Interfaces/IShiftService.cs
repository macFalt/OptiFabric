using OptiFabricMVC.Application.ViewModels.EmployeeVM;

namespace OptiFabricMVC.Application.Interfaces;

public interface IShiftService
{
    //void StartShift(DateTime startShiftData, string UserId);
    Task StartShift(DateTime startShiftData, string UserId);
    //void EndShift(DateTime endShiftData, string UserId);
    Task EndShift(DateTime endShiftData, string UserId);
    ListWorkingHoursVM GetAllShifts(string id, int pageSize, int pageNo, string searchString);
}