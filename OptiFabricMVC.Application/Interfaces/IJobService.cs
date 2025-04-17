using OptiFabricMVC.Application.ViewModels.JobVM;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.Interfaces;

public interface IJobService
{
    Task<int> AddJob(AddNewJobVM model);
    Task EditJobAsync(EditJobVM model);
    Task<AddNewJobVM> GetSelectedJobAsync(int id);
    Task<ListJobsVM> GetAllJobsAsync(int pageSize, int pageNo, string searchString);
    
    


    

}





//AddNewJobVM GetSelectedJob(int id);
//void EditJob(AddNewJobVM model);
//int AddJob(AddNewJobVM model);
