using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twits.Data.Models.ViewModels
{
    public class NotesVM
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

    }
}
