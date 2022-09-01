namespace EMS.Domain.Entity
{
    [Table("app_file_info")]
    public class AppFileInfo: BaseEntity
    {
        [Column("orgin_name")]
        public string OriginName {get;set;}=null!;
        
        [Column("current_name")]
        public string CurrentName {get;set;}=null!;

        [Column("relative_path")]
        public string RelativePath {get;set;}=null!;

        [Column("content_type")]
        public string ContentType {get;set;}=null!;

        [Column("file_size")]
        public Int32 FileSize {get;set;}
    }
}