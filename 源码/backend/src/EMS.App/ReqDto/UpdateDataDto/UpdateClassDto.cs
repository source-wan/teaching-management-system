namespace EMS.App.ReqDto.UpdateDataDto;

public class UpdateClassDto : Base.UpdateDataDto
{
    
        public string? ClassName { get; set; }

        public Guid? InstroctorId {get;set;}

        public Int16? ClassCode { get; set; }

        public Guid? MajorId { get; set; }
}
