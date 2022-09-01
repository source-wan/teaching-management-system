namespace EMS.App.ReqDto.CreateDataDto;

public class CreateCourseTextBookDto : Base.CreateDataDto
{


    public Guid TextBookId { get; set; }

    public Guid CourseId { get; set; }

}
