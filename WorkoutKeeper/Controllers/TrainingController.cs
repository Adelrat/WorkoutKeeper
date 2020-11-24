using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using WorkoutKeeper.Models;

namespace WorkoutKeeper.Controllers
{
    public class TrainingController : Controller
    {
        private ITrainingRepository repository;
        List<Training> tran;
        List<ExerViewModel> ExersView = new List<ExerViewModel>();
        public TrainingController(ITrainingRepository repos)
        {
            repository = repos;
            tran = repository.Trainings.ToList();
        }
        public IActionResult List()
        {
            return View(tran);
        }
        [HttpPost]
        public IActionResult Exercises(int trainingId)
        {
            List<Day> Alldays = repository.Days.Include(t => t.DayExercise).ThenInclude(c => c.Exercise).ToList();
            var days = Alldays.Where(t => t.TrainingId == trainingId).ToList();
            string traiName = repository.Trainings.FirstOrDefault(tr => tr.Id == trainingId).Name;
            foreach (var t in days)
            {
                var r = t.DayExercise.Select(t => t.Exercise).ToList();
                ExersView.Add(new ExerViewModel { Day = t.TDay, Exercises = r, TrainingName=traiName });
            }
            return View(ExersView);

        }
        
    }
}
