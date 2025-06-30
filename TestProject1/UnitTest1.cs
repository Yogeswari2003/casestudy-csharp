using casestudy_oops.dao;
using casestudy_oops.entity;
using casestudy_oops.myexceptions;

namespace TestProject1
{
    public class Tests
    {
        private IProjectRepository repo;

        [SetUp]
        public void Setup()
        {
            repo = new ProjectRepositoryImpl(); 
        }




        [Test]
        public void TestCreateEmployee_Success()
        {
            Employee emp = new Employee(106, "hari", "designer", "male", 50000, 1);
            bool result = repo.CreateEmployee(emp);
            Assert.IsTrue(result);

            // Cleanup (delete after test)
            repo.DeleteEmployee(106);
        }

        [Test]
        public void TestCreateTask_Success()
        {
            ProjectTask task = new ProjectTask(4, "profile page", 2, 1, "started");
            bool result = repo.CreateProjectTask(task);
            Assert.IsTrue(result);

            
        }



        [Test]
        public void TestGetTasksByEmployee_Success()
        {
            int employeeId = 101;
            int projectId = 102;
            List<ProjectTask> tasks = repo.GetAllTasks(employeeId,projectId);
            Assert.IsNotNull(tasks);
            Assert.IsTrue(tasks.Count >= 0); // Adjust based on setup data
        }

        [Test]
        public void TestDeleteNonExistentEmployee_ThrowsException()
        {
            var ex = Assert.Throws<EmployeeNotFoundException>(() =>
            {
                repo.DeleteEmployee(999); // Assuming 999 doesn't exist
            });

            Assert.That(ex.Message, Is.EqualTo("Employee not found"));
        }


        // 4. Test if ProjectNotFoundException is thrown
        [Test]
        public void TestDeleteNonExistentProject_ThrowsException()
        {
            var ex = Assert.Throws<ProjectNotFoundException>(() =>
            {
                repo.DeleteProject(999); // Assuming 999 doesn't exist
            });

            Assert.That(ex.Message, Is.EqualTo("Project not found"));
        }


    }
}