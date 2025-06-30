//using casestudy_oops.main;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using casestudy_oops.dao;
using casestudy_oops.entity;
using casestudy_oops.myexceptions;
using System;

namespace casestudy_oops.main
{
    public class ProjectApp
    {
        public static void Main(string[] args)
        {
            IProjectRepository repo = new ProjectRepositoryImpl();

            while (true)
            {
                Console.WriteLine("\n--- Project Management System ---");
                Console.WriteLine("1. Add Employee");
                Console.WriteLine("2. Add Project");
                Console.WriteLine("3.Add Task");
                Console.WriteLine("4. Assign Project to Employee");
                Console.WriteLine("5. Assign Task in Project to Employee");
                Console.WriteLine("6. Delete Employee");
                Console.WriteLine("7. Delete Project");
                Console.WriteLine("8. View Tasks Assigned to an Employee in a Project");
                Console.WriteLine("9. Exit");
                Console.Write("Enter your choice: ");

                int choice;
                bool valid = int.TryParse(Console.ReadLine(), out choice);
                if (!valid)
                {
                    Console.WriteLine(" Invalid input. Enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        try
                        {
                            Console.Write("Enter Employee ID: ");
                            int empId = Convert.ToInt32(Console.ReadLine());

                            Console.Write("Enter Name: ");
                            string name = Console.ReadLine();

                            Console.Write("Enter Designation: ");
                            string designation = Console.ReadLine();

                            Console.Write("Enter Gender: ");
                            string gender = Console.ReadLine();

                            Console.Write("Enter Salary: ");
                            int salary = Convert.ToInt32(Console.ReadLine());

                            Console.Write("Enter Project ID: ");
                            int projectId = Convert.ToInt32(Console.ReadLine());

                            Employee emp = new Employee(empId, name, designation, gender, salary, projectId);
                            bool empResult = repo.CreateEmployee(emp);
                            Console.WriteLine(empResult ? "Employee added successfully." : "Failed to add employee.");
                        }
                        catch (ProjectNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Unexpected error: " + ex.Message);
                        }
                        break;

                    case 2:
                        Console.Write("Enter Project ID: ");
                        int pid = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Enter Project Name: ");
                        string pname = Console.ReadLine();

                        Console.Write("Enter Description: ");
                        string desc = Console.ReadLine();

                        Console.Write("Enter Start Date (yyyy-mm-dd): ");
                        DateTime startDate = DateTime.Parse(Console.ReadLine());

                        Console.Write("Enter Status: ");
                        string status = Console.ReadLine();

                        Project pj = new Project(pid, pname, desc, startDate, status);
                        bool projResult = repo.CreateProject(pj);

                        Console.WriteLine(projResult ? "Project added successfully." : "Failed to add project.");
                        break;

                    case 3:
                        try
                        {
                            Console.Write("Enter Task ID: ");
                            int taskid = Convert.ToInt32(Console.ReadLine());

                            Console.Write("Enter Task Name: ");
                            string taskname = Console.ReadLine();

                            Console.Write("Enter Project ID: ");
                            int projid = Convert.ToInt32(Console.ReadLine());

                            Console.Write("Enter Employee ID: ");
                            int empid = Convert.ToInt32(Console.ReadLine());

                            Console.Write("Enter Task Status: ");
                            string taskStatus = Console.ReadLine();

                            ProjectTask task = new ProjectTask(taskid, taskname, projid, empid, taskStatus);

                            bool taskResult = repo.CreateProjectTask(task);
                            Console.WriteLine(taskResult ? " Task added successfully." : "Failed to add task.");
                        }
                        catch (EmployeeNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (ProjectNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(" Unexpected error: " + ex.Message);
                        }
                        break;

                    case 4:
                        try
                        {
                            Console.Write("Enter Project ID: ");
                            int projId = Convert.ToInt32(Console.ReadLine());

                            Console.Write("Enter Employee ID: ");
                            int eid = Convert.ToInt32(Console.ReadLine());

                            bool assignProjectResult = repo.AssignProjectToEmployee(projId, eid);
                            Console.WriteLine(assignProjectResult ? " Project assigned to employee successfully." : " Failed to assign project.");
                        }
                        catch (ProjectNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (EmployeeNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Unexpected error: " + ex.Message);
                        }
                        break;

                    case 5:
                        try
                        {
                            Console.Write("Enter Task ID: ");
                            int taskId = Convert.ToInt32(Console.ReadLine());

                            Console.Write("Enter Project ID: ");
                            int projectid = Convert.ToInt32(Console.ReadLine());

                            Console.Write("Enter Employee ID: ");
                            int eId = Convert.ToInt32(Console.ReadLine());

                            bool assignTaskResult = repo.AssignTaskInProjectToEmployee(taskId, projectid, eId);
                            Console.WriteLine(assignTaskResult ? " Task assigned to employee successfully." : " Failed to assign task.");
                        }
                        catch (ProjectNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (EmployeeNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Unexpected error: " + ex.Message);
                        }
                        break;

                    case 6:
                        try
                        {
                            Console.Write("Enter Employee ID to delete: ");
                            int empIdToDelete = Convert.ToInt32(Console.ReadLine());

                            bool empDeleted = repo.DeleteEmployee(empIdToDelete);
                            Console.WriteLine(empDeleted ? "Employee deleted successfully." : " Failed to delete employee.");
                        }
                        catch (EmployeeNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Unexpected error: " + ex.Message);
                        }
                        break;

                    case 7:
                        try
                        {
                            Console.Write("Enter Project ID to delete: ");
                            int projIdToDelete = Convert.ToInt32(Console.ReadLine());

                            bool projDeleted = repo.DeleteProject(projIdToDelete);
                            Console.WriteLine(projDeleted ? "Project deleted successfully." : "Failed to delete project.");
                        }
                        catch (ProjectNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Unexpected error: " + ex.Message);
                        }
                        break;

                    case 8:
                        try
                        {
                            Console.Write("Enter Employee ID: ");
                            int emplid = Convert.ToInt32(Console.ReadLine());

                            Console.Write("Enter Project ID: ");
                            int proid = Convert.ToInt32(Console.ReadLine());

                            List<ProjectTask> taskList = repo.GetAllTasks(emplid, proid);

                            if (taskList.Count == 0)
                            {
                                Console.WriteLine("No tasks found for the given employee and project.");
                            }
                            else
                            {
                                Console.WriteLine("\n--- Assigned Tasks ---");
                                foreach (ProjectTask Task in taskList)
                                {
                                    Console.WriteLine($"Task ID: {Task.taskid}, Task Name: {Task.taskname}, Status: {Task.status}");
                                }
                            }
                        }
                        catch (ProjectNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (EmployeeNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Unexpected error: " + ex.Message);
                        }
                        break;

                    case 9:
                        Console.WriteLine(" Exiting... Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
