using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SchoolManagementSystem.Models.CommonFn;

namespace SchoolManagementSystem.Admin
{
    public partial class Marks : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                GetClass();
                GetMarks();
               
            }
        }

        private void GetMarks()
        {
            DataTable dt = fn.Fetch(@"Select ROW_NUMBER() over(order by (select 1)) as [Sr.No], e.ExamId, e.ClassId, c.ClassName, 
                                    e.SubjectId, s.SubjectName, e.RollNo, e.TotalMarks, e.OutofMarks from Exam e inner join Class c on 
                                    e.ClassId = c.ClassId inner join Subject s on e.SubjectId = s.SubjectId");

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        private void GetClass()
        {
            DataTable dt = fn.Fetch("Select * from Class");
            ddlClass.DataSource = dt;
            ddlClass.DataTextField = "ClassName";
            ddlClass.DataValueField = "ClassId";
            ddlClass.DataBind();
            ddlClass.Items.Insert(0, "Select Class");
        }

        //private void GetRollNo()
        //{
        //    DataTable dt = fn.Fetch("Select * from Student");
        //    ddlClass.DataSource = dt;
        //    ddlClass.DataTextField = "RollNo";
        //    ddlClass.DataValueField = "ClassId";
        //    ddlClass.DataBind();
        //    ddlClass.Items.Insert(0, "Select Class");
        //}


        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            string classId = ddlClass.SelectedValue;
            DataTable dt = fn.Fetch("Select * from Subject where ClassId= '" + classId + "' ");
            ddlSubject.DataSource = dt;
            ddlSubject.DataTextField = "SubjectName";
            ddlSubject.DataValueField = "SubjectId";
            ddlSubject.DataBind();
            ddlSubject.Items.Insert(0, "Select Subject");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string classId = ddlClass.SelectedValue;
                string subjectId = ddlSubject.SelectedValue;
                string rollNo = txtRoll.Text.Trim();
                string studentMarks = txtStudentMarks.Text.Trim();
                string outOfmarks = txtOutOfMarks.Text.Trim();

                DataTable dttbl = fn.Fetch("Select StudentId from Student Where ClassId='" + classId +
                                        "'and RollNo='" + rollNo + "' ");

                if(dttbl.Rows.Count > 0)
                {
                    DataTable dt = fn.Fetch("Select * from Exam Where ClassId='" + classId +
                                        "'and SubjectId='" + subjectId + "' and RollNo = '" + rollNo + "' ");
                    if (dt.Rows.Count == 0)
                    {
                        string query = "Insert into Exam values('" + classId + "','" + subjectId + "','" + rollNo + "','" + studentMarks + "','" + outOfmarks + "')";
                        fn.Query(query);
                        lblmsg.Text = "Inserted Sucessfully !";
                        lblmsg.CssClass = "alert alert-success";
                        ddlClass.SelectedIndex = 0;
                        ddlSubject.SelectedIndex = 0;
                        txtRoll.Text = string.Empty;
                        txtStudentMarks.Text = string.Empty;
                        txtOutOfMarks.Text = string.Empty;
                        GetMarks();

                    }
                    else
                    {
                        lblmsg.Text = "Entered <b> Data </b> does not exists for Selected Class !";
                        lblmsg.CssClass = "alert alert-danger";
                    }
                }
                else
                {
                    lblmsg.Text = "Entered Roll No <b> '"+rollNo+"' </b> already exists!";
                    lblmsg.CssClass = "alert alert-danger";
                }
                
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetMarks();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetMarks();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetMarks();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int examId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                string classId = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[2].FindControl("ddlClassGV")).SelectedValue;
                string subjectId = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[2].FindControl("ddlSubjectGV")).SelectedValue;
                string rollNo = (row.FindControl("txtRollNoGV") as TextBox).Text.Trim();
                string studentMarks = (row.FindControl("txtStudentMarksGV") as TextBox).Text.Trim();
                string outOfMarks = (row.FindControl("txtOutOfMarksGV") as TextBox).Text.Trim();
                fn.Query(@"Update Exam set ClassId='" + classId + "', SubjectId = '" + subjectId + "' , RollNo = '" + rollNo + "'," +
                    " TotalMarks = '" + studentMarks + "', OutofMarks = '" + outOfMarks + "' where ExamId = '" + examId + "' ");
                lblmsg.Text = "Recoed Updated Sucessfully";
                lblmsg.CssClass = "alert alert-sucess";
                GridView1.EditIndex = -1;
                GetMarks();

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList ddlClass = (DropDownList)e.Row.FindControl("ddlClassGV");
                    DropDownList ddlSubject = (DropDownList)e.Row.FindControl("ddlSubjectGV");
                    DataTable dt = fn.Fetch("Select * from Subject where ClassId = '" + ddlClass.SelectedValue + "' ");
                    ddlSubject.DataSource = dt;
                    ddlSubject.DataTextField = "SubjectName";
                    ddlSubject.DataValueField = "SubjectId";
                    ddlSubject.DataBind();
                    ddlSubject.Items.Insert(0, "Select Subject");
                    string selectedSubject = DataBinder.Eval(e.Row.DataItem, "SubjectName").ToString();
                    ddlSubject.Items.FindByText(selectedSubject).Selected = true;
                    

                }
            }
        }

        protected void ddlClassGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlClassSelected = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddlClassSelected.NamingContainer;
            if (row != null)
            {
                if ((row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList ddlSubjectGV = (DropDownList)row.FindControl("ddlSubjectGV");
                    DataTable dt = fn.Fetch("Select * from Subject where ClassId = '" + ddlClassSelected.SelectedValue + "' ");
                    ddlSubjectGV.DataSource = dt;
                    ddlSubjectGV.DataTextField = "SubjectName";
                    ddlSubjectGV.DataValueField = "SubjectId";
                    ddlSubjectGV.DataBind();
                }
            }
        }

       
    }
}