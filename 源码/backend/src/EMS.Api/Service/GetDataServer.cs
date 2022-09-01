using EMS.Domain.Base;

namespace EMS.Api.Service;
public class GetDataServer<T> : IGetDataServer<T> where T : BaseEntity
{
    private readonly IRepository<T> _table;

    public GetDataServer(IRepository<T> table)
    {
        _table = table;
    }
    
    public async Task<T?> GetDataById(Guid id) => await _table.GetById(id);

    public IEnumerable<T> GetDataList(Func<T, bool> checker, out int count)
    {
        var data = _table.Table.Where(checker);
        count = data.Count();
        return data;
    }
}
