using System;
using SQLite;

namespace PackTracker
{
    public static class Constants
    {
        private const string DBFileName = "PackTracker.db3";

        public const SQLiteOpenFlags Flags = SQLiteOpenFlags.ReadWrite |
                                             SQLiteOpenFlags.Create |
                                             SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                return Path.Combine(FileSystem.AppDataDirectory, DBFileName);
            }
        }
    }
}

