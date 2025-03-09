using AutoMapper;
using AutoMapper.QueryableExtensions;
using OptiFabricMVC.Application.Interfaces;
using OptiFabricMVC.Application.ViewModels.EmployeeVM;
using OptiFabricMVC.Domain.Interfaces;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.Services;

public class ShiftService: IShiftService
{
    public readonly IShiftRepository _ShiftRepository;
    public readonly IMapper _mapper;

    public ShiftService(IShiftRepository shiftRepository,IMapper mapper)
    {
        _ShiftRepository = shiftRepository;
        _mapper = mapper;
    }

    public void StartShift(DateTime startShiftData,string UserId)
    {
        var shift = new Shift
        {
            UserId = UserId,
            StartTime = startShiftData,
            isActive = true,
            EndTime = null

        };
        
        _ShiftRepository.StartShiftData(shift);
    }
    
    public void EndShift(DateTime endShiftData,string UserId)
    {
        var shift = new Shift
        {
            UserId = UserId,
            EndTime = endShiftData,
        };
        
        _ShiftRepository.EndShiftData(shift);
    }
    

    public ListWorkingHoursVM GetAllShifts(string id,int pageSize, int pageNo, string searchString)
    {
        var listShifts = _ShiftRepository.GetAllShifts()
            .Where(s => s.UserId == id &&
                        (string.IsNullOrEmpty(searchString) || 
                         s.StartTime.Date == DateTime.Parse(searchString).Date))
            .ProjectTo<WorkingHoursVM>(_mapper.ConfigurationProvider)
            .ToList();
        
        var shiftToShow = listShifts.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToList();
        
        var shiftList=new ListWorkingHoursVM()
        {
            PageSize = pageSize,
            CurrentPage = pageNo,
            SearchString = searchString,
            WorkingHours = shiftToShow,
            Count = listShifts.Count
        };
        return shiftList;
    }
}