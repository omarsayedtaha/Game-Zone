using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer
{
    public class Device:BaseEntity
    {
        public string Name { get; set; }

        public string Icon { get; set; }

        public ICollection<GameDevices> DeviceGames { get; set; } = new HashSet<GameDevices>();
    }
}
