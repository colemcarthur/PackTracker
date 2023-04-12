using System;
using System.Linq.Expressions;

namespace PackTracker.Abstractions
{
    public interface IBaseRepository<T> : IDisposable
         where T : TableData, new()
    {
        void SaveItem(T item);
        //void SaveItemWithChildren(T item, bool recursive = false);
        //T GetItem(int id);
        //T GetItem(Expression<Func<T, bool>> predicate);
        //List<T> GetItems();
        //List<T> GetItems(Expression<Func<T, bool>> predicate);
        //List<T> GetItemsWithChildren();
        //void DeleteItem(T item);
    }
}

