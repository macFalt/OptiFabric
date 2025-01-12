using System.Reflection.PortableExecutable;
using Machine = OptiFabricMVC.Domain.Model.Machine;

namespace OptiFabricMVC.Domain.Interfaces;

public interface IMachinesRepository
{
    IQueryable<Machine> GetAllMachinesFromDB();
    int AddMachineForDB(Machine machine);
    Machine GetMachineDetailFromDB(int id);
    void EditMachineDB(Machine machine);
    void DeleteMachineFromDB(int id);
}