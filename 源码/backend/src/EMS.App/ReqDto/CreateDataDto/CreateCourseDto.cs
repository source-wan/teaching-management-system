namespace EMS.App.ReqDto.CreateDataDto;

public class CreateCourseDto : Base.CreateDataDto
{
    public string CourseName { get; set; } = null!;
    public decimal Credit { get; set; }
    public string Period { get; set; } = null!;
}
