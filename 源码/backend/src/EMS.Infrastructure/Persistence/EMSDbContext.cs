using Microsoft.EntityFrameworkCore;
using EMS.Domain.Entity;

namespace EMS.Infrastructure.Persistence
{
    public class EMSDbContext : DbContext
    {
        public EMSDbContext(DbContextOptions options) : base(options: options) { }
        public DbSet<AppException> AppException => Set<AppException>();
        public DbSet<IdentityUser> IdentityUser => Set<IdentityUser>();
        public DbSet<RoleInfo> RoleInfo => Set<RoleInfo>();
        public DbSet<UserGroupInfo> UserGroupInfo => Set<UserGroupInfo>();
        public DbSet<UserInfo> UserInfo => Set<UserInfo>();
        public DbSet<UserGroupUserInfo> UserGroupUserInfo => Set<UserGroupUserInfo>();
        public DbSet<AppFileInfo> FileInfo => Set<AppFileInfo>();
        public DbSet<CampusInfo> CampusInfo => Set<CampusInfo>();
        public DbSet<AcademyInfo> AcademyInfo => Set<AcademyInfo>();
        public DbSet<MajorInfo> MajorInfo => Set<MajorInfo>();
        public DbSet<ClassInfo> ClassInfo => Set<ClassInfo>();
        public DbSet<CourseSchedulingInfo> CourseSchedulingInfo => Set<CourseSchedulingInfo>();
        public DbSet<StudentInfo> StudentInfo => Set<StudentInfo>();
        public DbSet<TextBookInfo> TextBookInfo => Set<TextBookInfo>();
        public DbSet<CourseInfo> CourseInfo => Set<CourseInfo>();
        public DbSet<ScoreInfo> ScoreInfo => Set<ScoreInfo>();
        public DbSet<AcademyTeacher> AcademyTeacher => Set<AcademyTeacher>();
        public DbSet<ClassCourse> ClassCourse => Set<ClassCourse>();
        public DbSet<ClassStudent> ClassStudent => Set<ClassStudent>();
        public DbSet<StudentMajor> StudentMajor => Set<StudentMajor>();
        public DbSet<TeacherInfo> TeacherInfo => Set<TeacherInfo>();
        public DbSet<CourseTextBook> CourseTextBook => Set<CourseTextBook>();
        public DbSet<SurveyInfo> SurveyInfo => Set<SurveyInfo>();
        public DbSet<SurveyQuestion> SurveyQuestion => Set<SurveyQuestion>();
        public DbSet<QuestionInfo> QuestionInfo => Set<QuestionInfo>();
    }
}