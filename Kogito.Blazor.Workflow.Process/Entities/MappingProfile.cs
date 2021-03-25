using AutoMapper;

namespace Kogito.Blazor.Workflow.Process.Entities
{
    public class MappingProfile : Profile
    {
        // We only have this here so we can create an instance in Startup.cs and use DI in the project
        // Since we are only using Dictionary<string, object> as mapping sources, we don't need custom profiles
        // If we DID need custom profiles, you would write them kinda like this:

        //public MappingProfile()
        //{
        //    CreateMap<AuthorModel, AuthorDto>()
        //        .ForMember(destination => destination.ContactDetails, opts => opts.MapFrom(source => source.Contact));
        //
        //    CreateMap<PersonModel, PersonDto>()
        //        .ForMember(destination => destination.FirstName, opts => opts.MapFrom(source => source.First_Name))
        //        .ForMember(destination => destination.LastName, opts => opts.MapFrom(source => source.Last_Name));
        //        .ForMember(destination => destination.EmailAddress, opts => opts.MapFrom(source => source.Email_Address));
        //
        //    CreateMap<FROMCLASS, TOCLASS>()
        //    .ForMember(destination => destination.THIS_IS_ON_TOCLASS, opts => opts.MapFrom(source => source.THIS_IS_ON_FROMCLASS));
        //}
        //
        //                                                                              v-- variable of type FROMCLASS
        // then throughout the project we could do things like _mapper.Map<TOCLASS>(sourceObject);
        //                                                                    ^- tell mapper what we want
    }
}