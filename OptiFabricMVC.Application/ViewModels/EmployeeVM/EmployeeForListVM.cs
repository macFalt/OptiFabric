using AutoMapper;
using OptiFabricMVC.Application.Mapping;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Application.ViewModels.EmployeeVM;

public class EmployeeForListVM: IMapFrom<ApplicationUser>
{
  public string Id { get; set; }
  public string Name { get; set; }
  public string Surname { get; set; }
  public string Position { get; set; }
  public string Role { get; set; }
  
  public List<string> AvailabeRoles { get; set; }
  
  public void ConfigureMapping(Profile profile)
  {
    profile.CreateMap<ApplicationUser, EmployeeForListVM>().ReverseMap();
  }}