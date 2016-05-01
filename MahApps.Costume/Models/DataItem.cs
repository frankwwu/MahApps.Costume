using System.ComponentModel;

namespace MahAppsDemo.Models
{
    public class DataItem
    {
        [DisplayName("Currency Code")]
        public string Currency { get; set; }

        [DisplayName("Country and Currency")]
        public string Description { get; set; }
    }
}
