using System;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        string connectionString = "Data Source=(local);Initial Catalog=YourDatabaseName;Integrated Security=True"; 

        // Запрос на получение сотрудника с максимальной заработной платой
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT TOP 1 * FROM EMPLOYEE ORDER BY SALARY DESC";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Console.WriteLine("Сотрудник с максимальной заработной платой:");
                    Console.WriteLine($"Имя: {reader["NAME"]}");                    
                    Console.WriteLine($"Заработная плата: {reader["SALARY"]}");
                }
                else
                {
                    Console.WriteLine("Сотрудников не найдено");
                }
            }
            connection.Close();
        }

        // Запрос на вычисление глубины дерева руководителей в таблице Сотрудники
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = "WITH CTE AS (SELECT Id, CHIEF_ID, 1 AS DEPTH FROM EMPLOYEE WHERE CHIEF_ID IS NULL UNION ALL SELECT emp.Id, emp.CHIEF_ID, c.DEPTH + 1 FROM EMPLOYEE emp INNER JOIN CTE c ON emp.CHIEF_ID = c.Id) SELECT MAX(DEPTH) AS MAX_DEPTH FROM CTE";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                object result = command.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    Console.WriteLine($"Максимальная длина цепочки руководителей: {result}");
                }
                else
                {
                    Console.WriteLine("Данные не найдены");
                }
            }
            connection.Close();
        }

        // Запрос на получение отдела с максимальной суммарной зарплатой сотрудников
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT TOP 1 dep.* FROM DEPARTMENT dep INNER JOIN EMPLOYEE emp ON dep.Id = emp.DEPARTMENT_ID GROUP BY dep.Id, dep.NAME ORDER BY SUM(emp.SALARY) DESC";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Console.WriteLine("Отдел с максимальной суммарной зарплатой сотрудников:");
                    Console.WriteLine($"ID: {reader["ID"]}");
                    Console.WriteLine($"Наименование: {reader["NAME"]}");
                }
                else
                {
                    Console.WriteLine("Отделы не найдены");
                }
            }
            connection.Close();
        }
        // Запрос на получение сотрудника, имя которого начинается на "Р" и заканчивается на "н"
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT * FROM EMPLOYEE WHERE NAME LIKE 'Р%н'";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Console.WriteLine("Сотрудник, имя которого начинается на 'Р' и заканчивается на 'н': ");
                    Console.WriteLine($"Имя: {reader["NAME"]}");                    
                }
                else
                {
                    Console.WriteLine("Сотрудник не найден");
                }
            }
            connection.Close();
        }

        Console.ReadLine();
    }
}
