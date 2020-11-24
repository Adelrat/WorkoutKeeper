using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutKeeper.Models
{
    public class Training
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }

        public List<Day> Days { get; set; }

    }
}
