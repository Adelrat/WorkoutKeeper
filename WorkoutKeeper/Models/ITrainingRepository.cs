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
            
            void DeleteExercise(Exercise exercise, int dayName, string trainingName);
            void SaveExercise(Exercise exercise, int dayName, string trainingName);
        void AddTraining(string TrainingName, string level);
        void DeleteTraining(string TrainingName);
    }
}
