using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OptiFabricMVC.Application.Interfaces;
using OptiFabricMVC.Application.ViewModels.MachinesVM;
using OptiFabricMVC.Application.ViewModels.OperationVM;
using OptiFabricMVC.Domain.Interfaces;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.Services;

public class OperationService : IOperationService
{
    private readonly IOperationRepository _operationRepository;
    private readonly IMapper _mapper;

    public OperationService(IOperationRepository operationRepository, IMapper mapper)
    {
        _operationRepository = operationRepository;
        _mapper = mapper;
    }

    public async Task<int> AddNewOperationPatternAsync(OperationPatternForListVM model)
    {
        var operation = _mapper.Map<OperationPattern>(model);
        return await _operationRepository.AddAsync(operation);
    }

    public async Task EditOperationAsync(OperationPatternForListVM model)
    {
        var operation = _mapper.Map<OperationPattern>(model);
        await _operationRepository.UpdateAsync(operation);
    }

    public async Task DeleteOperationAsync(int id)
    {
         await _operationRepository.DeleteAsync(id);
    }

    public async Task<OperationPatternForListVM> GetOperationPatternAsync(int id)
    {
        var operation = await _operationRepository.GetByIdAsync(id);
        return _mapper.Map<OperationPatternForListVM>(operation);
    }

    
    public ListOperationVM GetAllOperations(int jobId,int ProductId, int pageSize, int pageNo, string searchString)
    {
        var operationsList = _operationRepository.GetAllOperationsFromDB()
            .Where(m => m.Name.StartsWith(searchString) && m.JobId == jobId)
            .ProjectTo<OperationForListVM>(_mapper.ConfigurationProvider).ToList();
        var operationsToShow = operationsList.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToList();
        
        var operationListVM = new ListOperationVM()
        {
            PageSize = pageSize,
            CurrentPage = pageNo,
            SearchString = searchString,
            OperationForListVms = operationsToShow,
            Count = operationsList.Count,
            JobId = jobId,
        };
        return operationListVM;
    }
    
    
     public async Task<ListOperationPatternVM> GetAllOperationsPattern(int productId, int pageSize, int pageNo, string searchString)
    {
        var baseQuery = _operationRepository.GetAllOperationsPatternFromDB(productId)
            .Where(m => m.Name.StartsWith(searchString));

        var count = await baseQuery.CountAsync();

        var operationsPatternToShow = await baseQuery
            .OrderBy(m => m.Name)
            .Skip((pageNo - 1) * pageSize)
            .Take(pageSize)
            .ProjectTo<OperationPatternForListVM>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return new ListOperationPatternVM
        {
            PageSize = pageSize,
            CurrentPage = pageNo,
            SearchString = searchString,
            OperationPatternForListVms = operationsPatternToShow,
            Count = count
        };
    }
    
}










// public async Task<ListOperationPatternVM> GetAllOperationPatternAsync( int ProductId, int pageSize, int pageNo, string searchString)
// {
//     var query = _operationRepository.GetAll()
//         .Where(m => m.Name.StartsWith(searchString));
//
//     var count = await query.CountAsync();
//
//     var operationsToShow = await query
//         .OrderBy(m => m.Name)
//         .Skip((pageNo - 1) * pageSize)
//         .Take(pageSize)
//         .ProjectTo<OperationPatternForListVM>(_mapper.ConfigurationProvider)
//         .ToListAsync();
//
//     var operationList = new ListOperationPatternVM()
//     {
//         PageSize = pageSize,
//         CurrentPage = pageNo,
//         SearchString = searchString,
//         OperationPatternForListVms = operationsToShow,
//         Count = count
//     };
//
//     return operationList;
// }



















// public ListOperationVM GetAllOperations(int jobId,int ProductId, int pageSize, int pageNo, string searchString)
    // {
    //     var operationsList = _operationRepository.GetAllOperationsFromDB()
    //         .Where(m => m.Name.StartsWith(searchString) && m.JobId == jobId)
    //         .ProjectTo<OperationForListVM>(_mapper.ConfigurationProvider).ToList();
    //     var operationsToShow = operationsList.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToList();
    //     
    //     var operationListVM = new ListOperationVM()
    //     {
    //         PageSize = pageSize,
    //         CurrentPage = pageNo,
    //         SearchString = searchString,
    //         OperationForListVms = operationsToShow,
    //         Count = operationsList.Count,
    //         JobId = jobId,
    //     };
    //     return operationListVM;
    // }
    

    // public OperationForListVM GetOperationById(int id)
    // {
    //     var operation = _operationRepository.GetOperationFromDB(id);
    //     return _mapper.Map<OperationForListVM>(operation);
    // }

    // public void EditOperation(OperationForListVM model)
    // {
    //     var operation = _mapper.Map<Operation>(model);
    //     _operationRepository.EditOperationDB(operation);
    // }

    // public ListOperationPatternVM GetAllOperationsPattern(int productId, int pageSize, int pageNo, string searchString)
    // {
    //     var operationsPatternList = _operationRepository.GetAllOperationsPatternFromDB(productId)
    //         .Where(m => m.Name.StartsWith(searchString) && m.ProductId == productId)
    //         .ProjectTo<OperationPatternForListVM>(_mapper.ConfigurationProvider).ToList();
    //     var operationsPatternToShow = operationsPatternList.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToList();
    //
    //     var operationPatternListVM = new ListOperationPatternVM()
    //     {
    //         PageSize = pageSize,
    //         CurrentPage = pageNo,
    //         SearchString = searchString,
    //         OperationPatternForListVms = operationsPatternToShow,
    //         Count = operationsPatternList.Count,
    //     };
    //
    //     return operationPatternListVM;
    // }
    //
    // public void AddNewOperationPattern(OperationPatternForListVM model)
    // {
    //     var operation = _mapper.Map<OperationPattern>(model);
    //     _operationRepository.AddOperatonPatternToDB(operation);
    //     
    // }