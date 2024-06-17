namespace StufentManagementSystem
{
    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Class> Classes { get; set; }
    }

    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SchoolId { get; set; }
        public School School { get; set; }
        public List<Student> Students { get; set; }
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
    }
    public class SchoolRepository
    {
        private readonly string _connectionString;

        public SchoolRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void CreateSchool(School school)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Schools (Name) VALUES (@Name)";
                command.Parameters.AddWithValue("@Name", school.Name);
                command.ExecuteNonQuery();
            }
        }

        // 其他增删改查方法...
    }
    internal class Program
    {
        public void CreateSchool(School school)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Schools (Name) VALUES (@Name)";
                command.Parameters.AddWithValue("@Name", school.Name);
                command.ExecuteNonQuery();

                // 记录日志
                var logCommand = connection.CreateCommand();
                logCommand.CommandText = "INSERT INTO Logs (Operation, Entity, EntityId) VALUES (@Operation, @Entity, @EntityId)";
                logCommand.Parameters.AddWithValue("@Operation", "Create");
                logCommand.Parameters.AddWithValue("@Entity", "School");
                logCommand.Parameters.AddWithValue("@EntityId", school.Id);
                logCommand.ExecuteNonQuery();
            }
        }

    }
}
