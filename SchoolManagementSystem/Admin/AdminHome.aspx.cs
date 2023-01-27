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
    public partial class AdminHome : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            else
            {
                StudentCount();
                TeacherCount();
                ClassCount();
                SubjectsCount();
            }

        }

        void StudentCount()
        {
            DataTable dt = fn.Fetch("Select Count(*) from Student ");
            Session["Student"] = dt.Rows[0][0];
        }
        void TeacherCount()
        {
            DataTable dt = fn.Fetch("Select Count(*) from Teacher ");
            Session["Teacher"] = dt.Rows[0][0];
        }
        void ClassCount()
        {
            DataTable dt = fn.Fetch("Select Count(*) from Class ");
            Session["Class"] = dt.Rows[0][0];
        }
        void SubjectsCount()
        {
            DataTable dt = fn.Fetch("Select Count(*) from Subject ");
            Session["Subjects"] = dt.Rows[0][0];
        }
    }
}