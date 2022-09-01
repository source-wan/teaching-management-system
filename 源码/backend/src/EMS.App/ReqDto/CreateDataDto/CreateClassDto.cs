namespace EMS.App.ReqDto.CreateDataDto;

public class CreateClassDto : Base.CreateDataDto
{
    
        public string ClassName { get; set; } = null!;
        public Guid InstroctorId {get;set;}
        public Int16 ClassCode { get; set; }

        public Guid MajorId { get; set; }
}
