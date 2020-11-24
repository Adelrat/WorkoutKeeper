using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutKeeper.Models
{
    public interface ITrainingRepository
    { 
            IQueryable<Training> Trainings { get; }
            IQueryable<Exercise> Exercises { get; }
            IQueryable<Day> Days { get; }
            
            void DeleteExercise(Exercise exercise, string dayName, string trainingName);
            void SaveExercise(Exercise exercise, string dayName, string trainingName);
    }
}
