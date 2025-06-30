//using Microsoft.Data.SqlClient;

using casestudy_oops.entity;
using casestudy_oops.myexceptions;
using casestudy_oops.Util; // Assuming DBConnection utility is here
using System;
using System.Data.SqlClient;



namespace casestudy_oops.dao
{
    public class ProjectRepositoryImpl : IProjectRepository
    {
        public bool CreateEmployee(Employee emp)
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();

                    string checkProjQuery = "SELECT COUNT(*) FROM project WHERE projectid = @pid";
                    SqlCommand checkCmd = new SqlCommand(checkProjQuery, conn);
                    checkCmd.Parameters.AddWithValue("@pid", emp.ProjectId);
                    int projCount = (int)checkCmd.ExecuteScalar();

                    if (projCount == 0)
                        throw new ProjectNotFoundException("exce: Project not found with the given ID.");

                    string query = @"INSERT INTO employee 
                            (empid, name, designation, gender, salary, projectid) 
                             VALUES (@empid, @name, @designation, @gender, @salary, @projectid)";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Match parameter names with SQL query and your entity
                    cmd.Parameters.AddWithValue("@empid", emp.EmpId);
                    cmd.Parameters.AddWithValue("@name", emp.Name);
                    cmd.Parameters.AddWithValue("@designation", emp.Designation);
                    cmd.Parameters.AddWithValue("@gender", emp.Gender);
                    cmd.Parameters.AddWithValue("@salary", emp.Salary);
                    cmd.Parameters.AddWithValue("@projectid", emp.ProjectId);
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
            catch (ProjectNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting employee: " + ex.Message);
                return false;
            }
        }



        public bool CreateProject(Project pj)
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    string query = @"INSERT INTO project 
                                    (projectid, projectname, description, startdate, status) 
                                     VALUES (@id, @name, @desc, @start, @status)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", pj.projectid);
                    cmd.Parameters.AddWithValue("@name", pj.projectname);
                    cmd.Parameters.AddWithValue("@desc", pj.description);
                    cmd.Parameters.AddWithValue("@start", pj.startdate);
                    cmd.Parameters.AddWithValue("@status", pj.status);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting project: " + ex.Message);
                return false;
            }
        }


        public bool CreateProjectTask(ProjectTask task)
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();

                    bool empValid = true;
                    bool projValid = true;

                    string checkEmpQuery = "SELECT COUNT(*) FROM employee WHERE empid = @eid";
                    SqlCommand checkEmpCmd = new SqlCommand(checkEmpQuery, conn);
                    checkEmpCmd.Parameters.AddWithValue("@eid", task.employeeid);
                    int empCount = (int)checkEmpCmd.ExecuteScalar();
                    if (empCount == 0) empValid = false;

                    string checkProjQuery = "SELECT COUNT(*) FROM project WHERE projectid = @pid";
                    SqlCommand checkProjCmd = new SqlCommand(checkProjQuery, conn);
                    checkProjCmd.Parameters.AddWithValue("@pid", task.projectid);
                    int projCount = (int)checkProjCmd.ExecuteScalar();
                    if (projCount == 0) projValid = false;

                    if (!empValid && !projValid)
                        throw new Exception(" Employee and Project not found with the given IDs.");
                    else if (!empValid)
                        throw new EmployeeNotFoundException(" Employee not found with the given ID.");
                    else if (!projValid)
                        throw new ProjectNotFoundException(" Project not found with the given ID.");

                    string query = @"INSERT INTO task 
                             (taskid, taskname, projectid, empid, status) 
                             VALUES (@taskid, @taskname, @projectid, @empid, @status)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@taskid", task.taskid);
                    cmd.Parameters.AddWithValue("@taskname", task.taskname);
                    cmd.Parameters.AddWithValue("@projectid", task.projectid);
                    cmd.Parameters.AddWithValue("@empid", task.employeeid);
                    cmd.Parameters.AddWithValue("@status", task.status);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (EmployeeNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (ProjectNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
                return false;
            }
        }


        public bool AssignProjectToEmployee(int projectId, int employeeId)
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();

                    bool empValid = true;
                    bool projValid = true;

                    //  Check if employee exists
                    string checkEmpQuery = "SELECT COUNT(*) FROM employee WHERE empid = @empid";
                    SqlCommand checkEmpCmd = new SqlCommand(checkEmpQuery, conn);
                    checkEmpCmd.Parameters.AddWithValue("@empid", employeeId);
                    int empCount = (int)checkEmpCmd.ExecuteScalar();
                    if (empCount == 0) empValid = false;

                    //  Check if project exists
                    string checkProjQuery = "SELECT COUNT(*) FROM project WHERE projectid = @projectid";
                    SqlCommand checkProjCmd = new SqlCommand(checkProjQuery, conn);
                    checkProjCmd.Parameters.AddWithValue("@projectid", projectId);
                    int projCount = (int)checkProjCmd.ExecuteScalar();
                    if (projCount == 0) projValid = false;

                    //  Throw exceptions accordingly
                    if (!empValid && !projValid)
                        throw new Exception(" Both Employee and Project not found with the given IDs.");
                    if (!empValid)
                        throw new EmployeeNotFoundException("Employee not found with the given ID.");
                    if (!projValid)
                        throw new ProjectNotFoundException(" Project not found with the given ID.");

                    string query = @"UPDATE employee 
                             SET projectid = @projectId 
                             WHERE empid = @employeeId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@projectId", projectId);
                    cmd.Parameters.AddWithValue("@employeeId", employeeId);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (EmployeeNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (ProjectNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Unexpected error: " + ex.Message);
                return false;
            }
        }

        public bool AssignTaskInProjectToEmployee(int taskId, int projectId, int employeeId)
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();

                    bool empValid = true;
                    bool projValid = true;
                    bool taskValid = true;

                    // Check employee
                    string checkEmpQuery = "SELECT COUNT(*) FROM employee WHERE empid = @empid";
                    SqlCommand checkEmpCmd = new SqlCommand(checkEmpQuery, conn);
                    checkEmpCmd.Parameters.AddWithValue("@empid", employeeId);
                    int empCount = (int)checkEmpCmd.ExecuteScalar();
                    if (empCount == 0) empValid = false;

                    // Check project
                    string checkProjQuery = "SELECT COUNT(*) FROM project WHERE projectid = @projectid";
                    SqlCommand checkProjCmd = new SqlCommand(checkProjQuery, conn);
                    checkProjCmd.Parameters.AddWithValue("@projectid", projectId);
                    int projCount = (int)checkProjCmd.ExecuteScalar();
                    if (projCount == 0) projValid = false;

                    // Check task
                    string checkTaskQuery = "SELECT COUNT(*) FROM task WHERE taskid = @taskid";
                    SqlCommand checkTaskCmd = new SqlCommand(checkTaskQuery, conn);
                    checkTaskCmd.Parameters.AddWithValue("@taskid", taskId);
                    int taskCount = (int)checkTaskCmd.ExecuteScalar();
                    if (taskCount == 0) taskValid = false;

                    // Throw appropriate exception
                    if (!empValid && !projValid && !taskValid)
                        throw new Exception(" Employee, Project, and Task not found with the given IDs.");
                    if (!empValid && !projValid)
                        throw new Exception(" Both Employee and Project not found with the given IDs.");
                    if (!empValid && !taskValid)
                        throw new Exception(" Employee and Task not found with the given IDs.");
                    if (!projValid && !taskValid)
                        throw new Exception(" Project and Task not found with the given IDs.");
                    if (!empValid)
                        throw new EmployeeNotFoundException(" Employee not found with the given ID.");
                    if (!projValid)
                        throw new ProjectNotFoundException(" Project not found with the given ID.");
                    if (!taskValid)
                        throw new Exception(" Task not found with the given ID.");

                    // Proceed to update
                    string query = @"UPDATE task 
                             SET empid = @empid 
                             WHERE taskid = @taskId AND projectid = @projectId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@empid", employeeId);
                    cmd.Parameters.AddWithValue("@taskId", taskId);
                    cmd.Parameters.AddWithValue("@projectId", projectId);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (EmployeeNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (ProjectNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(" " + ex.Message);
                return false;
            }
        }

        public bool DeleteEmployee(int empId)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open(); 

                string query = "DELETE FROM Employee WHERE EmpId = @empId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@empId", empId);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new EmployeeNotFoundException("Employee not found");
                }

                return true;
            }
        }



        public bool DeleteProject(int projId)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open(); 

                string query = "DELETE FROM Project WHERE ProjectId = @projId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@projId", projId);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new ProjectNotFoundException("Project not found");
                }

                return true;
            }
        }



        public List<ProjectTask> GetAllTasks(int empId, int projectId)
        {
            List<ProjectTask> taskList = new List<ProjectTask>();

            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();

                    bool empValid = true;
                    bool projValid = true;

                    // Check employee existence
                    string checkEmpQuery = "SELECT COUNT(*) FROM employee WHERE empid = @empid";
                    SqlCommand checkEmpCmd = new SqlCommand(checkEmpQuery, conn);
                    checkEmpCmd.Parameters.AddWithValue("@empid", empId);
                    int empCount = (int)checkEmpCmd.ExecuteScalar();
                    if (empCount == 0) empValid = false;

                    // Check project existence
                    string checkProjQuery = "SELECT COUNT(*) FROM project WHERE projectid = @projectid";
                    SqlCommand checkProjCmd = new SqlCommand(checkProjQuery, conn);
                    checkProjCmd.Parameters.AddWithValue("@projectid", projectId);
                    int projCount = (int)checkProjCmd.ExecuteScalar();
                    if (projCount == 0) projValid = false;

                    // Throw exceptions as per validation
                    if (!empValid && !projValid)
                        throw new Exception(" Both Employee and Project not found with the given IDs.");
                    if (!empValid)
                        throw new EmployeeNotFoundException(" Employee not found with the given ID.");
                    if (!projValid)
                        throw new ProjectNotFoundException(" Project not found with the given ID.");

                    // Fetch tasks
                    string query = @"SELECT taskid, taskname, projectid, empid, status 
                             FROM task 
                             WHERE empid = @empid AND projectid = @projectid";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@empid", empId);
                    cmd.Parameters.AddWithValue("@projectid", projectId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ProjectTask task = new ProjectTask
                        {
                            taskid = Convert.ToInt32(reader["taskid"]),
                            taskname = reader["taskname"].ToString(),
                            projectid = Convert.ToInt32(reader["projectid"]),
                            employeeid = Convert.ToInt32(reader["empid"]),
                            status = reader["status"].ToString()
                        };
                        taskList.Add(task);
                    }
                }
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

            return taskList;
        }





    }
}