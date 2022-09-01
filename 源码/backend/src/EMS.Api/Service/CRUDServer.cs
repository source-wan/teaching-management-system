using System.Reflection;
using EMS.App.ReqDto.CreateDataDto.Base;
using EMS.App.ReqDto.UpdateDataDto.Base;
using EMS.Domain.Base;

namespace EMS.Api.Service;
public class CRUDServer<TSource, TCreateDataDto, TUpdateDataDto> : ICRUDServer<TSource, TCreateDataDto, TUpdateDataDto>
where TSource : BaseEntity
where TCreateDataDto : CreateDataDto
where TUpdateDataDto : UpdateDataDto
{
    private readonly ICreateDataServer<TSource, TCreateDataDto> _createServer;
    private readonly IDeleteDataServer<TSource> _deleteServer;
    private readonly IUpdateDataServer<TSource, TUpdateDataDto> _updateServer;
    private readonly IGetDataServer<TSource> _getServer;
    private readonly IQueryHelper<TSource> _queryHelper;
    private readonly ResponseDto _response = new ResponseDto();
    public CRUDServer
    (
        ICreateDataServer<TSource, TCreateDataDto> createServer,
        IDeleteDataServer<TSource> deleteServer,
        IUpdateDataServer<TSource, TUpdateDataDto> updateServer,
        IGetDataServer<TSource> getServer,
        IQueryHelper<TSource> queryHelper
    )
    {
        _createServer = createServer;
        _deleteServer = deleteServer;
        _updateServer = updateServer;
        _getServer = getServer;
        _queryHelper = queryHelper;
    }
    public async Task<ResponseDto> CreateDataAsync(IEnumerable<TCreateDataDto> datas)
    {
        var entities = await _createServer.CreateDataAsync(datas);
        if (entities.Count() < datas.Count())
        {
            foreach (var data in datas)
            {
                var dataProps = data.GetType().GetProperties();
                foreach (var dataProp in dataProps)
                {
                    var value = dataProp.GetValue(data);
                    if (value == null || value.ToString().IsNullOrEmpty()) return _response.CreateDataFail(dataProp.Name, data);
                }
            }

            return new ResponseDto
            {
                Code = ResponseCode.Fail,
                Message = "通常不会到这里"
            };
        }
        return _response.CreateDataSuccess(entities);
    }

    public async Task<ResponseDto> CreateDataAsync(TCreateDataDto data)
    {
        var entity = await _createServer.CreateDataAsync(data);

        if (entity == null)
        {
            var dataProps = data.GetType().GetProperties();
            foreach (var dataProp in dataProps)
            {
                var value = dataProp.GetValue(data);
                if (value == null || value.ToString().IsNullOrEmpty()) return _response.CreateDataFail(dataProp.Name, data);
            }
            
            return new ResponseDto
            {
                Code = ResponseCode.Fail,
                Message = "通常不会到这里"
            };

        }

        return _response.CreateDataSuccess(data);
    }

    public async Task<ResponseDto> DeleteDataAsync(Guid id)
    {
        var entity = await _deleteServer.DeleteDataAsync(id);
        if (entity == null) return _response.DataIsEmpty(id);
        return _response.DeleteDataSuccess(entity);
    }

    public async Task<ResponseDto> DeleteDataAsync(IEnumerable<Guid> id)
    {
        var entities = await _deleteServer.DeleteDataAsync(id);
        if (entities.Count() < id.Count())
        {
            var entityId = entities.Select(entity => entity?.Id);
            var notExistsObjectId = id.Where(id => !entityId.Contains(id));
            return _response.DataIsEmpty(notExistsObjectId);
        }

        return _response.DeleteDataSuccess(entities);
    }

    public async Task<ResponseDto> GetData(Func<TSource, bool> checker)
    {
        var index = int.Parse(_queryHelper.GetRequestQueryParam("index") ?? "1");
        var size = int.Parse(_queryHelper.GetRequestQueryParam("size") ?? "5");

        int count = 0;
        var entities = await Task.Run(() => _getServer.GetDataList(checker, out count)
        .Skip((index - 1) * size).Take(size));
        return _response.ReturnDataInfoWithCount(entities, count);
    }

    public async Task<ResponseDto> GetDataById(Guid id)
    {
        var data = await _getServer.GetDataById(id);
        if (data == null) return _response.DataIsEmpty(id);
        return _response.ReturnDataInfo(data);
    }

    public async Task<ResponseDto> UpdateDataAsync(Guid id, TUpdateDataDto data)
    {
        var entity = await _getServer.GetDataById(id);
        if (entity == null) return _response.DataIsEmpty(id);
        var origin = await _updateServer.UpdateDataAsync(entity, data);
        if (origin == entity) return _response.TargetSameAsSouce(origin, entity);
        return _response.UpdateDataSuccess(origin, entity);
    }
}