namespace EMS.App.ReqDto.CreateDataDto;

public class CreateCourseTeacherDto : Base.CreateDataDto
{


    public Guid TeacherId { get; set; }

    public Guid CourseId { get; set; }

}
