
namespace EMS.App.Common.Interface;
/// <summary>
/// 用于数据更新的服务
/// </summary>
/// <typeparam name="TSource">原本的数据的类型</typeparam>
/// <typeparam name="TTarget">原本的数据的差异类型，需要具有和原来的类型一样的属性名，且类型可以转换</typeparam>
public interface IUpdateDataServer<TSource, TTarget>
{
    /// <summary>
    /// 更新指定的数据到数据库
    /// </summary>
    /// <param name="origin">需要被更新的源数据</param>
    /// <param name="target">希望被更新成的样子</param>
    /// <returns>源数据的备份</returns>
    Task<TSource> UpdateDataAsync(TSource source, TTarget target);
}

public interface IUpdateDataServer<T>
{
    /// <summary>
    /// 更新指定的数据到数据库
    /// </summary>
    /// <param name="origin">需要被更新的源数据</param>
    /// <param name="target">希望被更新成的样子</param>
    /// <returns>源数据的备份</returns>
    Task<T> UpdateDataAsync(T source, T target);

    /// <summary>
    /// 批量更新数据到数据库
    /// </summary>
    /// <param name="source">需要被更新的数据</param>
    /// <param name="target">目标</param>
    /// <returns>更新前的数据集合</returns>
    Task<IEnumerable<T>> UpdateDataAsync(IEnumerable<T> source, IEnumerable<T> target);
}