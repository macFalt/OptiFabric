using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using OptiFabricMVC.Application.Interfaces;
using OptiFabricMVC.Application.Mapping;
using OptiFabricMVC.Application.ViewModels.EmployeeVM;
using OptiFabricMVC.Domain.Interfaces;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.Services;

public class EmployeeService: IEmployeeService
{
    public readonly IEmployeeRepository _EmployeeRepository;
    public readonly IMapper _mapper;
    public readonly UserManager<ApplicationUser> _UserManager;

    public EmployeeService(IEmployeeRepository employeeRepository,IMapper mapper,UserManager<ApplicationUser> userManager)
    {
        _EmployeeRepository = employeeRepository;
        _mapper = mapper;
        _UserManager = userManager;
    }

    
    public async Task<IdentityResult> AddEmployeeAsync(NewEmployeeVM model)
    { 
        var employee = _mapper.Map<ApplicationUser>(model);
        var result = await _UserManager.CreateAsync(employee, model.Password);
        return result;
    }

    public ListEmployeeVM GetAllEmployee(int pageSize, int pageNo, string searchString)
    {
        var employee=_EmployeeRepository.GetAllEmployeeFromDB().Where(p=>p.Surname.StartsWith(searchString))
            .ProjectTo<OptiFabricMVC.Application.ViewModels.EmployeeVM.EmployeeForListVM>(_mapper.ConfigurationProvider).ToList();
        var employeeToShow = employee.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToList();
        var employeeList=new ListEmployeeVM()
        {
        PageSize = pageSize,
        CurrentPage = pageNo,
        SearchString = searchString,
        EmployeeForListVms = employeeToShow,
        Count = employee.Count
    };
        return employeeList;

    }

    public EmployeeDetailsVM GetEmployeeDetail(string id)
    {
        var employee = _EmployeeRepository.GetEmployee(id);
        var emp = _mapper.Map<EmployeeDetailsVM>(employee);
        return emp;
    }

    public void DeleteEmployee(string id)
    {
        _EmployeeRepository.DeleteEmployee(id);
    }

    public void EditEmployee(EditEmployeeVM model)
    {
        var employee = _mapper.Map<ApplicationUser>(model);
        _EmployeeRepository.UpdateEmployee(employee);
    }

    public void DeleteShift(int id)
    {
        _EmployeeRepository.DeleteShiftFromDB(id);
    }
}