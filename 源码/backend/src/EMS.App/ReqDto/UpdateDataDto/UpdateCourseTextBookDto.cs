namespace EMS.App.ReqDto.UpdateDataDto;

public class UpdateCourseTextBookDto : Base.UpdateDataDto
{


    public Guid? TextBookId { get; set; }

    public Guid? CourseId { get; set; }

}
