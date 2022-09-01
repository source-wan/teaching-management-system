namespace EMS.App.ReqDto.CreateDataDto;

public class CreateClassCourseDto : Base.CreateDataDto
{


    public Guid ClassId { get; set; }

    public Guid CourseId { get; set; }
    public Guid TeacherId { get; set; }
    public Guid BookId { get; set; }
    public int Term { get; set; }
}
