namespace EMS.App.Common.Interface;
public interface IGetDataServer<T>
{
    /// <summary>
    /// 根据 checker 来获取数据
    /// </summary>
    /// <param name="checker"></param>
    /// <param name="count">符合条件的记录条数</parma>
    /// <returns>符合条件的结果集</returns>
    IEnumerable<T> GetDataList(Func<T, bool> checker, out int count);

    /// <summary>
    /// 根据 Id 获取数据, 当不存在该数据时， 返回 null
    /// </summary>
    /// <param name="id">需要获取的数据的Id</param>
    /// <returns>取回的数据</returns>
    Task<T?> GetDataById(Guid id);
}
