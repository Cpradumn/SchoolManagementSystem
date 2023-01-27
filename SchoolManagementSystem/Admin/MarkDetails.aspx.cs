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
    public partial class MarkDetails : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetClass();
                GetMarks();
                //GetStudentName();
            }

        }

        private void GetMarks()
        {
            DataTable dt = fn.Fetch(@"Select ROW_NUMBER() over(order by (select 1)) as [Sr.No],e.RollNo, e.ClassId, c.ClassName,  
                                    e.SubjectId, s.SubjectName, e.TotalMarks, e.OutofMarks from Exam e inner join Class c on 
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

     

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string classId = ddlClass.SelectedValue;
                string rollNo = txtRoll.Text.Trim();
                DataTable dt = fn.Fetch(@"Select ROW_NUMBER() over(order by (select 1)) as [Sr.No],e.RollNo, e.ClassId, c.ClassName,  
                                    e.SubjectId, s.SubjectName, e.TotalMarks, e.OutofMarks from Exam e inner join Class c on 
                                    e.ClassId = c.ClassId inner join Subject s on e.SubjectId = s.SubjectId
                                    where e.ClassId = '" + classId+ "' and e.RollNo = '"+rollNo+"' ");

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
        }
    }
}