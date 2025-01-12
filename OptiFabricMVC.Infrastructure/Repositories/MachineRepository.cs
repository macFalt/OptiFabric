using System.Reflection.PortableExecutable;
using OptiFabricMVC.Domain.Interfaces;
using Machine = OptiFabricMVC.Domain.Model.Machine;

namespace OptiFabricMVC.Infrastructure.Repositories;

public class MachineRepository: IMachinesRepository
{
    private readonly Context _context;

    public MachineRepository(Context context)
    {
        _context = context;
    }
    
    public IQueryable<Machine> GetAllMachinesFromDB()
    {
        return _context.Machines.AsQueryable();
    }

    public int AddMachineForDB(Machine machine)
    {
         _context.Machines.Add(machine);
         _context.SaveChanges();
         return machine.Id;

    }

    public Machine GetMachineDetailFromDB(int id)
    {
        return _context.Machines.FirstOrDefault(m => m.Id == id);
    }

    public void EditMachineDB(Machine machine)
    {
        var mach = _context.Machines.FirstOrDefault(m => m.Id == machine.Id);
        mach.Name = machine.Name;
        mach.Type = machine.Type;
        mach.Status = machine.Status;
        _context.SaveChanges();
    }

    public void DeleteMachineFromDB(int id)
    {
        var machines = _context.Machines.FirstOrDefault(m => m.Id == id);
        
        _context.Machines.Remove(machines);
        _context.SaveChanges();
    }
}