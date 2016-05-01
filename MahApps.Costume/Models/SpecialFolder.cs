using System.ComponentModel;

namespace MahAppsDemo.Models
{
    public class SpecialFolder
    {
        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Folder")]
        public string Folder { get; set; }
    }
}
