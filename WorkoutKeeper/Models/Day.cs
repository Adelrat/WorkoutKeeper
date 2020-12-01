using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutKeeper.Models
{
    public class Day
    {
        public int Id { get; set; }
        public int TDay { get; set; }
        public int TrainingId { get; set; }

        public Training Training { get; set; }
        public List<DayExercise> DayExercise { get; set; }
        
        public Day()
        {
            DayExercise = new List<DayExercise>();
        }
    }
}
