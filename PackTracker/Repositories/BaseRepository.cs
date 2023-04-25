using System;
using System.Linq.Expressions;
using PackTracker.Abstractions;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace PackTracker.Repositories
{
    /// <summary>
    /// This class is inherited by table data classes for base database
    /// functionality
    /// </summary>
    /// <typeparam name="T">Generic Data Table Class</typeparam>
    public class BaseRepository<T> :
          IBaseRepository<T> where T : TableData, new()
    {

        // Private Properties
        SQLiteConnection connection;

        // Public Properties
        public string StatusMessage { get; set; }

        /// <summary>
        /// Initialize the repository
        /// </summary>
        public BaseRepository()
		{
            // Create a connection object
            connection = new SQLiteConnection(Constants.DatabasePath,
                                              Constants.Flags);

            // Create the database. If it doesn't exist it will create it
            // for the first time
            connection.CreateTable<T>();
        }

        /// <summary>
        /// Method to close the database
        /// </summary>
        public void Dispose()
        {
            // Close the database
            connection.Close();
        }

        /// <summary>
        /// Save the data item
        /// </summary>
        /// <param name="record">Generic Data Item</param>
        public void Save(T record)
        {
            int result = 0;
            try
            {
                if (record.Id != 0)
                {
                    result = connection.Update(record);
                    StatusMessage =
                         $"{result} row(s) updated";
                }
                else
                {
                    result = connection.Insert(record);
                    StatusMessage =
                         $"{result} row(s) added";
                }

            }
            catch (Exception ex)
            {
                StatusMessage =
                     $"Error: {ex.Message}";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="record"></param>
        /// <param name="recursive"></param>
        public void SaveWithChildren(T record, bool recursive = false)
        {

            try
            {
                if (record.Id != 0)
                {
                    connection.UpdateWithChildren(record);
                }
                else
                {
                    connection.InsertWithChildren(record, recursive);
                }

            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }

        }

        /// <summary>
        /// Delete Item
        /// </summary>
        /// <param name="item">Generic Data Item</param>
        public void Delete(T record)
        {
            try
            {
                //connection.Delete(item);
                connection.Delete(record, true);
            }
            catch (Exception ex)
            {
                StatusMessage =
                     $"Error: {ex.Message}";
            }
        }

        public List<T> GetItems()
        {
            try
            {
                return connection.Table<T>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = $"{ex.Message}";
            }
            return null;
        }

        public List<T> GetItemsWithChildren()
        {
            try
            {
                
                return connection.GetAllWithChildren<T>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = $"{ex.Message}";
            }
            return null;
        }

        public T GetItemsWithChildren(int Id)
        {
            try
            {
                return connection.GetAllWithChildren<T>()
                            .FirstOrDefault(x => x.Id == Id);
            }
            catch (Exception ex)
            {
                StatusMessage = $"{ex.Message}";
            }

            return null;
        }

        public T GetItem(int id)
        {
            try
            {
                return connection.Table<T>()
                    .FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                StatusMessage = $"{ex.Message}";
            }

            return null;
        }

        public List<T> GetItems(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return connection.Table<T>()
                    .Where(predicate).ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = $"{ex.Message}";
            }
            return null;
        }

        public T GetItem(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return connection.Table<T>()
                    .Where(predicate).FirstOrDefault();
            }
            catch (Exception ex)
            {
                StatusMessage = $"{ex.Message}";
            }
            return null;
        }

        public Double TotalValue()
        {
            var query = @"Select SUM(PurchasePrice) as TotalValue
                          FROM Item";

            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = query;

            return command.ExecuteScalar<Double>();

        }
    }
}