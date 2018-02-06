namespace WebServer.ByTheCakeApplication.Data
{
    using ViewModels;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class CakesData
    {
        private const string DatabaseFile = @"ByTheCakeApplication\Data\database.csv";

        public IEnumerable<Cake> All()
        {
            const char delimiter = ',';

            return File.ReadAllLines(DatabaseFile)
                .Where(l => l.Contains(delimiter))
                .Select(l => l.Split(delimiter))
                .Select(l => new Cake
                {
                    Id = int.Parse(l[0]),
                    Name = l[1],
                    Price = decimal.Parse(l[2])
                });
        }

        public Cake Find(int id)
        {
            return this.All().FirstOrDefault(c => c.Id == id);
        }

        public void Add(string name, string price)
        {
            var streamReader = new StreamReader(DatabaseFile);

            var id = streamReader
                .ReadToEnd()
                .Split(Environment.NewLine)
                .Length;

            streamReader.Dispose();

            using (var streamWriter = new StreamWriter(DatabaseFile, true))
            {
                streamWriter.WriteLine($"{id},{name},{price}");
            }
        }
    }
}
