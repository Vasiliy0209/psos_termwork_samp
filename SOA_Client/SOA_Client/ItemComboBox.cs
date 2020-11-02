using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOA_Client
{
    class ItemComboBox
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public ItemComboBox(int Id, string Name )
        {
            this.Name = Name;
            this.Id = Id;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
