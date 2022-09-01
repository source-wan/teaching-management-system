using System.Reflection;
using EMS.App.ReqDto.CreateDataDto.Base;
using EMS.Domain.Base;

namespace EMS.Api.Service;

public class CreateDataServer<T> : ICreateDataServer<T>
where T : BaseEntity
{
    private readonly IRepository<T> _table;
    public CreateDataServer(IRepository<T> table)
    {
        _table = table;
    }
    public async Task<T?> CreateDataAsync(T entity)
    {
        if (entity == null) return null;
        var targetData = Activator.CreateInstance<T>();
        var targetDataProps = targetData.GetType().GetProperties();
        targetDataProps.FirstOrDefault(x => x.Name.EndsWith("Code"))?.SetValue(targetData, (Int16)(_table.Table.Count() + 1));
        foreach (var entityProp in entity.GetType().GetProperties())
        {
            var value = entityProp.GetValue(entity);
            if (value == null
            || value.ToString().IsNullOrEmpty()
            || !targetDataProps.Any(prop => prop.Name.Equals(entityProp.Name))) continue;

            targetDataProps.FirstOrDefault(prop => prop.Name.Equals(entityProp.Name))
            ?.SetValue(targetData, value);
        }

        if (targetData.HasNullOrEmpty(BindingFlags.DeclaredOnly)) return null;
        var data = await _table.AddAsync(targetData);
        return data;
    }

    public async Task<IEnumerable<T?>> CreateDataAsync(IEnumerable<T> entities)
    {
        return await _table.AddAsync(entities);
    }
}
public class CreateDataServer<TSource, TTarget> : ICreateDataServer<TSource, TTarget>
where TSource : BaseEntity
where TTarget : CreateDataDto
{
    private readonly IRepository<TSource> _table;

    public CreateDataServer(IRepository<TSource> table)
    {
        _table = table;
    }
    public async Task<TSource?> CreateDataAsync(TTarget entity)
    {
        if (entity == null) return null;
        var targetData = Activator.CreateInstance<TSource>();
        var targetDataProps = targetData.GetType().GetProperties();
        targetDataProps.FirstOrDefault(x => x.Name.EndsWith("Code"))?.SetValue(targetData, (Int16)(_table.Table.Count() + 1));
        foreach (var entityProp in entity.GetType().GetProperties())
        {
            var value = entityProp.GetValue(entity);
            if (value == null
            || value.ToString().IsNullOrEmpty()
            || !targetDataProps.Any(prop => prop.Name.Equals(entityProp.Name))) continue;

            targetDataProps.FirstOrDefault(prop => prop.Name.Equals(entityProp.Name))
            ?.SetValue(targetData, value);
        }

        if (targetData.HasNullOrEmpty(BindingFlags.DeclaredOnly)) return null;
        var data = await _table.AddAsync(targetData);
        return data;
    }

    public async Task<IEnumerable<TSource?>> CreateDataAsync(IEnumerable<TTarget> entities)
    {
        var targetDataList = new List<TSource>();
        foreach (var entity in entities)
        {
            var data = Activator.CreateInstance<TSource>();
            if (entity == null || entity.HasNullOrEmpty(BindingFlags.DeclaredOnly)) continue;

            foreach (var entityProp in entity.GetType().GetProperties())
            {
                var value = entityProp.GetValue(entity);
                if (value == null || value.ToString().IsNullOrEmpty()) continue;
                data.GetType().GetProperty(entityProp.Name)?.SetValue(data, value);
            }
            targetDataList.Add(data);
        }
        if (targetDataList.Count() < entities.Count()) return targetDataList;
        return await _table.AddAsync(targetDataList);
    }
}
