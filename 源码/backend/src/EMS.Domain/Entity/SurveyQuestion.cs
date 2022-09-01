namespace EMS.Domain.Entity
{
    [Table("survey_question")]
    public class SurveyQuestion : BaseEntity
    {
        [Column("survey_id")]
        public Guid SurveyId { get; set; }

        [Column("question_id")]
        public Guid QuestionId { get; set; }
        [Column("score")]
        public int Score { get; set; }

    }
}