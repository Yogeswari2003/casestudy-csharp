using casestudy_oops.entity;

namespace casestudy_oops.dao
{
    public interface IProjectRepository
    {
        
        bool CreateEmployee(Employee emp);
        bool CreateProject(Project pj);
        bool CreateProjectTask(ProjectTask task);

        bool AssignProjectToEmployee(int projectId, int employeeId);
        bool AssignTaskInProjectToEmployee(int taskId, int projectId, int employeeId);

        bool DeleteEmployee(int userId);
        bool DeleteProject(int projectId);
        List<ProjectTask> GetAllTasks(int empId, int projectId);


    }
}
