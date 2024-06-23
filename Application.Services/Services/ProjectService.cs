using Domain.Core.Entities;
using Domain.Core.Entities.Backlog;
using Domain.Core.Entities.Sprint;
using Domain.Services.Patterns.Factory.Factory_Interfaces;
using Domain.Services.Patterns.Observer;
using Domain.Services.Patterns.State.ItemStates;
using Domain.Services.Patterns.State.Sprint;
using Domain.Services.Repositories;
using Domain.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Services
{
    public class ProjectService : IProjectService
    {
        readonly private IProjectRepository _repository;
        readonly private ISprintService _sprintService;
        public ProjectService(IProjectRepository repository, ISprintService sprintService) {
            _repository = repository;
            _sprintService = sprintService;
        }

        // FR - 01
        public Project CreateProject(string name, User user)
        {
            var x = new Project(name, user);
            _repository.Create(x);
            return x;
        }

        public void AddUser(User user, Project project)
        {
            if (!project.Developers.Contains(user))
            {
                project.Developers.Add(user);
                project.Subscribe(user.PreferedNotificationType, user);
            }
            else
            {
                Console.WriteLine("User is already part of this project");
            }
        }

        public void AssignScrumMaster(User user, Project project)
        {
            if (project.ScrumMaster != null)
            {
                AddUser(project.ScrumMaster, project);
                project.ScrumMaster = user;
            } else
            {
                project.ScrumMaster = user;
                project.Subscribe(user.PreferedNotificationType, user);
            }
        }

        //FR - 02
        public void AddItem(Item item, Project project)
        { 
            if (!project.Items.Contains(item) && item.project == null)
            {
                project.Items.Add(item);
                item.project = project;
            }
            else
            {
                Console.WriteLine("Project can't have duplicate items or a item can't exist in multiple projects");
            }
        }

        //FR - 03
        public void AddSprint(ISprintFactory sprintFactory, Project project, string goal, ISubject subject)
        {
            project.Sprints.Add(_sprintService.NewSprint(sprintFactory, project, goal, subject));
        }

        //FR - 05
        public void OnItemStateChanged(Item item)
        {
            if (item.State.GetState() is ReadyForTesting && item.project != null)
            {
                item.project.Notify("Email", $"Item '{item.Name}' is ready for testing");
                item.project.Notify("App", $"Item '{item.Name}' is ready for testing");
            }
        }

        public void TestFailedNotification(Item item)
        {
            if (item.project != null && item.project.ScrumMaster != null)
            {
                item.project.ScrumMaster.Update($"Item '{item.Name}' has failed in testing");
                
            }
        }

    }
}
