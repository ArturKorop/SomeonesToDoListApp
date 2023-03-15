using SomeonesToDoListApp.DataAccessLayer.Entities;
using SomeonesToDoListApp.Services.ViewModels;
using AutoMapper;

namespace SomeonesToDoListApp.Services
{
    public class AutoMapperConfiguration
    {
        public static void Initialize()
        {
            // Initializing the AutoMapper configuration for the to do mappings
            Mapper.Initialize((cfg) =>
            {
                cfg.AddProfile<ToDoMappingProfile>();
            });
        }
    }
    
    public class ToDoMappingProfile : Profile
    {
        
        public ToDoMappingProfile()
        {
            CreateMap<ToDo, ToDoViewModel>();
        }
    }
}
