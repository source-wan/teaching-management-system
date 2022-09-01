namespace EMS.App.ReqDto.UpdateDataDto;

public class UpdateClassCourseDto : Base.UpdateDataDto
{


    public Guid? ClassId { get; set; }

    public Guid? CourseId { get; set; }

    public int? Term { get; set; }
}
