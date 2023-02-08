using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twits.Data.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public override string ToString()
        {
            return this.Title;
        }
    }
}
