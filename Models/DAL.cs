using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;

namespace CRUDAPPDOTNETCORE.Models
{
    public class DAL
    {
        public Response GetAllEmployees(SqlConnection connection)
        {
            Response response = new Response();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM TblCrudNetCore", connection);
            DataTable dataTable = new DataTable();
            List<Employee> listEmployees = new List<Employee>();
            adapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Employee employee = new Employee();
                    employee.Id = Convert.ToInt32(dataTable.Rows[i]["ID"]);
                    employee.Name = Convert.ToString(dataTable.Rows[i]["Name"]);
                    employee.Email = Convert.ToString(dataTable.Rows[i]["Email"]);
                    employee.IsActive = Convert.ToInt32(dataTable.Rows[i]["IsActive"]);
                    listEmployees.Add(employee);
                }
            }
            if (listEmployees.Count > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Data Found";
                response.listEmployee = listEmployees;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No Data Found";
                response.listEmployee = null;
            }

            return response;
        }

        public Response GetAllEmployeeById(SqlConnection connection, int id)
        {
            Response response = new Response();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM TblCrudNetCore WHERE ID= '" + id + "' AND IsActive=1", connection);
            DataTable dataTable = new DataTable();
            Employee Employees = new Employee();
            adapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {

                Employee employee = new Employee();
                employee.Id = Convert.ToInt32(dataTable.Rows[0]["ID"]);
                employee.Name = Convert.ToString(dataTable.Rows[0]["Name"]);
                employee.Email = Convert.ToString(dataTable.Rows[0]["Email"]);
                employee.IsActive = Convert.ToInt32(dataTable.Rows[0]["IsActive"]);
                response.StatusCode = 200;
                response.StatusMessage = "Data Found";
                response.Employee = employee;

            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No Data Found";
                response.Employee = null;
            }

            return response;
        }

        public Response AddEmployee(SqlConnection connection, Employee employee)
        {
            Response response = new Response();
            SqlCommand command = new SqlCommand("INSERT INTO TblCrudNetCore(Name,Email,IsActive,CreatedOn) VALUES('" + employee.Name + "','" + employee.Email + "','" + employee.IsActive + "',GETDATE())", connection);
            connection.Open();
            int i = command.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Employee added.";

            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No Data inserted";
            }

            return response;
        }


        public Response UpdateEmployee(SqlConnection connection, Employee employee)
        {
            Response response = new Response();
            SqlCommand command = new SqlCommand("UPDATE TblCrudNetCore SET Name='" + employee.Name + "' ,Email='" + employee.Email + "' WHERE ID= '" + employee.Id + "'", connection);
            connection.Open();
            int i = command.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Employee updated.";

            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No Data updated";
            }

            return response;
        }

        public Response DeleteEmployee(SqlConnection connection, int id)
        {
            Response response = new Response();
            SqlCommand command = new SqlCommand("DELETE FROM TblCrudNetCore WHERE ID = '"+id+"'", connection);
            connection.Open();
            int i = command.ExecuteNonQuery();
            connection.Close();

            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Employee deleted";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No Employee deleted";
            }
            return response;
        }
    }
}
