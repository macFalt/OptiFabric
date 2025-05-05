using AutoMapper;
using Microsoft.Extensions.Logging;
using OptiFabricMVC.Application.Interfaces;
using OptiFabricMVC.Application.ViewModels.JobEmployeeVM;
using OptiFabricMVC.Application.ViewModels.JobVM;
using OptiFabricMVC.Application.ViewModels.OperationVM;
using OptiFabricMVC.Domain.Interfaces;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.Services;

public class JobEmployeeService : IJobEmployeeService
{
    private readonly IMapper _mapper;
    private readonly IJobEmployeeRepository _jobEmployeeRepository;
    private readonly IJobRepository _jobRepository;
    private readonly IOperationRepository _operationRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMachinesRepository _machinesRepository;
    private readonly ILogger _logger;


    public JobEmployeeService(IMapper mapper, IJobEmployeeRepository jobEmployeeRepository,
        IJobRepository jobRepository, IOperationRepository operationRepository,
        IEmployeeRepository employeeRepository, IMachinesRepository machinesRepository,
        ILogger<JobEmployeeService> logger)
    {
        _mapper = mapper;
        _jobEmployeeRepository = jobEmployeeRepository;
        _jobRepository = jobRepository;
        _operationRepository = operationRepository;
        _employeeRepository = employeeRepository;
        _machinesRepository = machinesRepository;
        _logger = logger;
    }


    public async Task StartJobEmployee2(DateTime data, string? userId, int id, int selectedMachineId, int jobId)
    {
        if (_machinesRepository.IsMachineBusy(selectedMachineId))
        {
            throw new InvalidOperationException("Maszyna jest już zajęta");
        }


        if (_machinesRepository.IsMachineBroken(selectedMachineId))
        {
            throw new InvalidOperationException("Awaria maszyny");
        }

        var job = new JobEmployee()
        {
            CompletedQuantity = 0,
            EmployeeComments = "",
            MissingQuantity = 0,
            StartTime = data,
            CurrentWorkerId = userId,
            OperationId = id,
            MachineId = selectedMachineId,
            JobId = jobId
        };


        await _jobEmployeeRepository.StartJobEmployee(job);
    }

    public async Task StopJobEmployee(EndJobEmployeeVM model, DateTime data, string? userId, int id)
    {
        var endJob = _mapper.Map<JobEmployee>(model);
        var JobId = model.JobId;
        await _jobEmployeeRepository.StopJobEmployee(endJob, id, userId, JobId);
    }


    public async Task<List<JobEmployeeForListVM>> GetAllJobEmployeesByJobIdAsync(int JobId)
    {
        var jobEmployees = await _jobEmployeeRepository.GetAllJobEmployeeByJobId(JobId);
        var jobEmployeeVm = _mapper.Map<List<JobEmployeeForListVM>>(jobEmployees);
        return jobEmployeeVm;
    }

    public async Task<List<DetailsJobEmployeeVM>> GetAllJobsEmployeeDetails(int operationId)
    {
        var jel = await _jobEmployeeRepository.GetAllJobsEmployeeByOperationIdFromDB(operationId);
        var jobEmployeeList = _mapper.Map<List<DetailsJobEmployeeVM>>(jel);
        foreach (var jobEmployee in jobEmployeeList)
        {
            var user = _employeeRepository.GetEmployee(jobEmployee.CurrentWorkerId);

            if (user != null)
            {
                jobEmployee.EmployeeName = user.Name;
                jobEmployee.EmployeeSurname = user.Surname;
            }
        }


        return jobEmployeeList;
    }

    public async Task<DetailsJobEmployeeVM> GetJobEmployeeDetailsAsync(int id)
    {
        var jobEmployee = await _jobEmployeeRepository.GetByIdAsync(id);
        return _mapper.Map<DetailsJobEmployeeVM>(jobEmployee);
    }


    public async Task EditJobEmployee(EditJobEmployeeVM model)
    {
        var jobEmp = await _jobEmployeeRepository.GetByIdAsync(model.Id);
        jobEmp.CompletedQuantity = model.CompletedQuantity;
        jobEmp.MissingQuantity = model.MissingQuantity;
        await _jobEmployeeRepository.UpdateJobEmployee(jobEmp);
        await _jobEmployeeRepository.SaveChangesAsync();
        
        var jel = await _jobEmployeeRepository.GetAllJobsEmployeeByOperationIdFromDB(model.OperationId);
        var operations = await _operationRepository.GetAllOperationsByJobIdFromDB(model.JobId);

        foreach (var operation in operations)
        {
            operation.CompletedQuantity = jel
                .Where(emp => emp.OperationId == operation.Id)
                .Sum(emp => emp.CompletedQuantity);
            operation.MissingQuantity = jel
                .Where(emp => emp.OperationId == operation.Id)
                .Sum(emp => emp.MissingQuantity);
            await _operationRepository.UpdateOperation(operation);
        }



    }
}


//  public void StartJobEmployee(DateTime data, string? userId, int id)
// {
//     var job = new JobEmployee()
//     {
//         CompletedQuantity = 0,
//         EmployeeComments = "",
//         MissingQuantity = 0,
//         StartTime = data,
//         CurrentWorkerId = userId,
//         OperationId = id
//     };
//
//     _jobEmployeeRepository.StartJobEmployee(job);
// }


// public List<JobEmployee> GetAllJobsEmployee()
// {
//     var jel = _jobEmployeeRepository.GetAllJobsEmployeeFromDB();
//     return _mapper.Map<List<JobEmployee>>(jel);
// }
//

// public DetailsJobEmployeeVM GetJobEmployeeDetailById(int id)
// {
//     var jobEmployee = _jobEmployeeRepository.GetJobEmployeeFromDB(id);
//     var jobEmployeeVM = _mapper.Map<DetailsJobEmployeeVM>(jobEmployee);
//     return jobEmployeeVM;
// }

// public async Task EditJobEmployeeAsync(EditJobEmployeeVM model)
// {
//     var jobEmployee = _mapper.Map<JobEmployee>(model);
//     await _jobEmployeeRepository.UpdateAsync(jobEmployee);
// }