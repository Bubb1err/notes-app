using AutoMapper;
using Twits.Data.Models;
using Twits.Data.Models.ViewModels;

namespace Twits.API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            //CreateMap<LocalUser, RegisterVM>().ReverseMap();
            CreateMap<Note, NotesVM>().ForMember(x => x.Author,
                m => m.MapFrom(a => a.LocalUser.UserName))
                .ForMember(x => x.Title,
                m => m.MapFrom(a => a.Title))
                .ForMember(x => x.Content,
                m => m.MapFrom(a => a.Content))
                .ForMember(x => x.Category,
                m => m.MapFrom(a => a.Category.ToString()))
                .ForMember(x => x.Created,
                m => m.MapFrom(a => a.CreationDate))
                .ForMember(x => x.Updated,
                m => m.MapFrom(a => a.LastModifiedDate)).ReverseMap();
            //CreateMap<NoteCreateVM, Note>()
            //    .ForMember(x => x.Title, 
            //    m => m.MapFrom(a => a.Title))
            //    .ForMember(x => x.Content, 
            //    m => m.MapFrom(a => a.Content))
            //    .ForMember()
        }
    }
}
