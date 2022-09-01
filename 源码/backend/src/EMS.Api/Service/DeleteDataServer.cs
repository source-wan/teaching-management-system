using System.Reflection;
using EMS.Domain.Base;

namespace EMS.Api.Service;
public class DeleteDataServer<T> : IDeleteDataServer<T> where T : BaseEntity
{
    private readonly IRepository<T> _table;

    public DeleteDataServer(IRepository<T> table)
    {
        _table = table;
    }

    public async Task<T> DeleteDataAsync(T entity) =>
        await _table.DeleteAsync(entity) ?? entity;

    public async Task<IEnumerable<T>> DeleteDataAsync(IEnumerable<T> entities) =>
        entities.Any() ? await _table.DeleteAsync(entities) : entities;

    public async Task<IEnumerable<T?>> DeleteDataAsync(IEnumerable<Guid> id)
    {
        var entities = await _table.GetByIdList(id);
        if (entities.Count() != id.Count()) return entities;
        return await this.DeleteDataAsync(entities);
    }

    public async Task<T?> DeleteDataAsync(Guid id)
    {
        var entity = await _table.GetById(id);
        if (entity == null) return null;
        return await this.DeleteDataAsync(entity);
    }
}
