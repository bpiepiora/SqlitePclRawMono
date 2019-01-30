using System;
using System.IO;
using System.Net;
using SQLite;

namespace SqlitePclRawMono
{
    internal class Program
    {
        private const string DatabaseFile = "sqlite.db";
        
        public static void Main(string[] args)
        {
            // Make sure, that SQLitePCL.raw has to use the libraries in the "Sqlite/<arch>" folder.
            File.Delete("libe_sqlite3.so");
            
            // The library to use, is resolved by the DllMaps in "SQLitePCLRaw.provider.e_sqlite3.dll.config".
            // All files need to be added manually to the project (e.g. into the "Sqlite" folder"),
            // but it allows to deploy the project on different architectures. 
            // It would be nice and cleaner, when the targets of SQLitePCL.raw would not include/link
            // the "libe_sqlite3.so" file, when the "SQLitePCLRaw.provider.e_sqlite3.dll.config" file exists.
            
            // In a separate project, I programmed a library, that adds dependent libraries for all
            // architectures to the project folder incl DllMaps and the user can decide which archs
            // he wants to use, by just changing the "Copy to Output Folder" option.
            // This works great for Mono, but was not tested with .NET on Windows, since I don't know
            // if .NET supports the DllMap configuration.
            
            using (var database = new SQLiteConnection(DatabaseFile))
            {
                var list = database.GetTableInfo("sqlite_master");

                foreach (var columnInfo in list)
                {
                    Console.WriteLine(columnInfo.Name);
                }
            }
        }
    }
}