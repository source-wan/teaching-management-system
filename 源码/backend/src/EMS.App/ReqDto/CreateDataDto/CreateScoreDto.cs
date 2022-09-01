namespace EMS.App.ReqDto.CreateDataDto;
public class CreateScoreDto : Base.CreateDataDto
{
    public Guid StudentId { get; set; }
    public Guid CourseId { get; set; }
    public int Score { get; set; } = 0;

    public int Term { get; set; }
}
