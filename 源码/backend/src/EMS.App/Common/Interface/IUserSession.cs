namespace EMS.App.Common.Interface
{
    public interface IUserSession
    {
        /// <summary>
        /// 用户Id, 从 Session 中获取
        /// </summary>
        /// <value></value>
        Guid? Id { get; }

        /// <summary>
        /// 账户
        /// </summary>
        /// <value></value>
        string? Account { get; }
    }
}