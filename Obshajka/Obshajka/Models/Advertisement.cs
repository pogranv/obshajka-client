using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obshajka.Models
{
    public class Advertisement
    {
        public long Id { get; set; }

        public long CreatorId { get; set; }

        public string CreatorName { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public int DormitoryId { get; set; }

        public int? Price { get; set; }

        public string? Image { get; set; }

        public DateOnly DateOfAddition { get; set; }
    }
}
