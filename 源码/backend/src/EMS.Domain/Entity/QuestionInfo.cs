namespace EMS.Domain.Entity
{
    [Table("question_info")]
    public class QuestionInfo : BaseEntity
    {
        [Column("survey_id")]
        public Guid SurveyId { get; set; }
        [Column("question_name")]
        public string QuestionName { get; set; }=null!;
    }
}