using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutKeeper.Models;



namespace WorkoutKeeper.Controllers
{
    public class AdminController : Controller
    {
        private readonly ITrainingRepository repository;
        //private List<Training> tran;
        List<ExerViewModel> ExersView = new List<ExerViewModel>();
        public AdminController(ITrainingRepository repos)
        {
            repository = repos;
            //tran = repository.Trainings.ToList();
        }
        public ViewResult IndexTrain()
        {
            List<string> Trainings = repository.Trainings.Select(t=>t.Name).ToList();
            return View(Trainings);
        }
        [HttpGet]
        public IActionResult AddTraining()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddTraining(string TrainingName,string level)
        {
            repository.AddTraining(TrainingName, level);
            return RedirectToAction("IndexTrain");
        }
        public IActionResult DeleteTraining(string TrainingName)
        {
            repository.DeleteTraining(TrainingName);
            return RedirectToAction("IndexTrain");
        }
            //TODO:принимать не имя, а ID
            public IActionResult IndexExercises(string TrainingName)
        {
            List<Day> Alldays = repository.Days.Include(t => t.DayExercise).ThenInclude(c => c.Exercise).ToList();
            var trainingId = repository.Trainings.FirstOrDefault(t => t.Name == TrainingName).Id;
            var days = Alldays.Where(t => t.TrainingId==trainingId).ToList();
            var result=days.OrderBy(day => day.TDay);
            foreach (var t in result)
            {
                var r = t.DayExercise.Select(t => t.Exercise).ToList();
                ExersView.Add(new ExerViewModel { Day = t.TDay, Exercises = r, TrainingName=TrainingName});
            }
            return View(ExersView);
        }

        public IActionResult Create() => RedirectToAction("AddEx", 0);

        [HttpGet]
        public ViewResult AddEx(int exId)
        {
            Exercise ex;
            if (exId == 0)
            {
                ex = new Exercise();
            }
            else
            {
                ex = repository.Exercises.FirstOrDefault(e => e.Id == exId);
            }
            return View(ex);
        }
        public IActionResult DeleteEx(int exId, int day, string trainingName)
        {
            var exercise= repository.Exercises.FirstOrDefault(e => e.Id == exId);
            repository.DeleteExercise(exercise,  day, trainingName);
            return RedirectToAction("IndexExercises", new { TrainingName = trainingName });
        }
        [HttpPost]
        public IActionResult AddEx(Exercise exercise, int dayName, string trainingName)
        {
            repository.SaveExercise(exercise,dayName,trainingName);
            return RedirectToAction("IndexExercises",new { TrainingName = trainingName });
        }
    }
}
