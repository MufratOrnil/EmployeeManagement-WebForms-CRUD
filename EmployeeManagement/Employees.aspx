<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Employees.aspx.cs" Inherits="EmployeeManagement.Employees" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Management</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body class="bg-light">
    <form id="form1" runat="server" class="container my-4">
        <h2 class="mb-4 text-center">Employee Management</h2>

        <!-- Search -->
        <div class="card mb-4">
            <div class="card-body">
                <div class="row g-2 align-items-end">
                    <div class="col-md-6">
                        <label for="txtSearch" class="form-label">Search by Name</label>
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <asp:Button ID="btnSearch" runat="server" Text="Search"
                            OnClick="btnSearch_Click" CausesValidation="False"
                            CssClass="btn btn-primary me-2" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear"
                            OnClick="btnClear_Click" CausesValidation="False"
                            CssClass="btn btn-outline-secondary" />
                    </div>
                </div>
            </div>
        </div>

        <!-- Add / Edit form -->
        <asp:HiddenField ID="hfEmployeeId" runat="server" />

        <div class="card mb-4">
            <div class="card-header">
                <asp:Label ID="lblFormTitle" runat="server" Text="Add New Employee"></asp:Label>
            </div>
            <div class="card-body">
                <div class="row g-3">
                    <div class="col-md-6">
                        <label for="txtName" class="form-label">Name</label>
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvName" runat="server"
                            ControlToValidate="txtName" ErrorMessage="Name is Required"
                            ForeColor="Red" Display="Dynamic" />
                    </div>
                    <div class="col-md-6">
                        <label for="txtEmail" class="form-label">Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                            ControlToValidate="txtEmail" ErrorMessage="Email is Required"
                            ForeColor="Red" Display="Dynamic" />
                    </div>
                    <div class="col-md-6">
                        <label for="txtPhone" class="form-label">Phone</label>
                        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <label for="txtDepartment" class="form-label">Department</label>
                        <asp:TextBox ID="txtDepartment" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <label for="txtJoinDate" class="form-label">Join Date</label>
                        <asp:TextBox ID="txtJoinDate" runat="server" CssClass="form-control"
                            Placeholder="yyyy-MM-dd"></asp:TextBox>
                    </div>
                </div>

                <div class="mt-3">
                    <asp:Button ID="btnSave" runat="server" Text="Save"
                        OnClick="btnSave_Click" CssClass="btn btn-success" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                        OnClick="btnCancel_Click" CausesValidation="False"
                        CssClass="btn btn-outline-secondary ms-2" />
                </div>

                <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                    ForeColor="Red" CssClass="text-danger mt-2" />
            </div>
        </div>

        <!-- GridView -->
        <div class="card">
            <div class="card-header">
                Employees
            </div>
            <div class="card-body p-0">
                <asp:GridView ID="gvEmployees" runat="server"
                    CssClass="table table-striped table-bordered mb-0"
                    AutoGenerateColumns="False" DataKeyNames="EmployeeId"
                    OnRowCommand="gvEmployees_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="EmployeeId" HeaderText="ID" ReadOnly="True" />
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="Phone" HeaderText="Phone" />
                        <asp:BoundField DataField="Department" HeaderText="Department" />
                        <asp:BoundField DataField="JoinDate" HeaderText="Join Date" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit"
                                    CommandName="EditRow"
                                    CommandArgument='<%# Eval("EmployeeId") %>'
                                    CausesValidation="False"
                                    CssClass="btn btn-sm btn-outline-primary" />
                                &nbsp;
                                <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete"
                                    CommandName="DeleteRow"
                                    CommandArgument='<%# Eval("EmployeeId") %>'
                                    OnClientClick="return confirm('Are you sure you want to delete this employee?');"
                                    CausesValidation="False"
                                    CssClass="btn btn-sm btn-outline-danger" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
