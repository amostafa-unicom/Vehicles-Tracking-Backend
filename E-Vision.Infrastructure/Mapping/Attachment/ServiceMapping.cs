using AutoMapper;
using CarService.Core.UseCases.Lookup.Attachments.AttachmentsAddUseCase;

namespace E_Vision.Infrastructure.Mapping.Attachment
{
    public class AttachmentMapping : Profile
    {
        public AttachmentMapping() 
        {
            CreateMap<AttachmentsAddInputDto, Core.Entities.Attachment>();


        }
    }
}
