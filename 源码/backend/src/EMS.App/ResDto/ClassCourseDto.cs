namespace EMS.App.ResDto;

public class ClassCourseDto
{
    public Guid Id {get;set;}
    public int Term { get; set; }

    public string? courseName {get;set;}

    public string? courseTeacherName {get;set;}

    public string? courseTextBook{get;set;} 
}
