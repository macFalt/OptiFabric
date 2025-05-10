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
    

}












