namespace EMS.App.ReqDto.UpdateDataDto;

public class UpdateCourseDto : Base.UpdateDataDto
{
    public string? CourseName { get; set; }
    public decimal? Credit { get; set; }
    public string? Period { get; set; }
}
