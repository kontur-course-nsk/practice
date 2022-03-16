using AutoMapper;
using ViewPosts = View.Posts;
using ModelPosts = Model.Posts;

namespace Blog.Mapping
{
    internal sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.AllowNullCollections = true;

            this.CreateMap<ViewPosts.Post, ModelPosts.Post>(MemberList.None).ReverseMap();
            this.CreateMap<ViewPosts.PostShortInfo, ModelPosts.Post>(MemberList.None).ReverseMap();
            this.CreateMap<ViewPosts.PostCreateInfo, ModelPosts.PostCreateInfo>(MemberList.None).ReverseMap();
            this.CreateMap<ViewPosts.PostSearchInfo, ModelPosts.PostSearchInfo>(MemberList.None).ReverseMap();
            this.CreateMap<ViewPosts.PostsList, ModelPosts.PostsList>(MemberList.None).ReverseMap();
            this.CreateMap<ViewPosts.PostUpdateInfo, ModelPosts.PostUpdateInfo>(MemberList.None).ReverseMap();
        }
    }
}
