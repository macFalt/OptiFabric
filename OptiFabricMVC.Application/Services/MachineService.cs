using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OptiFabricMVC.Application.Interfaces;
using OptiFabricMVC.Application.ViewModels.MachinesVM;
using OptiFabricMVC.Domain.Interfaces;
using OptiFabricMVC.Domain.Model;
using OptiFabricMVC.Infrastructure.Common;
using MachineStatus = OptiFabricMVC.Domain.Model.MachineStatus;

namespace OptiFabricMVC.Application.Services;

public class MachineService : IMachineService
{
    private readonly IMachinesRepository _machinesRepository;
    private readonly IMapper _mapper;
    
    
    public MachineService(IMachinesRepository machinesRepository, IMapper mapper)
    {
        _machinesRepository = machinesRepository;
        _mapper = mapper;
    }
    
    
    public async Task<int> AddMachineAsync(AddNewMachineVM model)
    {
        var machine = _mapper.Map<Machine>(model);
        var id= await _machinesRepository.AddAsync(machine);
        return id;
    }
    

    public async Task<MachineDetailsVM> GetDetailsAsync(int id)
    {
        var machine = await _machinesRepository.GetByIdAsync(id);
        var machineVM = _mapper.Map<MachineDetailsVM>(machine);
        return machineVM;
    }

    public async Task<EditMachineVM> GetEditDetailsAsync(int id)
    {
        var machine = await _machinesRepository.GetByIdAsync(id);
        var machineVM = _mapper.Map<EditMachineVM>(machine);
        return machineVM;
    }
    
    
    public async Task EditMachineAsync(EditMachineVM model)
    {
        var machine = _mapper.Map<Machine>(model);
        await _machinesRepository.UpdateAsync(machine);
    }

    public async Task DeleteMachineAsync(int id)
    {
        await _machinesRepository.DeleteAsync(id);
    }

    public async Task<ListMachinesVM> GetAllMachines(int pageSize, int pageNo, string searchString,string sortOrder)
    {
        var query = _machinesRepository.GetAll()
            .Where(m => m.Name.StartsWith(searchString));

        query = sortOrder switch
        {
            "name_asc" => query.OrderBy(m => m.Name),
            "name_desc" => query.OrderByDescending(m => m.Name),
            "type_asc" => query.OrderBy(m => m.Type),
            "type_desc" => query.OrderByDescending(m => m.Type),
            "status_asc" => query.OrderBy(m => m.Status),
            "status_desc" => query.OrderByDescending(m => m.Status),
            _ => query.OrderBy(m => m.Name)
        };

        var count = await query.CountAsync();

        var machinesToShow = await query
            //.OrderBy(m => m.Name)
            .Skip((pageNo - 1) * pageSize)
            .Take(pageSize)
            .ProjectTo<MachinesForListVM>(_mapper.ConfigurationProvider)
            .ToListAsync();

        var machinesList = new ListMachinesVM()
        {
            PageSize = pageSize,
            CurrentPage = pageNo,
            SearchString = searchString,
            MachinesForListVms = machinesToShow,
            Count = count
        };

        return machinesList;
    }

    public async Task<bool> IsMachineBusyAsync(int machineId)
    {
        var machine = await _machinesRepository.GetByIdAsync(machineId);
        if (machine == null) throw new InvalidOperationException("Maszyna nie istnieje");
        return machine.Status== MachineStatus.Zajęta;
    }
    
    public async Task<bool> IsMachineBrokenAsync(int machineId)
    {
        var machine = await _machinesRepository.GetByIdAsync(machineId);
        if (machine == null) throw new InvalidOperationException("Maszyna nie istnieje");
        return machine.Status== MachineStatus.Uszkodzona;
    }
    
    

    
}









// public MachineDetailsVM GetDetails(int id)
// {
//     var machine = _machinesRepository.GetMachineDetailFromDB(id);
//     var machineVM = _mapper.Map<MachineDetailsVM>(machine);
//     return machineVM;
// }


// public int AddMachine(MachinesForListVM model)
// {
//     var machine = _mapper.Map<Machine>(model);
//     var id = _machinesRepository.AddMachineForDB(machine);
//     return id;
// }

// public void EditMachine(EditMachineVM model)
// {
//     var machine = _mapper.Map<Machine>(model);
//     _machinesRepository.EditMachineDB(machine);
// }

// public void DeleteMachine(int id)
// {
//     _machinesRepository.DeleteMachineFromDB(id);
// }

// public ListMachinesVM GetAllMachines(int pageSize, int pageNo, string searchString)
// {
//     var machines = _machinesRepository.GetAllMachinesFromDB().Where(m => m.Name.StartsWith(searchString))
//         .ProjectTo<MachinesForListVM>(_mapper.ConfigurationProvider).ToList();
//     var machinesToShow = machines.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToList();
//
//     var machinesList = new ListMachinesVM()
//     {
//         PageSize = pageSize,
//         CurrentPage = pageNo,
//         SearchString = searchString,
//         MachinesForListVms = machinesToShow,
//         Count = machines.Count
//     };
//     return machinesList;
// }