using Microsoft.AspNetCore.Mvc;
using TechJobs.Data;
using TechJobs.ViewModels;

namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

        // Our reference to the data store
        private static JobData jobData;

        static JobController()
        {
            jobData = JobData.GetInstance();
        }

        // The detail display for a given Job at URLs like /Job?id=17
        public IActionResult Index(int id)
        {
            // TODO #1 - get the Job with the given ID and pass it into the view
            Models.Job Found = jobData.Find(id);
            return View(Found);
        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
            // TODO #6 - Validate the ViewModel and if valid, create a 
            // new Job and add it to the JobData data store. Then
            // redirect to the Job detail (Index) action/view for the new Job.

            if (ModelState.IsValid)
            {
                Models.Job newJob = new Models.Job();
                newJob.Name = newJobViewModel.Name;
                newJob.Employer = jobData.Employers.Find(newJobViewModel.EmployerID);
                newJob.Location = jobData.Locations.Find(newJobViewModel.LocationID);
                newJob.CoreCompetency = jobData.CoreCompetencies.Find(newJobViewModel.CoreCompetencyID);
                newJob.PositionType = jobData.PositionTypes.Find(newJobViewModel.PositionTypeID);
                jobData.Jobs.Add(newJob);

                return Redirect("Index?id=" + newJob.ID);
            }

            return View(newJobViewModel);


        }
    }       
}
