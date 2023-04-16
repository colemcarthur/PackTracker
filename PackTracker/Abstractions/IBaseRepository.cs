using System;
using System.Linq.Expressions;

namespace PackTracker.Abstractions
{
    public interface IBaseRepository<T> : IDisposable
         where T : TableData, new()
    {
        void Save(T record);
        void SaveWithChildren(T recoord, bool recursive = false);
        //T GetItem(int id);
        //T GetItem(Expression<Func<T, bool>> predicate);
        List<T> GetItems();
        List<T> GetItemsWithChildren();
        //List<T> GetItems(Expression<Func<T, bool>> predicate);
        void Delete(T record);
    }
}

