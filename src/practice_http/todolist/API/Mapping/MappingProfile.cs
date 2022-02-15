namespace API.Mapping
{
    using AutoMapper;
    using ViewTodos = View.Todo;
    using ModelTodos = Model.Todo;

    internal sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.AllowNullCollections = true;

            this.CreateTodosMapping();
        }

        private void CreateTodosMapping()
        {
            this.CreateMap<ViewTodos.TodoBuildInfo, ModelTodos.TodoBuildInfo>(MemberList.None);
            this.CreateMap<ViewTodos.Todo, ModelTodos.Todo>(MemberList.None).ReverseMap();
            this.CreateMap<ViewTodos.TodoInfo, ModelTodos.TodoInfo>(MemberList.None).ReverseMap();
            this.CreateMap<ViewTodos.TodoPatchInfo, ModelTodos.TodoPatchInfo>(MemberList.None).ReverseMap();
            this.CreateMap<ViewTodos.TodoInfoSearchQuery, ModelTodos.TodoPatchInfo>(MemberList.None).ReverseMap();
        }
    }
}
