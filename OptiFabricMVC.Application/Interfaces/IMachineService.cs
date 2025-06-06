using OptiFabricMVC.Application.ViewModels.MachinesVM;
using OptiFabricMVC.Domain.Interfaces;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.Interfaces;

public interface IMachineService 
{

    Task<int> AddMachineAsync(AddNewMachineVM model);
    Task<MachineDetailsVM> GetDetailsAsync(int id);
    Task EditMachineAsync(EditMachineVM model);
    Task DeleteMachineAsync(int id);
    Task<ListMachinesVM> GetAllMachines(int pageSize, int pageNo, string searchString,string sortOrder);
    Task<EditMachineVM> GetEditDetailsAsync(int id);

    Task<bool> IsMachineBusyAsync(int machineId);
    Task<bool> IsMachineBrokenAsync(int machineId);

}



//int AddMachine(MachinesForListVM model);

// MachineDetailsVM GetDetails(int id);

// void EditMachine(EditMachineVM model);

// void DeleteMachine(int id);

// ListMachinesVM GetAllMachines(int pageSize, int pageNo, string searchString);




