using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using MahAppsDemo.Models;

namespace MahAppsDemo.Services
{
    [Export(typeof(IDataService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class DataService : IDataService
    {
        public DataService()
        {
        }

        public List<DataItem> DataItems { get; private set; }

        public List<SpecialFolder> SpecialFolders { get; private set; }

        public void LoadData()
        {
            DataItems = new List<DataItem>();
            try
            {
                var reader = new StreamReader(File.OpenRead(@"Currencies.csv"));
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');
                    DataItem item = new DataItem() { Currency = values[0], Description = values[1] };
                    DataItems.Add(item);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            SpecialFolders = new List<SpecialFolder>();
            foreach (var name in Enum.GetValues(typeof(Environment.SpecialFolder)))
            {
                string p = Environment.GetFolderPath((Environment.SpecialFolder)name);
                SpecialFolder f = new SpecialFolder() { Name = name.ToString() , Folder = p};
                SpecialFolders.Add(f);
            }            
        }
    }
}
