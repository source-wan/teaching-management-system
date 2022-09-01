using EMS.App.Common.Interface;
using Microsoft.EntityFrameworkCore;
using EMS.Domain.Base;

namespace EMS.Infrastructure.Persistence.Repository;
public class EFRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly EMSDbContext _db;
    private readonly DbSet<T> _table;
    private readonly IUserSession _user;

    public EFRepository(EMSDbContext db, IUserSession user)
    {
        _db = db;
        _table = db.Set<T>();
        _user = user;
    }
    public IQueryable<T> Table => _table.AsNoTracking();

    public async Task<T> AddAsync(T entity)
    {
        // if (_user.Id == null && (entities.Any(entity => entity.CreatedBy != null || entity.UpdatedBy != null)))
        // {
        //     throw new Exception("Invalid operation, user is not login");
        // }

        entity.Id = Guid.NewGuid();
        entity.CreatedBy = _user.Id != null ? (Guid)_user.Id : new Guid();
        entity.UpdatedBy = _user.Id;
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;
        entity.IsActived = true;
        entity.IsDeleted = false;

        await _table.AddAsync(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<T>> AddAsync(IEnumerable<T> entities)
    {
        // if (_user.Id == null && (entities.Any(entity => entity.CreatedBy != null || entity.UpdatedBy != null)))
        // {
        //     throw new Exception("Invalid operation, user is not login");
        // }

        foreach (var entity in entities)
        {
            entity.Id = Guid.NewGuid();
            entity.CreatedBy = _user.Id != null ? (Guid)_user.Id : new Guid();
            entity.UpdatedBy = _user.Id;
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.IsActived = true;
            entity.IsDeleted = false;
        }

        await _table.AddRangeAsync(entities);
        await _db.SaveChangesAsync();
        return entities;

    }

    public async Task<T?> DeleteAsync(T entity, bool shoudHardDel = false)
    {
        if (shoudHardDel == true)
        {
            _table.Remove(entity);
            await _db.SaveChangesAsync();
        }
        else
        {
            entity.UpdatedBy = _user.Id;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.IsDeleted = true;
            await UpdateAsync(entity);
        }

        return entity;
    }

    public async Task<IEnumerable<T>> DeleteAsync(IEnumerable<T> entities, bool shoudHardDel = false)
    {
        if (shoudHardDel == true)
        {
            _table.RemoveRange(entities);
            await _db.SaveChangesAsync();
        }
        else
        {
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
            }

            await UpdateAsync(entities);
        }

        return entities;
    }

    public async Task<T?> DeleteAsync(Guid id, bool shoudHardDel = false)
    {
        var entity = await this.GetById(id);
        if (entity == null) return null;
        return await this.DeleteAsync(entity, shoudHardDel);
    }
    public async Task<T?> GetById(Guid id) =>
        await _table.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false && x.IsActived == true);

    public async Task<IEnumerable<T>> GetByIdList(IEnumerable<Guid> idList) =>
        await Task.Run(() => GetById(idList));

    private IEnumerable<T> GetById(IEnumerable<Guid> idList) =>
        _table.Where(data => idList.Contains(data.Id)).ToList();

    public IEnumerable<T> GetByPageIndex(int pageIndex = 1, int pageSize = 5)
    {
        return GetByPageIndex(_table, pageIndex, pageSize);
    }
    public IEnumerable<T> GetByPageIndex(IQueryable<T> dataSource, int pageIndex = 1, int pageSize = 5)
    {
        return dataSource
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToList<T>();
    }
    public async Task<T> UpdateAsync(T entity)
    {
        entity.UpdatedBy = _user.Id;
        entity.UpdatedAt = DateTime.UtcNow;

        _table.Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<T>> UpdateAsync(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            entity.UpdatedBy = _user.Id;
            entity.UpdatedAt = DateTime.UtcNow;
        }

        _table.UpdateRange(entities);
        await _db.SaveChangesAsync();
        return entities;
    }
}
