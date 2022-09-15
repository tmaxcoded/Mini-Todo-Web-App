



namespace TodoApp.Core.ProfileMapper
{
    public class MapModelProfile:Profile
    {
        public MapModelProfile()
        {
            CreateMap<Todo, PostTodoApp>().ReverseMap();
            CreateMap<Todo, GetTodoApp>().ReverseMap();
            CreateMap<UserForRegistration, User>().ReverseMap();
        }
    }
}
