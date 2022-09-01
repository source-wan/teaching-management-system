
namespace EMS.App.ReqDto.UpdateDataDto;
public class UpdateScoreDto : Base.UpdateDataDto
{
    public Guid StudentId { get; set; }
    public Guid? CourseId { get; set; }
    public int? Score { get; set; }
}
