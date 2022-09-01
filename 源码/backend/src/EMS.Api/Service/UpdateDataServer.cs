using EMS.Domain.Base;

namespace EMS.Api.Service;
public class UpdateDataServer<T> : IUpdateDataServer<T> where T : BaseEntity
{
    private readonly IRepository<T> _table;
    public UpdateDataServer(IRepository<T> table)
    {
        _table = table;
    }
    public async Task<IEnumerable<T>> UpdateDataAsync(IEnumerable<T> source, IEnumerable<T> targets)
    {
        if (!targets.Any()) return source;

        bool targetIsNullOrRepeat = true;
        var originDataList = new List<T>();

        for (int i = 0; i < targets.Count(); i++)
        {
            var originData = Activator.CreateInstance<T>();
            var target = targets.ElementAt(i);
            foreach (var prop in target.GetType().GetProperties())
            {
                var value = prop.GetValue(target);
                var sourceProp = source.ElementAt(i).GetType().GetProperty(prop.Name);
                var originValue = sourceProp?.GetValue(source.ElementAt(i));
                originData.GetType().GetProperty(prop.Name)?.SetValue(originData, originValue);
                if (prop.Name == "Id") continue;

                if (value == null || value.ToString().IsNullOrEmpty()) continue;

                if (sourceProp != null && (originValue == null || !originValue.Equals(value)))
                {
                    sourceProp.SetValue(source.ElementAt(i), value);
                    targetIsNullOrRepeat = false;
                }
            }

            originDataList.Add(originData);
        }

        if (targetIsNullOrRepeat || originDataList.HasNullOrEmpty(System.Reflection.BindingFlags.DeclaredOnly)) return source;

        var updated = await _table.UpdateAsync(source);
        return originDataList;
    }
    public async Task<T> UpdateDataAsync(T source, T target)
    {
        bool targetIsNullOrRepeat = true;
        var originDataList = Activator.CreateInstance<T>();
        if (target.ToString().IsNullOrEmpty()) return source;

        foreach (var prop in target.GetType().GetProperties())
        {
            var value = prop.GetValue(target);
            var sourceProp = source.GetType().GetProperty(prop.Name);
            var originValue = sourceProp == null ? null : sourceProp.GetValue(source);
            originDataList.GetType().GetProperty(prop.Name)?.SetValue(originDataList, originValue);
            if (value == null || value.ToString().IsNullOrEmpty()) continue;

            if (sourceProp != null && (originValue == null || !originValue.Equals(value)))
            {
                sourceProp.SetValue(source, value);
                targetIsNullOrRepeat = false;
            }
        }

        if (targetIsNullOrRepeat) return source;

        var updated = await _table.UpdateAsync(source);
        return originDataList;
    }
}
public class UpdateDataServer<TSource, TTarget> : IUpdateDataServer<TSource, TTarget>
where TSource : BaseEntity
where TTarget : App.ReqDto.RequestDto
{
    private readonly IRepository<TSource> _table;
    public UpdateDataServer(IRepository<TSource> table)
    {
        _table = table;
    }
    public async Task<TSource> UpdateDataAsync(TSource source, TTarget target)
    {
        bool targetIsNullOrRepeat = true;
        var originDataList = Activator.CreateInstance<TSource>();
        if (target.ToString().IsNullOrEmpty()) return source;

        foreach (var prop in target.GetType().GetProperties())
        {
            var value = prop.GetValue(target);
            var sourceProp = source.GetType().GetProperty(prop.Name);
            var originValue = sourceProp == null ? null : sourceProp.GetValue(source);
            originDataList.GetType().GetProperty(prop.Name)?.SetValue(originDataList, originValue);
            if (value == null || value.ToString().IsNullOrEmpty()) continue;

            if (sourceProp != null && (originValue == null || !originValue.Equals(value)))
            {
                sourceProp.SetValue(source, value);
                targetIsNullOrRepeat = false;
            }
        }

        if (targetIsNullOrRepeat) return source;

        var updated = await _table.UpdateAsync(source);
        return originDataList;
    }
}
