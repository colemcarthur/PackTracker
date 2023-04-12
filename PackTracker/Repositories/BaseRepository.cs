using System;
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
        /// <param name="item">Generic Data Item</param>
        public void SaveItem(T item)
        {
            int result = 0;
            try
            {
                if (item.Id != 0)
                {
                    result =
                         connection.Update(item);
                    StatusMessage =
                         $"{result} row(s) updated";
                }
                else
                {
                    result = connection.Insert(item);
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
    }
}

