using Domain.Core.Entities;
using Domain.Core.Entities.Backlog;
using Domain.Core.Entities.Sprint;
using Domain.Services.Patterns.Observer;
using Domain.Services.Patterns.State.Sprint;
using Domain.Services.Repositories;
using Domain.Services.Services;
using Domain.Services.Patterns.Factory;
using Domain.Services.Patterns.Factory.Factory_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.Services.Services
{
    public class SprintService : ISprintService
    {

        private ISprintRepository _repository;
        private IDevPipeService _devPipeService;
        private IReportService _reportService;
        public SprintService(ISprintRepository sprintRepository, IDevPipeService devPipeService, IReportService reportService) {
            _repository = sprintRepository;
            _devPipeService = devPipeService;
            _reportService = reportService;
        }
        public ISprint NewSprint(ISprintFactory sprintFactory, Project project, string goal, ISubject subject)
        {
            var y = new InDevelopment();
            var x = sprintFactory.CreateSprint(goal, y, subject, project);
            _repository.Create(x);
            return x;



            //switch (type)
            //{
            //    case "release":
            //        var y = new InDevelopment();
            //        var x = new ReleaseSprint("Software Release",y,new Subject(),project);
            //        _repository.Create(x);
            //        return x;
            //    case "partial":
            //        var z = new InDevelopment();
            //        var q = new PartialSprint("Partial product for Feedback",z,new Subject(), project);
            //        _repository.Create(q);
            //        return q; 
            //    default:
            //        throw new Exception("Sprint Type not found");
            //} 
        }

        // Implements Observer Pattern
        public void Observe(IObserver observer, ISprint sprint)
        {
            sprint.Subject.Subscribe("Test Failed", observer);
        }

        public void TestFailure(ISprint sprint)
        {
            sprint.Subject.Notify("Tests Failed", "The Tests have failed");
        }

        public void AddDeveloper(User user, ISprint sprint)
        {
            if (sprint.State.GetState() is not InDevelopment)
            {
                Console.WriteLine("Sprint is finished and cannot be edited");
            }
            else
            {
                if (!sprint.Users.Contains(user))
                {
                    sprint.Users.Add(user);
                }
                else
                {
                    Console.WriteLine("User already part of this sprint");
                }
            }
        }

        public void AddItems(ISprint sprint, Item item)
        {
            if (sprint.State.GetState() is not InDevelopment)
            {
                Console.WriteLine("Sprint is finished and cannot be edited");
            }
            else
            {
                if (!sprint.Backlogs.Contains(item))
                {
                    sprint.Backlogs.Add(item);
                }
                else
                {
                    Console.WriteLine("Sprint can't have duplicate items");
                }
            }
        }

        public void AddScrummaster(User user, ISprint sprint)
        {
            if (sprint.State.GetState() is not InDevelopment)
            {
                Console.WriteLine("Sprint is finished and cannot be edited");
            }
            else
            {
                if (sprint.Scrummaster != null)
                {
                    Console.WriteLine("Sprint already has a Scrummaster");
                }
                else
                {
                    sprint.Scrummaster = user;
                }
            }

        }

        // Implements State Pattern

        public void NextState(ISprint sprint) {
            sprint.State.NextState();
        }

        // FR - 07

        public void FinishSprint(ISprint sprint)
        {
            if (sprint.State.GetState() is InDevelopment)
            {
                this.NextState(sprint);
            } 
            else
            {
                Console.WriteLine("State is already finished");
            }
        }

        public void AddReview(Review review, PartialSprint sprint)
        {
            if (sprint.State.GetState() is not Finished)
            {
                Console.WriteLine("Sprint is not Finished");
            }
            else
            {
                if (sprint.Review != review)
                {
                    sprint.Review = review;
                    this.NextState(sprint);
                }
                else
                {
                    Console.WriteLine("Already added this Report");
                }
            }
        }

        //FR - 08
        public void AddDevPipe(ISprint sprint)
        {
           if (sprint.State.GetState() is Finished && sprint.Scrummaster != null)
           {
                if (sprint.DevPipe != null)
                {
                    Console.WriteLine("Sprint already has a Development Pipeline");
                }     
                else
                {
                    var devPipe = _devPipeService.NewDevPipe(sprint);
                    sprint.DevPipe = devPipe;
                    devPipe.Subject.Subscribe("Scrummaster", new EmailObserver(sprint.Scrummaster));
                    devPipe.Subject.Subscribe("ProductOwner", new EmailObserver(sprint.Project.ProductOwner));
                }
           }
           else
           {
                Console.WriteLine("Sprint is not yet finished");
           }
        }

        //FR - 09
        public void GenerateReport(ISprint sprint)
        {
            if (sprint.Report == null)
            {
                sprint.Report = _reportService.NewReport(sprint);
            }
            else
            {
                Console.WriteLine("Sprint has already been reported upon");
            }

        }

    }
}
