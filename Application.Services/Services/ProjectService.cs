using Domain.Core.Entities;
using Domain.Core.Entities.Backlog;
using Domain.Core.Entities.Sprint;
using Domain.Services.Patterns.Factory.Factory_Interfaces;
using Domain.Services.Patterns.Observer;
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
        private IProjectRepository _repository;
        private ISprintService _sprintService;
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
            }
            else
            {
                Console.WriteLine("User is already part of this project");
            }
        }

        //FR - 02
        public void AddItem(Item item, Project project)
        { 
            if (!project.Items.Contains(item))
            {
                project.Items.Add(item);
            }
            else
            {
                Console.WriteLine("Project can't have duplicate items");
            }
        }

        //FR - 03
        public void AddSprint(ISprintFactory sprintFactory, Project project, string goal, ISubject subject)
        {
            project.Sprints.Add(_sprintService.NewSprint(sprintFactory, project, goal, subject));
        }

    }
}
