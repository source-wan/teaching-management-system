namespace EMS.App.ReqDto.UpdateDataDto;

public class UpdateCourseTeacherDto : Base.UpdateDataDto
{


    public Guid? TeacherId { get; set; }

    public Guid? CourseId { get; set; }

}
