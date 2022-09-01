using EMS.App.ResDto;

namespace EMS.App.Common.Interface;
public interface ICRUDServer<TSource, TCreateDataDto, TUpdateDataDto>
{
    /// <summary>
    /// 批量插入数据
    /// </summary>
    /// <param name="datas">需要被插入的数据实例</param>
    /// <returns>结果对象</returns>
    Task<ResponseDto> CreateDataAsync(IEnumerable<TCreateDataDto> datas);

    /// <summary>
    /// 插入一条数据
    /// </summary>
    /// <param name="data">需要被插入的数据实例</param>
    /// <returns>结果对象</returns>
    Task<ResponseDto> CreateDataAsync(TCreateDataDto data);

    /// <summary>
    /// 更新数据集
    /// </summary>
    /// <param name="datas">需要被更新的数据集</param>
    /// <returns>结果对象</returns>
    // Task<ResponseDto> UpdateDataAsync(IEnumerable<TData> datas);

    /// <summary>
    /// 更新数据集
    /// </summary>
    /// <param name="id">需要被更新的数据的Id</param>
    /// <param name="data">需要被更新成的目标</param>
    /// <returns>结果对象</returns>
    Task<ResponseDto> UpdateDataAsync(Guid id, TUpdateDataDto data);

    /// <summary>
    /// 获取符合条件的数据集,
    /// 此函数会根据请求的参数进行分页
    /// </summary>
    /// <param name="checker">数据需要满足的条件</param>
    /// <returns>结果对象</returns>
    Task<ResponseDto> GetData(Func<TSource, bool> checker);

    /// <summary>
    /// 根据 id 获取数据， 数据不存在时返回 null
    /// </summary>
    /// <param name="id">数据的Id</param>
    /// <returns>结果对象</returns>
    Task<ResponseDto> GetDataById(Guid id);

    /// <summary>
    /// 根据 id 删除数据
    /// </summary>
    /// <param name="id">需要被删除的数据</param>
    /// <returns>结果对象</returns>
    Task<ResponseDto> DeleteDataAsync(Guid id);

    /// <summary>
    /// 根据 id 删除数据
    /// </summary>
    /// <param name="id">需要被删除的数据集</param>
    /// <returns>结果对象</returns>
    Task<ResponseDto> DeleteDataAsync(IEnumerable<Guid> id);
}
