using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.App.SectionOne.DIP
{
    public class SqlDatabase
    {
        public void SaveData(string data)
        {
            Console.WriteLine($"Data is saved to SQL Server: {data}");
        }
    }

    public class DataManager
    {
        private readonly SqlDatabase _sqlDatabase;

        public DataManager()
        {
            _sqlDatabase = new SqlDatabase();
        }

        public void Save(string data)
        {
            _sqlDatabase.SaveData(data);
        }
    }
}
