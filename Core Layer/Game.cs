using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer
{
    public class Game:BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Poster { get; set; }

        public int? CategoryId { get; set; }

        public Category? Category { get; set; }

        public ICollection<GameDevices>? Devices { get; set; } = new List<GameDevices>();

        
    }
}
