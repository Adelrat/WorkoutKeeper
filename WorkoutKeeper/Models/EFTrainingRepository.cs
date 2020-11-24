using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutKeeper.Models
{
    public class EFTrainingRepository : ITrainingRepository
    {
        private ApplicationDbContext context;
        public EFTrainingRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Training> Trainings => context.Trainings;

        public IQueryable<Exercise> Exercises => context.Exercises;
        public IQueryable<Day> Days => context.Days;

        public void DeleteExercise(Exercise exercise, string dayName, string trainingName)
        {
            var DbEntryEx = context.Exercises.FirstOrDefault(ex => ex.Name == exercise.Name);
            if (DbEntryEx != null)
            {
                var AllDay = context.Days.Include(d => d.DayExercise).ThenInclude(c => c.Exercise).ToList();
                var DayEx = AllDay.FirstOrDefault(day => day.TDay == dayName && day.Training.Name == trainingName)
                    .DayExercise.FirstOrDefault(ex => ex.ExerciseId == DbEntryEx.Id);
                DbEntryEx.DayExercise.Remove(DayEx);

                var IsDeleteEx=DbEntryEx.DayExercise.FirstOrDefault(t=>t.DayId>0);
                if (IsDeleteEx == null)
                {
                    context.Remove(DbEntryEx);
                }
                context.SaveChanges();
                var IsDeleteDay = context.Days.Where(day => day.TDay == dayName)
                                                .FirstOrDefault(day=>day.Training.Name == trainingName);
                var IsDeleteDayEx = IsDeleteDay.DayExercise.FirstOrDefault(t => t.DayId > 0);
                if (IsDeleteDayEx == null)
                {
                    context.Days.Remove(IsDeleteDay);
                    
                }
                context.SaveChanges();
            }

        }
        //добавление/изменение упражнения
        public void SaveExercise(Exercise exercise,string dayName,string trainingName)
        {
            //Получаем день, чтобы позже проверить нужно ли добавлять dayex или мы изменяем существуещее
            List<Day> Alldays = context.Days.Include(t => t.DayExercise).ThenInclude(c => c.Exercise).ToList();
            object IsNewDayEx = null;

            var DbEntryEx = context.Exercises.FirstOrDefault(e => e.Name == exercise.Name);
            //Добавляем упражнение    
            if (DbEntryEx == null)
            {
                context.Exercises.Add(exercise);
            }
            //если упражнение уже существует(редактирование)
            else
            {
                DbEntryEx.Name = exercise.Name;
                DbEntryEx.Description = exercise.Description;
                DbEntryEx.ApproachesNum = exercise.ApproachesNum;
                IsNewDayEx=Alldays.FirstOrDefault(day => day.TDay == dayName).DayExercise.FirstOrDefault(e => e.ExerciseId == DbEntryEx.Id);
            }
            context.SaveChanges();
            //добавляем день
            if (dayName != null)
            {         
            var DbEntryDay = context.Days.FirstOrDefault(d => d.TDay == dayName && d.Training.Name == trainingName);
            Training tran = context.Trainings.FirstOrDefault(t => t.Name == trainingName);
            Day day ;
            if (DbEntryDay == null)
            {
                day = new Day()
                {
                    TDay = dayName,
                    Training = tran
                };
                context.Add(day);
            }
            context.SaveChanges();
                //Если dayEx уже существует, то заканчиваем добавление
                if (IsNewDayEx == null) { 

            var exId = context.Exercises.FirstOrDefault(e => e.Name == exercise.Name).Id;
            //привязка нового(отредактированного) упражнения к старому дню
            if (DbEntryDay != null)
            {
                //если упражнение новое
                if (DbEntryEx == null)
                {
                    exercise.DayExercise.Add(new DayExercise { DayId = DbEntryDay.Id, ExerciseId = exId });
                }
                //если упражнение старое
                else
                {
                    DbEntryEx.DayExercise.Add(new DayExercise { DayId = DbEntryDay.Id, ExerciseId = exId });
                }
            }
            //если новый день
            else
            {
                var nday = context.Days.FirstOrDefault(day => day.TDay == dayName && day.Training.Name == trainingName);
                //если упражнение новое
                if (DbEntryEx == null)
                {
                    exercise.DayExercise.Add(new DayExercise { DayId = nday.Id, ExerciseId = exId });
                }
                //если упражнение старое
                else
                {
                    DbEntryEx.DayExercise.Add(new DayExercise { DayId = nday.Id, ExerciseId = exId });
                }

                }
                }//конец (если деньупражнение уже есть)
            }//конец (если день указан)
            context.SaveChanges();
        }
    }
}
