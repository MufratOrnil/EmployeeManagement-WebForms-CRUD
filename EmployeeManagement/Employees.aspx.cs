using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeManagement
{
    public partial class Employees : System.Web.UI.Page
    {
        private readonly string _conString =
            ConfigurationManager.ConnectionStrings["EmployeeCon"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid(string nameFilter = null)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;

                if (string.IsNullOrWhiteSpace(nameFilter))
                {
                    cmd.CommandText =
                        "SELECT EmployeeId, Name, Email, Phone, Department, JoinDate " +
                        "FROM Employees ORDER BY EmployeeId DESC";
                }
                else
                {
                    cmd.CommandText =
                        @"SELECT EmployeeId, Name, Email, Phone, Department, JoinDate
                          FROM Employees
                          WHERE Name LIKE @Name
                          ORDER BY EmployeeId DESC";
                    cmd.Parameters.AddWithValue("@Name", "%" + nameFilter + "%");
                }

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    gvEmployees.DataSource = dt;
                    gvEmployees.DataBind();
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid(txtSearch.Text.Trim());
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            BindGrid();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            int employeeId;
            bool isUpdate = int.TryParse(hfEmployeeId.Value, out employeeId) && employeeId > 0;

            if (isUpdate)
            {
                UpdateEmployee(employeeId);
            }
            else
            {
                InsertEmployee();
            }

            ClearForm();
            BindGrid();
        }

        private void InsertEmployee()
        {
            using (SqlConnection con = new SqlConnection(_conString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText =
                    @"INSERT INTO Employees (Name, Email, Phone, Department, JoinDate)
                      VALUES (@Name, @Email, @Phone, @Department, @JoinDate)";

                cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@Phone",
                    string.IsNullOrWhiteSpace(txtPhone.Text) ? (object)DBNull.Value : txtPhone.Text.Trim());
                cmd.Parameters.AddWithValue("@Department",
                    string.IsNullOrWhiteSpace(txtDepartment.Text) ? (object)DBNull.Value : txtDepartment.Text.Trim());

                DateTime joinDate;
                if (DateTime.TryParse(txtJoinDate.Text.Trim(), out joinDate))
                    cmd.Parameters.AddWithValue("@JoinDate", joinDate);
                else
                    cmd.Parameters.AddWithValue("@JoinDate", DBNull.Value);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void UpdateEmployee(int employeeId)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText =
                    @"UPDATE Employees
                      SET Name = @Name,
                          Email = @Email,
                          Phone = @Phone,
                          Department = @Department,
                          JoinDate = @JoinDate
                      WHERE EmployeeId = @EmployeeId";

                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@Phone",
                    string.IsNullOrWhiteSpace(txtPhone.Text) ? (object)DBNull.Value : txtPhone.Text.Trim());
                cmd.Parameters.AddWithValue("@Department",
                    string.IsNullOrWhiteSpace(txtDepartment.Text) ? (object)DBNull.Value : txtDepartment.Text.Trim());

                DateTime joinDate;
                if (DateTime.TryParse(txtJoinDate.Text.Trim(), out joinDate))
                    cmd.Parameters.AddWithValue("@JoinDate", joinDate);
                else
                    cmd.Parameters.AddWithValue("@JoinDate", DBNull.Value);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void DeleteEmployee(int employeeId)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = @"DELETE FROM Employees WHERE EmployeeId = @EmployeeId";
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        protected void gvEmployees_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int employeeId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "EditRow")
            {
                LoadEmployeeForEdit(employeeId);
            }
            else if (e.CommandName == "DeleteRow")
            {
                DeleteEmployee(employeeId);
                BindGrid();
            }
        }

        private void LoadEmployeeForEdit(int employeeId)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText =
                    @"SELECT EmployeeId, Name, Email, Phone, Department, JoinDate
                      FROM Employees
                      WHERE EmployeeId = @EmployeeId";
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        lblFormTitle.Text = "Edit Employee";
                        hfEmployeeId.Value = rdr["EmployeeId"].ToString();
                        txtName.Text = rdr["Name"].ToString();
                        txtEmail.Text = rdr["Email"].ToString();
                        txtPhone.Text = rdr["Phone"].ToString();
                        txtDepartment.Text = rdr["Department"].ToString();

                        if (rdr["JoinDate"] != DBNull.Value)
                        {
                            DateTime jd = (DateTime)rdr["JoinDate"];
                            txtJoinDate.Text = jd.ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            txtJoinDate.Text = string.Empty;
                        }
                    }
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            hfEmployeeId.Value = string.Empty;
            txtName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtDepartment.Text = string.Empty;
            txtJoinDate.Text = string.Empty;
            lblFormTitle.Text = "Add New Employee";
        }
    }
}
