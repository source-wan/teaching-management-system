namespace EMS.App.Common.Interface;
public interface IDeleteDataServer<T>
{   
    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="entity">需要被删除的数据的实例</param>
    /// <returns></returns>
    Task<T> DeleteDataAsync(T entity);

    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="entity">需要被移除是实体列表</param>
    /// <returns>被移除的数据集</returns>
    Task<IEnumerable<T>> DeleteDataAsync(IEnumerable<T> entities);

    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="id">需要被移除的实体的Id的集合</param>
    /// <returns>被移除的数据集</returns>
    Task<T?> DeleteDataAsync(Guid id);

    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="id">需要被移除的实体的Id的集合</param>
    /// <returns>被移除的数据集</returns>
    Task<IEnumerable<T?>> DeleteDataAsync(IEnumerable<Guid> id);
}
