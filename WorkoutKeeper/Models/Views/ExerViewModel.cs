using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutKeeper.Models
{
    public class ExerViewModel
    {
        public string TrainingName { get; set; }
        public int Day { get; set; }
        public List<Exercise> Exercises { get; set; }
    }
}
