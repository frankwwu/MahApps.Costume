using System.Collections.Generic;
using MahAppsDemo.Models;

namespace MahAppsDemo.Services
{
    public interface IDataService
    {
        List<DataItem> DataItems { get; }
        List<SpecialFolder> SpecialFolders { get; }
        void LoadData();
    }
}
