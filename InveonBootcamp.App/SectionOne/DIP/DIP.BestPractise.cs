using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.App.SectionOne.DIP
{
    public interface IDatabase
    {
        void SaveData(string data);
    }

    public class SqlDatabaseX : IDatabase
    {
        public void SaveData(string data)
        {
            Console.WriteLine($"Data saved to SQL Server: {data}");
        }
    }

    public class MongoDBDatabase : IDatabase
    {
        public void SaveData(string data)
        {
            Console.WriteLine($"Data saved to MongoDB: {data}");
        }
    }

    public class DataManagerX
    {
        private readonly IDatabase _database;

        public DataManagerX(IDatabase database)
        {
            _database = database;
        }

        public void Save(string data)
        {
            _database.SaveData(data);
        }
    }
}
