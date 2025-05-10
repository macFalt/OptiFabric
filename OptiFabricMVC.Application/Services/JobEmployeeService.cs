using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    private readonly IMachineService _machineService;


    public JobEmployeeService(IMapper mapper, IJobEmployeeRepository jobEmployeeRepository,
        IJobRepository jobRepository, IOperationRepository operationRepository,
        IEmployeeRepository employeeRepository, IMachinesRepository machinesRepository,
        ILogger<JobEmployeeService> logger, IMachineService machineService)
    {
        _mapper = mapper;
        _jobEmployeeRepository = jobEmployeeRepository;
        _jobRepository = jobRepository;
        _operationRepository = operationRepository;
        _employeeRepository = employeeRepository;
        _machinesRepository = machinesRepository;
        _logger = logger;
        _machineService = machineService;
    }
    
    //*********************************************************
    //************************StartJob*************************
    //*********************************************************
    public async Task StartJobEmployeeAsync(DateTime startTime, string? userId, int operationId, int machineId, int jobId)
    {
        await ValidateMachineStatusAsync(machineId);

        var jobEmployee = CreateJobEmployee(startTime, userId, operationId, machineId, jobId);

        await UpdateOperationStatusAsync(operationId);
        await UpdateJobStatusAsync(jobId);
        await UpdateMachineStatusAsync(machineId);

        await _jobEmployeeRepository.StartJobEmployee(jobEmployee);
    }

    private async Task ValidateMachineStatusAsync(int machineId)
    {
        if (await _machineService.IsMachineBusyAsync(machineId))
            throw new InvalidOperationException("Maszyna jest już zajęta");

        if (await _machineService.IsMachineBrokenAsync(machineId))
            throw new InvalidOperationException("Awaria maszyny");
    }

    private JobEmployee CreateJobEmployee(DateTime startTime, string? userId, int operationId, int machineId, int jobId)
    {
        return new JobEmployee
        {
            StartTime = startTime,
            CurrentWorkerId = userId,
            OperationId = operationId,
            MachineId = machineId,
            JobId = jobId,
            CompletedQuantity = 0,
            MissingQuantity = 0,
            EmployeeComments = string.Empty,
            IsActive = true
        };
    }

    private async Task UpdateOperationStatusAsync(int operationId)
    {
        var operation = await _operationRepository.GetOperationFromDB(operationId);
        if (operation != null)
        {
            operation.OperationStatus = OperationStatus.InProgress;
            await _operationRepository.UpdateOperation(operation);
        }
    }

    private async Task UpdateJobStatusAsync(int jobId)
    {
        var job = await _jobRepository.GetByIdAsync(jobId);
        if (job != null)
        {
            job.JobStatus = JobStatus.InProgress;
            job.ActivEmployeeJob = true;
            await _jobRepository.UpdateAsync(job);
        }
    }

    private async Task UpdateMachineStatusAsync(int machineId)
    {
        var machine = await _machinesRepository.GetByIdAsync(machineId);
        if (machine != null)
        {
            machine.Status = MachineStatus.Zajęta;
            await _machinesRepository.UpdateAsync(machine);
        }
    }
    
    //*********************************************************
    //*********************************************************
    //*********************************************************
    
    //*********************************************************
    //*********************StopJobEmployee*********************
    //*********************************************************
    public async Task StopJobEmployeeAsync(EndJobEmployeeVM model, string? userId)
    {
        var job=await _jobRepository.GetByIdAsync(model.JobId);
        var jobEmp=await _jobEmployeeRepository.GetActiveJobEmployeeByUserId(userId);
        var operation= await _operationRepository.GetOperationFromDB(model.OperationId);
        var machine= await _machinesRepository.GetByIdAsync(jobEmp.MachineId);
        var operations=await _operationRepository.GetAllOperationsByJobIdFromDB(model.JobId);

        if (operation != null)
        {
            UpdateOperation(operation,model);
        }

        if (jobEmp != null)
        {
            UpdateJobEmployee(jobEmp,model);
        }
        
        if (machine != null)
        {
            machine.Status= MachineStatus.Wolna;
        }

        if (job != null)
        {
            UpdateJob(operations,model,job);
        }
        await _jobEmployeeRepository.SaveChangesAsync();
    }

    private void UpdateOperation(Operation operation, EndJobEmployeeVM model)
    {
            operation.CompletedQuantity+=model.CompletedQuantity;
            operation.MissingQuantity+=model.MissingQuantity; 
            operation.OperationStatus = operation.CompletedQuantity+operation.MissingQuantity==operation.RequiredQuantity ? OperationStatus.Completed : OperationStatus.InProgress;
        
    }

    private void UpdateJobEmployee(JobEmployee jobEmp, EndJobEmployeeVM model)
    {
        jobEmp.EndTime = model.EndTime;
        jobEmp.CompletedQuantity = model.CompletedQuantity;
        jobEmp.EmployeeComments = model.EmployeeComments;
        jobEmp.MissingQuantity = model.MissingQuantity;
        jobEmp.IsActive = false; 
    }

    private void UpdateJob(List<Operation> operations, EndJobEmployeeVM model,Job job)
    {
        var lastOperation = operations
            .OrderByDescending(op => op.Id)
            .FirstOrDefault();
        

        if (lastOperation != null)
        {
            job.TotalCompletedQuantity = lastOperation.CompletedQuantity;
            job.TotalMissingQuantity = lastOperation.MissingQuantity;
        }
        
        if ((job.TotalCompletedQuantity+job.TotalMissingQuantity) >= job.RequiredQuantity)
        {
            job.JobStatus = JobStatus.Completed;
            job.CompletedAt = model.EndTime;
        }
        else
        {
            job.JobStatus = JobStatus.InProgress;
        }
    }
    
    
    //*********************************************************
    //*********************************************************
    //*********************************************************

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
            var user = await _employeeRepository.GetEmployee(jobEmployee.CurrentWorkerId);

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

