namespace EMS.App.Common.Interface;
public interface ICreateDataServer<T>
{
    /// <summary>
    /// 向数据库插入数据
    /// </summary>
    /// <param name="entity">需要被插入的数据的实例</param>
    /// <returns>插入后的数据</returns>
    Task<T?> CreateDataAsync(T entity);

    /// <summary>
    /// 向数据库批量插入数据
    /// </summary>
    /// <param name="entities">需要被插入的数据集的集合</param>
    /// <returns>被插入后的数据集合</returns>
    Task<IEnumerable<T?>> CreateDataAsync(IEnumerable<T> entities);
}
public interface ICreateDataServer<TSource, TTarget>
{
    /// <summary>
    /// 向数据库插入数据
    /// </summary>
    /// <param name="entity">需要被插入的数据的实例</param>
    /// <returns>插入后的数据</returns>
    Task<TSource?> CreateDataAsync(TTarget entity);

    /// <summary>
    /// 向数据库批量插入数据
    /// </summary>
    /// <param name="entities">需要被插入的数据集的集合</param>
    /// <returns>被插入后的数据集合</returns>
    Task<IEnumerable<TSource?>> CreateDataAsync(IEnumerable<TTarget> entities);
}