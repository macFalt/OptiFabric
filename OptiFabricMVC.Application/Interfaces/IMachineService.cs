using OptiFabricMVC.Application.ViewModels.MachinesVM;

namespace OptiFabricMVC.Application.Interfaces;

public interface IMachineService
{
    ListMachinesVM GetAllMachines(int pageSize, int pageNo, string searchString);
    int AddMachine(MachinesForListVM model);
    MachineDetailsVM GetDetails(int id);
    void EditMachine(EditMachineVM model);
    void DeleteMachine(int id);
}