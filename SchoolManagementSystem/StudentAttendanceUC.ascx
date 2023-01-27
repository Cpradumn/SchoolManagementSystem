<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudentAttendanceUC.ascx.cs" Inherits="SchoolManagementSystem.StudentAttendanceUC" %>

<div style="background-image: url(); width: 100%; height: 100px; background-repeat: no-repeat; background-size: cover; background-attachment: fixed;">
    <div class="container p-md-4 p-sm-4">
        <div>
            <asp:Label ID="lblmsg" runat="server"></asp:Label>
        </div>
        <h2 class="text-center">Student Attendance Details</h2>

        <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">
            <div class="col-md-6">
                <label for="lblClass">Class</label>
                <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-control" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlClass_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Class is Required"
                    ControlToValidate="ddlClass" Display="Dynamic" ForeColor="Red"
                    InitialValue="Select Class" SetFocusOnError="True">
                </asp:RequiredFieldValidator>
            </div>

            <div class="col-md-6">
                <label for="lblSubject">Subject</label>
                <asp:DropDownList ID="ddlSubject" runat="server" CssClass="form-control"></asp:DropDownList>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Subject is Required"
                    ControlToValidate="ddlSubject" Display="Dynamic" ForeColor="Red"
                    InitialValue="Select Subject" SetFocusOnError="True">
                </asp:RequiredFieldValidator>--%>
            </div>
        </div>

        <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">
            <div class="col-md-6">
                <label for="lblRollNo">Roll No</label>
                <asp:TextBox ID="txtRollNo" runat="server" CssClass="form-control" Placeholder="Enter Student Roll No"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Roll No is Required"
                    ControlToValidate="txtRollNo" Display="Dynamic" ForeColor="Red"
                    InitialValue="Select Class" SetFocusOnError="True">
                </asp:RequiredFieldValidator>
            </div>

            <div class="col-md-6">
                <label for="lblMonth">Month</label>
                <asp:TextBox ID="txtMonth" runat="server" CssClass="form-control" TextMode="Month"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Month is Required"
                    ControlToValidate="txtMonth" Display="Dynamic" ForeColor="Red"
                    InitialValue="Select Class" SetFocusOnError="True">
                </asp:RequiredFieldValidator>
            </div>
        </div>


        <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">
            <div class="col-md-3 col-md-offset-2 mb-3">
                <asp:Button ID="btnCheckAttendance" runat="server" CssClass="btn-primary btn-block; form-control" BackColor="#5558C9"
                    Text="Check Attendance"  OnClick="btnCheckAttendance_Click"/>
            </div>
        </div>

        <div class="row mb-3 mr-lg-5">
            <div class="col-md-12">
                <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered" EmptyDataText="No Record to Display"
                    AutoGenerateColumns="False" Width="980px">
                    <Columns>
                        <asp:BoundField DataField="Sr.No" HeaderText="Sr.No">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Name" HeaderText="Name">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="label1" Text='<%# Boolean.Parse(Eval("Status").ToString()) ? "Present" : "Absent" %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:dd / MMMM / yyyy}">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>

                    </Columns>
                    <HeaderStyle BackColor="#5558C9" ForeColor="White" />
                </asp:GridView>
            </div>
        </div>
    </div>
</div>
