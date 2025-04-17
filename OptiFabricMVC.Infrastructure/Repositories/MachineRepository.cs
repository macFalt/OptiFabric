using System.Reflection.PortableExecutable;
using OptiFabricMVC.Domain.Interfaces;
using OptiFabricMVC.Domain.Model;
using Machine = OptiFabricMVC.Domain.Model.Machine;

namespace OptiFabricMVC.Infrastructure.Repositories;

public class MachineRepository: GenericRepository<Machine, int>, IMachinesRepository
{
    private readonly Context _context;

    public MachineRepository(Context context) : base(context)
    {
        _context = context;
    }
    
    public bool IsMachineBusy(int selectedMachineId)
    {
        return _context.Machines.Any(je => je.Id == selectedMachineId && je.Status== MachineStatus.Zajęta);
    }
    
    public bool IsMachineBroken(int selectedMachineId)
    {
        return _context.Machines.Any(je => je.Id == selectedMachineId && je.Status== MachineStatus.Uszkodzona);
    }

}




















// public IQueryable<Machine> GetAllMachinesFromDB()
// {
//     return _context.Machines.AsQueryable();
// }

// public int AddMachineForDB(Machine machine)
// {
//      _context.Machines.Add(machine);
//      _context.SaveChanges();
//      return machine.Id;
//
// }
//
// public Machine GetMachineDetailFromDB(int id)
// {
//     return _context.Machines.FirstOrDefault(m => m.Id == id);
// }
//
// public void EditMachineDB(Machine machine)
// {
//     var mach = _context.Machines.FirstOrDefault(m => m.Id == machine.Id);
//     mach.Name = machine.Name;
//     mach.Type = machine.Type;
//     mach.Status = machine.Status;
//     _context.SaveChanges();
// }
//
// public void DeleteMachineFromDB(int id)
// {
//     var machines = _context.Machines.FirstOrDefault(m => m.Id == id);
//     
//     _context.Machines.Remove(machines);
//     _context.SaveChanges();
// }