using AutoMapper;
using AutoMapper.QueryableExtensions;
using OptiFabricMVC.Application.Interfaces;
using OptiFabricMVC.Application.ViewModels.MachinesVM;
using OptiFabricMVC.Domain.Interfaces;
using OptiFabricMVC.Domain.Model;

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

    public ListMachinesVM GetAllMachines(int pageSize, int pageNo, string searchString)
    {
        var machines = _machinesRepository.GetAllMachinesFromDB().Where(m => m.Name.StartsWith(searchString))
            .ProjectTo<MachinesForListVM>(_mapper.ConfigurationProvider).ToList();
        var machinesToShow = machines.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToList();

        var machinesList = new ListMachinesVM()
        {
            PageSize = pageSize,
            CurrentPage = pageNo,
            SearchString = searchString,
            MachinesForListVms = machinesToShow,
            Count = machines.Count
        };
        return machinesList;
    }

    public int AddMachine(MachinesForListVM model)
    {
        var machine = _mapper.Map<Machine>(model);
        var id = _machinesRepository.AddMachineForDB(machine);
        return id;
    }

    public MachineDetailsVM GetDetails(int id)
    {
        var machine = _machinesRepository.GetMachineDetailFromDB(id);
        var machineVM = _mapper.Map<MachineDetailsVM>(machine);
        return machineVM;
    }

    public void EditMachine(EditMachineVM model)
    {
        var machine = _mapper.Map<Machine>(model);
        _machinesRepository.EditMachineDB(machine);
    }

    public void DeleteMachine(int id)
    {
        _machinesRepository.DeleteMachineFromDB(id);
    }
}