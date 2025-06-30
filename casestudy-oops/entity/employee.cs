namespace casestudy_oops.entity
{
    public class Employee
    {
        // Properties
        public int EmpId { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Gender { get; set; }
        public int Salary { get; set; }
        public int ProjectId { get; set; }

        // Default Constructor
        public Employee()
        {
        }

        // Parameterized Constructor
        public Employee(int empId, string name, string designation, string gender, int salary, int projectId)
        {
            EmpId = empId;
            Name = name;
            Designation = designation;
            Gender = gender;
            Salary = salary;
            ProjectId = projectId;
        }
    }
}