using System;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml;
using Npgsql;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;

namespace SK_App.Scripts
{
    public class Database
    {
        // LOGIC
        private NpgsqlConnection conn_;
        private string sql;
        private NpgsqlCommand cmd_;
        private NpgsqlDataAdapter adptr_;
        private DataTable dtable_;

        // DATABASE Connection
        public Database()
        {
            string connstring = String.Format("Server=localhost; Port=5432;" +
                                              "User Id = postgres; Password=admin; Database=StroyCompany;");
            conn_ = new NpgsqlConnection(connstring);
            cmd_ = new NpgsqlCommand();
            dtable_ = new DataTable();
            adptr_ = new NpgsqlDataAdapter();
        }
        public bool Connect()
        {
            try
            {
                conn_.Open();
                return true;
            }
            catch (Exception exp)
            {
                MessageBox.Show("error while opening connection (Database=>Connect()) : " + exp.Message);
                return false;
            }
        }
        public bool Disconnect()
        {
            try
            {
                conn_.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //// SELECT
        // Admin SELECT
        public object SelectALLforAdmin(string name)
        {
            Connect();
            sql = "SELECT * FROM " + name;
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        // Authorization
        public object SelectAuthInfo(string login, string password)
        {
            Connect();
            sql = "SELECT employees.employee_id, employees.name, employee_role_id " +
                  "FROM employees " +
                  "INNER JOIN employee_roles on employees.fk_employee_role_id=employee_roles.employee_role_id " +
                  "INNER JOIN authinfo ON employees.fk_authinfo_id = authinfo.authinfo_id " +
                  "WHERE login = '" + login + "' AND password = '" + password + "';";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        // Objects
        public object SelectAllObjects()
        {
            Connect();
            sql = "SELECT DISTINCT objects.object_id, projects.name, objects.name, adress,prime_cost " +
                  "FROM objects " +
                  "JOIN projects ON objects.fk_project_id = projects.project_id " +
                  "JOIN projects_employees ON projects_employees.project_id = projects.project_id ";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object SelectActualObjects()
        {
            Connect();
            sql = "SELECT DISTINCT objects.object_id, projects.name, objects.name, adress,prime_cost " +
                  "FROM objects " +
                  "JOIN projects ON objects.fk_project_id = projects.project_id " +
                  "JOIN projects_employees ON projects_employees.project_id = projects.project_id " +
                  "WHERE projects.end_date IS NULL;";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object SelectArchiveObjectsData()
        {
            Connect();
            sql = "SELECT DISTINCT objects.object_id, projects.name, objects.name, adress,prime_cost " +
                  "FROM objects " +
                  "JOIN projects ON objects.fk_project_id = projects.project_id " +
                  "JOIN projects_employees ON projects_employees.project_id = projects.project_id " +
                  "WHERE projects.end_date IS NOT NULL;";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object SelectObjectByEmployee(int id)
        {
            Connect();
            sql = "SELECT objects.object_id, projects.name, objects.name, adress,prime_cost " +
                  "FROM objects " +
                  "JOIN projects ON objects.fk_project_id = projects.project_id " +
                  "JOIN projects_employees ON projects_employees.project_id = projects.project_id " +
                  "WHERE projects.end_date IS NULL and projects_employees.employee_id = " + id;
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object SelectObjectByProject(int id)
        {
            Connect();
            sql = "SELECT objects.object_id, projects.name, objects.name, adress, prime_cost " +
                  "FROM objects " +
                  "JOIN projects ON objects.fk_project_id = projects.project_id" +
                  " WHERE projects.end_date IS NULL AND fk_project_id = " +id;
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object SelectObjectByEmployeeProject(int proj_id, int employ_id)
        {
            Connect();
            sql = "SELECT objects.object_id, projects.name, objects.name, adress, prime_cost " +
                  "FROM objects " +
                  "JOIN projects ON objects.fk_project_id = projects.project_id " +
                  "JOIN projects_employees ON projects.project_id = projects_employees.project_id " +
                  "WHERE projects.end_date IS NULL AND " +
                  "projects_employees.employee_id = " + employ_id + " AND fk_project_id = " + proj_id + " ;";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        // Projects
        public object SelectAllProjects()
        {
            Connect();
            sql = "SELECT project_id, name, start_date, end_date, working_hours, prime_cost_project FROM projects";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object SelectActualProjects()
        {
            Connect();
            sql = "SELECT project_id, name, start_date, end_date, working_hours, prime_cost_project FROM projects " +
                  "WHERE end_date IS NULL";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object SelectArchiveProjectsData()
        {
            Connect();
            sql = "SELECT project_id, name, start_date, end_date, working_hours, prime_cost_project FROM projects " +
                  "WHERE end_date IS NOT NULL";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object SelectProjectByEmployee(int id)
        {
            Connect();
            sql = "SELECT projects.project_id, projects.name, start_date, end_date, working_hours, prime_cost_project " +
                  "FROM Projects " +
                  "JOIN projects_employees ON projects.project_id = projects_employees.project_id " +
                  "WHERE end_date IS NULL AND projects_employees.employee_id = " + id;
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        // Employees
        public object SelectAllEmployees()
        {
            Connect();
            sql = "SELECT employees.name, employee_roles.name, phone_number, hourly_rate, hours_worked_month, salary " +
                  "FROM employees " +
                  "JOIN employee_roles ON fk_employee_role_id = employee_roles.employee_role_id ";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object SelectEmployeeById(int id)
        {
            Connect();
            sql = "SELECT employees.name, employee_roles.name, phone_number, hourly_rate, hours_worked_month, salary " +
                  "FROM employees " +
                  "JOIN employee_roles ON fk_employee_role_id = employee_roles.employee_role_id " +
                  "WHERE employee_id = " + id;
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object SelectEmployeeByRoleID(int id)
        {
            Connect();
            sql = "SELECT employees.name, employee_roles.name, phone_number, hourly_rate, hours_worked_month, salary " +
                  "FROM employees " +
                  "JOIN employee_roles ON fk_employee_role_id = employee_roles.employee_role_id " +
                  "WHERE fk_employee_role_id = " + id;
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object BkSelectEmployee()
        {
            Connect();
            sql = "SELECT employees.name, employee_roles.name, phone_number, hourly_rate, hours_worked_month, salary, login, password " +
                  "FROM employees " +
                  "JOIN authinfo ON authinfo.authinfo_id = fk_authinfo_id " +
                  "JOIN employee_roles ON fk_employee_role_id = employee_roles.employee_role_id";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object BkSelectEmployeeByID(int id)
        {
            Connect();
            sql = "SELECT employees.name, employee_roles.name, phone_number, hourly_rate, hours_worked_month, salary, login, password " +
                  "FROM employees " +
                  "JOIN authinfo ON authinfo.authinfo_id = fk_authinfo_id " +
                  "JOIN employee_roles ON fk_employee_role_id = employee_roles.employee_role_id " +
                  "WHERE employee_id = " + id;
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        // Clients
        public object SelectAllClients()
        {
            Connect();
            sql = "SELECT client_id, clients.name, clients.phone_number, projects.name " +
                  "FROM clients " +
                  "JOIN contracts ON contracts.fk_client_id = clients.client_id " +
                  "JOIN projects ON projects.fk_contract_id = contracts.contract_id ";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object SelectActualClients()
        {
            Connect();
            sql = "SELECT DISTINCT client_id, clients.name, clients.phone_number, projects.name " +
                  "FROM clients " +
                  "JOIN contracts ON contracts.fk_client_id = clients.client_id " +
                  "JOIN projects ON projects.fk_contract_id = contracts.contract_id " +
                  "WHERE projects.end_date IS NULL";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object SelectArchiveClientsData()
        {
            Connect();
            sql = "SELECT client_id, clients.name, clients.phone_number, projects.name " +
                  "FROM clients " +
                  "JOIN contracts ON contracts.fk_client_id = clients.client_id " +
                  "JOIN projects ON projects.fk_contract_id = contracts.contract_id " +
                  "WHERE projects.end_date IS NOT NULL ";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object SelectClientsByProjectId(int id)
        {
            Connect();
            sql = "SELECT client_id, clients.name, clients.phone_number, projects.name " +
                  "FROM clients " +
                  "JOIN contracts ON contracts.fk_client_id = clients.client_id " +
                  "JOIN projects ON projects.fk_contract_id = contracts.contract_id " +
                  "WHERE projects.end_date IS NULL AND project_id = " + id;
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        // Contracts
        public object SelectAllContracts()
        {
            Connect();
            sql = "SELECT contracts.contract_id, clients.name, projects.name, conclusion_date, payment_amount, final_amount " +
                  "FROM contracts " +
                  "JOIN clients ON contracts.fk_client_id = clients.client_id " +
                  "JOIN projects ON projects.fk_contract_id = contracts.contract_id ";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object SelectActualContracts()
        {
            Connect();
            sql = "SELECT contracts.contract_id, clients.name, projects.name, conclusion_date, payment_amount, final_amount " +
                  "FROM contracts " +
                  "JOIN clients ON contracts.fk_client_id = clients.client_id " +
                  "LEFT JOIN projects ON projects.fk_contract_id = contracts.contract_id " +
                  "WHERE projects.end_date IS NULL";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object SelectArchiveContractsData()
        {
            Connect();
            sql = "SELECT contracts.contract_id, clients.name, projects.name, conclusion_date, payment_amount, final_amount " +
                  "FROM contracts " +
                  "JOIN clients ON contracts.fk_client_id = clients.client_id " +
                  "JOIN projects ON projects.fk_contract_id = contracts.contract_id " +
                  "WHERE projects.end_date IS NOT NULL ";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object SelectContractsByClientId(int id)
        {
            Connect();
            sql = "SELECT contracts.contract_id, clients.name, projects.name, conclusion_date, payment_amount, final_amount " +
            "FROM contracts " +
            "JOIN clients ON contracts.fk_client_id = clients.client_id " +
            "JOIN projects ON projects.fk_contract_id = contracts.contract_id " +
            "WHERE client_id = " + id;
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        // Materials
        public object SelectAllMaterials()
        {
            Connect();
            sql =
                "SELECT materials.name, total_count " +
                "FROM materials ";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object BkSelectAllMaterials()
        {
            Connect();
            sql =
                "SELECT materials.name, total_count, unit_price, total_price " +
                "FROM materials ";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;

        }
        public object SelectAllMaterialsByObjectId(int id)
        {
            Connect();
            sql =
                "SELECT DISTINCT objects_materials.object_id, materials.name, objects_materials.count, objects_materials.total_price," +
                " objects.name, projects.name, adress " +
                "FROM materials " +
                "JOIN objects_materials ON objects_materials.material_id = materials.material_id " +
                "JOIN objects ON objects_materials.object_id = objects.object_id " +
                "JOIN projects ON objects.fk_project_id = projects.project_id " +
                "JOIN projects_employees ON projects.project_id = projects_employees.project_id " +
                "WHERE projects.end_date IS NULL AND objects.object_id = " + id;
                cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object SelectAllMaterialsByEmployee(int employee_id)
        {
            Connect();
            sql =
                "SELECT objects_materials.object_id, materials.name, objects_materials.count, objects.name, projects.name, adress " +
                "FROM materials " +
                "JOIN objects_materials ON objects_materials.material_id = materials.material_id " +
                "JOIN objects ON objects_materials.object_id = objects.object_id " +
                "JOIN projects ON projects.project_id = objects.fk_project_id " +
                "JOIN projects_employees ON projects_employees.project_id = projects.project_id " +
                "JOIN employees ON employees.employee_id = projects_employees.employee_id " +
                "WHERE projects.end_date IS NULL AND employees.employee_id = " + employee_id;
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;

        }
        // Messages
        public object SelectAllMessages()
        {
            Connect();
            sql =
                "SELECT message_id, employees.name, messages.message " +
                "FROM messages " +
                "JOIN employees ON employees.employee_id = messages.fk_employee_id";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object SelectMessagesByEmployee(int employee_id)
        {
            Connect();
            sql =
                "SELECT message_id, employees.name, messages.message " +
                "FROM messages " +
                "JOIN employees ON employees.employee_id = messages.fk_employee_id " +
                "WHERE messages.fk_employee_id = " + employee_id;
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        // Tasks
        public object SelectAllTasks()
        {
            Connect();
            sql = "SELECT tasks.task_id, tasks.name, employees.name, projects.name, objects.name, " +
                  "tasks.start_date, tasks.end_date, tasks.working_hours, tasks.prime_cost " +
                  "FROM tasks " +
                  "JOIN objects ON objects.object_id = tasks.fk_object_id " +
                  "JOIN projects ON tasks.fk_project_id = projects.project_id " +
                  "JOIN tasks_employees ON tasks_employees.task_id = tasks.task_id " +
                  "JOIN employees ON employees.employee_id = tasks_employees.employee_id ";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object SelectActualTasks()
        {
            Connect();
            sql = "SELECT tasks.task_id, tasks.name, employees.name, projects.name, objects.name, " +
                  "tasks.start_date, tasks.end_date, tasks.working_hours, tasks.prime_cost " +
                  "FROM tasks " +
                  "JOIN objects ON objects.object_id = tasks.fk_object_id " +
                  "JOIN projects ON tasks.fk_project_id = projects.project_id " +
                  "JOIN tasks_employees ON tasks_employees.task_id = tasks.task_id " +
                  "JOIN employees ON employees.employee_id = tasks_employees.employee_id " +
                  "WHERE tasks.end_date IS NULL";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object SelectArchiveTasksData()
        {
            Connect();
                sql = "SELECT tasks.task_id, tasks.name, employees.name, projects.name, objects.name, " +
                      "tasks.start_date, tasks.end_date, tasks.working_hours, tasks.prime_cost " +
                      "FROM tasks " +
                      "JOIN objects ON objects.object_id = tasks.fk_object_id " +
                      "JOIN projects ON tasks.fk_project_id = projects.project_id " +
                      "JOIN tasks_employees ON tasks_employees.task_id = tasks.task_id " +
                      "JOIN employees ON employees.employee_id = tasks_employees.employee_id " +
                      "WHERE tasks.end_date IS NOT NULL";
                cmd_ = new NpgsqlCommand(sql, conn_);
                adptr_ = new NpgsqlDataAdapter(cmd_);
                dtable_ = new DataTable();
                adptr_.Fill(dtable_);
                Disconnect();
                return dtable_;
        }
        public object SelectTasksByEmployeeId(int id)
        {
            Connect();
            sql = "SELECT tasks.task_id, tasks.name, employees.name, projects.name, objects.name, " +
                  "tasks.start_date, tasks.end_date, tasks.working_hours, tasks.prime_cost " +
                  "FROM tasks " +
                  "JOIN objects ON objects.object_id = tasks.fk_object_id " +
                  "JOIN projects ON tasks.fk_project_id = projects.project_id " +
                  "JOIN tasks_employees ON tasks_employees.task_id = tasks.task_id " +
                  "JOIN employees ON employees.employee_id = tasks_employees.employee_id " +
                  "WHERE tasks.end_date IS NULL AND employees.employee_id = " + id;
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object SelectTasksByObjectId(int id)
        {
            Connect();
            sql = "SELECT tasks.task_id, tasks.name, employees.name, projects.name, objects.name, " +
                  "tasks.start_date, tasks.end_date, tasks.working_hours, tasks.prime_cost " +
                  "FROM tasks " +
                  "JOIN objects ON objects.object_id = tasks.fk_object_id " +
                  "JOIN projects ON tasks.fk_project_id = projects.project_id " +
                  "JOIN tasks_employees ON tasks_employees.task_id = tasks.task_id " +
                  "JOIN employees ON employees.employee_id = tasks_employees.employee_id " +
                  "WHERE tasks.end_date IS NULL AND objects.object_id = " + id;
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object SelectTasksByProjectId(int id)
        {
            Connect();
            sql = "SELECT tasks.task_id, tasks.name, employees.name, projects.name, objects.name, " +
                  "tasks.start_date, tasks.end_date, tasks.working_hours, tasks.prime_cost " +
                  "FROM tasks " +
                  "JOIN objects ON objects.object_id = tasks.fk_object_id " +
                  "JOIN projects ON tasks.fk_project_id = projects.project_id " +
                  "JOIN tasks_employees ON tasks_employees.task_id = tasks.task_id " +
                  "JOIN employees ON employees.employee_id = tasks_employees.employee_id " +
                  "WHERE tasks.end_date IS NULL AND projects.project_id = " + id;
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object SelectEmployeeTasksByObjectId(int employee_id, int object_id)
        {
            Connect();
            sql = "SELECT tasks.task_id, tasks.name, employees.name, projects.name, objects.name, " +
                  "tasks.start_date, tasks.end_date, tasks.working_hours, tasks.prime_cost " +
                  "FROM tasks " +
                  "JOIN objects ON objects.object_id = tasks.fk_object_id " +
                  "JOIN projects ON tasks.fk_project_id = projects.project_id " +
                  "JOIN tasks_employees ON tasks_employees.task_id = tasks.task_id " +
                  "JOIN employees ON employees.employee_id = tasks_employees.employee_id " +
                  "WHERE tasks.end_date IS NULL AND " +
                  "objects.object_id = " + object_id + " AND employees.employee_id = " + employee_id;
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object SelectEmployeeTasksByProjectId(int employee_id, int project_id)
        {
            Connect();
            sql = "SELECT tasks.task_id, tasks.name, employees.name, projects.name, objects.name, " +
                  "tasks.start_date, tasks.end_date, tasks.working_hours, tasks.prime_cost " +
                  "FROM tasks " +
                  "JOIN objects ON objects.object_id = tasks.fk_object_id " +
                  "JOIN projects ON tasks.fk_project_id = projects.project_id " +
                  "JOIN tasks_employees ON tasks_employees.task_id = tasks.task_id " +
                  "JOIN employees ON employees.employee_id = tasks_employees.employee_id " +
                  "WHERE tasks.end_date IS NULL AND " +
                  "projects.project_id = " + project_id + " AND employees.employee_id = " + employee_id;
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        // Profile
        public object SelectEmployeeInfo(int id)
        {
            Connect();
            sql = "SELECT employees.name, phone_number, hourly_rate, employee_roles.name, hours_worked_month FROM employees " +
                  "JOIN employee_roles ON employees.fk_employee_role_id = employee_roles.employee_role_id " +
                  "WHERE employee_id = " + id;
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        // Select names
        public object SelectProjectsIDName()
        {
            Connect();
            sql = "SELECT projects.project_id, projects.name FROM projects";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object SelectObjectsIDName()
        {
            Connect();
            sql = "SELECT objects.object_id, objects.name FROM objects";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object SelectEmployeesIDName()
        {
            Connect();
            sql = "SELECT employees.employee_id, employees.name FROM employees";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object SelectClientsIDName()
        {
            Connect();
            sql = "SELECT client_id, name FROM clients";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object SelectMaterialsIDName()
        {
            Connect();
            sql = "SELECT materials.material_id, materials.name FROM materials";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object SelectEmployeeRolesIDName()
        {
            Connect();
            sql = "SELECT employee_roles.employee_role_id, employee_roles.name FROM employee_roles";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }

        //// UPDATE
        // Tasks
        public object StartTaskByEmployeeID(int emp_id, int task_id, string start_time)
        {
            Connect();
            sql = "UPDATE tasks_employees" +
                  "SET start_time = '" + start_time +
                  "' FROM tasks, employees " +
                  "WHERE tasks.task_id = tasks_employees.task_id AND employees.employee_id = tasks_employees.employee_id " +
                  "AND tasks.task_id = " + task_id + " AND employees.employee_id = " + emp_id;
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object StopTaskByEmployeeID(int emp_id, int task_id, string end_time)
        {
            Connect();
            sql = "UPDATE tasks_employees" +
                  "SET end_time = '" + end_time +
                  "' FROM tasks, employees " +
                  "WHERE tasks.task_id = tasks_employees.task_id AND employees.employee_id = tasks_employees.employee_id " +
                  "AND tasks.task_id = " + task_id + " AND employees.employee_id = " + emp_id;
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object EndTask(string datetime, int id)
        {
            Connect();
            sql = "UPDATE tasks SET end_time = " + datetime + "WHERE task_id = " + id + ";";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object UpdateTask(string name, int id)
        {
            Connect();
            sql = "UPDATE tasks " +
                  "SET name = '" + name + "' " +
                  "WHERE task_id =" + id + ";";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object UpdateTaskObject(int obj_id, int task_id)
        {
            Connect();
            sql = "UPDATE tasks " +
                  "SET fk_object_id = " + obj_id + " " +
                  "WHERE task_id =" + task_id + ";";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object CountTaskTimeByEmployee() // like trigger
        {
            Connect();
            sql = "UPDATE tasks_employees " +
                  "SET time_spent = end_time - start_time;";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object CountHoursTask()
        {
            Connect();
            sql = "UPDATE tasks t " +
                  "SET working_hours = tamp.sum_hours FROM " +
                  "(SELECT SUM(te.time_spent) AS sum_hours, te.task_id " +
                  "FROM tasks_employees as te " +
                  "GROUP BY te.task_id) AS tamp " +
                  "WHERE t.task_id = tamp.task_id;";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        } // like trigger
        public object CountTaskEmployeeCost() // like trigger
        {
            Connect();
            sql = "UPDATE tasks_employees " +
                  "SET prime_cost = EXTRACT(EPOCH FROM time_spent)/3600*hourly_rate " +
                  "FROM employees " +
                  "WHERE tasks_employees.employee_id = employees.employee_id;";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object CountTaskPrimeCost() // use like trigger
        {
            Connect();
            sql = "UPDATE tasks t " +
                  "SET prime_cost = tamp.sum_cost FROM " +
                  "(SELECT SUM(te.prime_cost) AS sum_cost, te.task_id " +
                  "FROM tasks_employees as te " +
                  "GROUP BY te.task_id) AS tamp " +
                  "WHERE t.task_id = tamp.task_id;";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object CountHoursEmployee()
        {
            Connect();
            sql = "UPDATE employees e " +
                  "SET hours_worked_month = tamp.sum_hours FROM " +
                  "(SELECT SUM(te.time_spent) AS sum_hours, te.employee_id " +
                  "FROM tasks_employees AS te " +
                  "GROUP BY te.employee_id) AS tamp " +
                  "WHERE e.employee_id = tamp.employee_id;";
                  cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        } // like trigger
        // Materials
        public object UpdateMaterialPrice(int id, string unit_price)
        {
            Connect();
            sql = "UPDATE materials " +
                  "SET unit_price = "+ unit_price +
                  " WHERE material_id = " + id + "";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object UpdateMaterialObjects(decimal count, int mat_id, int obj_id)
        {
            Connect();
            sql = "UPDATE objects_materials " +
                  "SET count = " + count + " " +
                  "WHERE material_id =" + mat_id + " AND object_id = " + obj_id + ";";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object CountTotalPriceObjectMaterials() // use like trigger
        {
            Connect();
            sql = "UPDATE objects_materials " +
                  "SET total_price = objects_materials.count * materials.unit_price " +
                  "FROM materials " +
                  "WHERE objects_materials.material_id = materials.material_id";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object CountNeedMaterials()
        {
            Connect();
            sql = "UPDATE materials m " +
                  "SET total_count = objm.sum_count FROM " +
                  "(SELECT SUM(ob.count) AS sum_count, ob.material_id " +
                  "FROM objects_materials as ob " +
                  "GROUP BY ob.material_id) AS objm " +
                  "WHERE m.material_id = objm.material_id";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        } // use like trigger
        // Objects
        public object UpdateObjects(string name, string adress, int objects)
        {
            Connect();
            sql = "UPDATE objects " +
                  "SET name = '" + name + "'," +
                  "adress = '" + adress + "'" +
                  "WHERE object_id =" + objects + ";";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object CountObjectsPrimeCost() // use like trigger
        {
            Connect();
            sql = "UPDATE objects o " +
                  "SET prime_cost = objm.sum_price FROM " +
                  "(SELECT SUM(ob.total_price) AS sum_price, ob.object_id " +
                  "FROM objects_materials as ob " +
                  "GROUP BY ob.object_id) " +
                  "AS objm " +
                  "WHERE o.object_id = objm.object_id";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        // Clients
        public object UpdateClient(string name, string phone_number, int client_id)
        {
            Connect();
            sql = "UPDATE clients " +
                  "SET name = '" + name + "'," +
                  "phone_number = '" + phone_number + "' " +
                  "WHERE client_id =" + client_id + ";";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        // Message
        public object UpdateMessage(string message, int mes_id)
        {
            Connect();
            sql = "UPDATE messages " +
                  "SET message = '" + message + "' " +
                  "WHERE message_id =" + mes_id + ";";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        // Projects
        public object EndProject(string datetime, int id)
        {
            Connect();
            sql = "UPDATE projects SET end_date = '" + datetime + "' WHERE project_id = " + id + ";";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object UpdateProject(string name, int id)
        {
            Connect();
            sql = "UPDATE projects " +
                  "SET name = '" + name + "' " +
                  "WHERE project_id =" + id + ";";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object CountProjectsPrimeCost() // use like trigger
        {
            Connect();
            sql = "UPDATE projects " +
                  "SET prime_cost_project = objects.prime_cost+tasks.prime_cost " +
                  "FROM objects, tasks " +
                  "WHERE projects.project_id = objects.fk_project_id " +
                  "AND projects.project_id = tasks.fk_project_id " +
                  "AND tasks.end_date IS NOT NULL";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object CountProjectWorkingHours() // use like trigger
        {
            Connect();
            sql = "UPDATE projects " +
                  "SET working_hours = twh.work_hours " +
                  "FROM " +
                  "(SELECT SUM(t.working_hours) AS work_hours, t.fk_project_id " +
                  "FROM tasks AS t GROUP BY t.fk_project_id) AS twh " +
                  "WHERE projects.project_id = twh.fk_project_id";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        // Contracts
        public object UpdateContractPayment(decimal payment, int id)
        {
            Connect();
            sql = "UPDATE contracts " +
                  "SET payment_amount = payment_amount +" + payment + " " +
                  "WHERE contract_id =" + id + ";";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object UpdateContractEnterProject(int contract_id, int project_id)
        {
            Connect();
            sql = "UPDATE projects " +
                  "SET fk_contract_id = " + contract_id + " " +
                  "WHERE project_id =" + project_id + ";";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object UpdateContractDissEnterProject(int contract_id, int project_id)
        {
            Connect();
            sql = "UPDATE projects " +
                  "SET fk_contract_id = NULL " +
                  "WHERE project_id =" + project_id + " AND fk_contract_id =  " + contract_id + ";";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object ContractFinalAmount() // use like trigger
        {
            Connect();
            sql = "UPDATE contracts " +
                  "SET final_amount = (projects.prime_cost_project % 20) + projects.prime_cost_project " +
                  "FROM projects " +
                  "WHERE projects.fk_contract_id  = contracts.contract_id " +
                  "AND projects.end_date IS NOT NULL";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        // Employees
        public object UpdateEmployeeContactInfo(string name, string telephone, int id)
        {
            Connect();
            sql = "UPDATE employees " +
                  "SET name = " + name + ", " +
                  "SET phone_number = " + telephone +", " +
                  "WHERE employee_id =" + id + ";";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object UpdateEmployeeRate(decimal rate, int id)
        {
            Connect();
            sql = "UPDATE employees " +
                  "SET hourly_rate = " + rate + " " +
                  "WHERE employee_id =" + id + ";";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object UpdateEmployeeRole(int role, int id)
        {
            Connect();
            sql = "UPDATE employees " +
                  "SET fk_employee_role_id = " + role + " " +
                  "WHERE employee_id =" + id + ";";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        //// CREATE
        public object CreateTask(string name, string start_date, int project_id)
        {
            Connect();
            sql = "INSERT INTO tasks(name, start_date, fk_project_id) " +
                  "VALUES(" + name + ", " + start_date + ", " + project_id + ");";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object CreateProject(string name, string start_date)
        {
            Connect();
            sql = "INSERT INTO projects(name, start_date) VALUES " +
                  "('" + name + "', '" + start_date + "' );";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object CreateEmployee(string name, string phone, decimal rate, int role, string login, string password )
        {
            Connect();
            sql = "INSERT INTO authinfo(login,password) VALUES(" + login +", " + password + ");" +
                  "INSERT INTO employees(name, phone_number, hourly_rate, fk_employee_role_id, fk_authinfo_id)" +
                  " VALUES ('" + name + "', '" + phone + "', " + rate + ", " + role + ", (SELECT MAX(authinfo_id) FROM authinfo));";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object CreateMaterials(string name, int total_count, decimal unit_price)
        {
            Connect();
            sql = "INSERT INTO materials(name, total_count, unit_price) VALUES " +
                  "('" + name + "', " + total_count + ", " + unit_price + " );";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object CreateObject(string name, string adress, int project)
        {
            Connect();
            sql = "INSERT INTO objects(name, adress, fk_project_id) " +
                  "VALUES('" + name + "', '" + adress + "', " + project+ ");";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object CreateClient(string name, string phone_number)
        {

            try
            {
                Connect();
                sql = "INSERT INTO clients(name, phone_number) VALUES ('" + name + "', '" + phone_number + "' );";
                cmd_ = new NpgsqlCommand(sql, conn_);
                adptr_ = new NpgsqlDataAdapter(cmd_);
                dtable_ = new DataTable();
                adptr_.Fill(dtable_);
                Disconnect();
            }
            catch
            {
                MessageBox.Show("Пользователь не найден. Проверьте введенные данные",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Disconnect();
            }
            return dtable_;
        }
        public object CreateContract(int client_id, decimal payment, string conclustion_date)
        {
            try
            {
                Connect();
                sql = "INSERT INTO contracts(conclusion_date, payment_amount fk_client_id) VALUES " +
                      "('" + conclustion_date + "', " + payment + ", " + client_id +" );";
                cmd_ = new NpgsqlCommand(sql, conn_);
                adptr_ = new NpgsqlDataAdapter(cmd_);
                dtable_ = new DataTable();
                adptr_.Fill(dtable_);
                Disconnect();
            }
            catch
            {
                MessageBox.Show("Клиент не найден. Проверьте введенные данные",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Disconnect();
            }
            return dtable_;
        }
        public object CreateMessage(int emp_id, string message)
        {
            Connect();
            sql = "INSERT INTO messages(message, fk_employee_id) " +
                  "VALUES('" + message + "', " + emp_id + ");";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object CreateObjectsMaterials(int obj_id, int mat_id)
        {
            Connect();
            sql = "INSERT INTO objects_materials(object_id, material_id) VALUES " +
                  "(" + obj_id + " , " + mat_id+ ");";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        
        //// DELETE
        public object DeleteObjects(int object_id)
        {
            Connect();
            sql = "DELETE FROM objects WHERE object_id = " + object_id + ";";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object DeleteClients(int client_id)
        {
            Connect();
            sql = "DELETE FROM clients WHERE client_id =" + client_id + ";";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object DeleteTasks(int task_id)
        {
            Connect();
            sql = "DELETE FROM tasks WHERE task_id = " + task_id + ";";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object DeleteMessage(int message_id)
        {
            Connect();
            sql = "DELETE FROM messages WHERE message_id = " + message_id + ";";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object DeleteContracts(int contract_id)
        {
            Connect();
            sql = "DELETE FROM contracts WHERE contract_id = " + contract_id + ";";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object DeleteEmployee(int employee_id)
        {
            Connect();
            sql = "DELETE FROM employees WHERE employee_id = " + employee_id + ";";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object DeleteProjects(int project_id)
        {
            Connect();
            sql = "DELETE FROM projects WHERE project_id = " + project_id + ";";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object DeleteMaterials(int material_id)
        {
            Connect();
            sql = "DELETE FROM materials WHERE material_id = " + material_id + ";";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
        public object DeleteObjectsMaterials(int material_id, int object_id)
        {
            Connect();
            sql = "DELETE FROM objects_materials WHERE material_id = " + material_id + " AND objects_id = " + object_id +";";
            cmd_ = new NpgsqlCommand(sql, conn_);
            adptr_ = new NpgsqlDataAdapter(cmd_);
            dtable_ = new DataTable();
            adptr_.Fill(dtable_);
            Disconnect();
            return dtable_;
        }
    }
}
