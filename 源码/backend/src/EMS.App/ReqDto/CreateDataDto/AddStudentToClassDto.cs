namespace EMS.App.ReqDto.CreateDataDto;
public class AddStudentToClassDto : Base.CreateDataDto
{
    public Guid StudentId { get; set; }

    public Guid MajorId {get;set;}

    public int JoinTime { get; set; } = 0;
}
