namespace EMS.App.ResDto;
public class CurrentStudentScoreDto
{
    public string CourseName { get; set; } = null!;
    public int Score { get; set; }
    public decimal Credit { get; set; }

    public int Term {get;set;}
}
