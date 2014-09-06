using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Membership.OpenAuth;
using WebParser.DAL.Model;

namespace WebParser.Account
{
    public partial class Register : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            LoginDTO item = new LoginDTO();
            item.Password = Password.Text;
            item.UserId = UserName.Text;
            item.IsAdmin = chktnAdmin.Checked;
            var connector = new WebParser.DAL.DataFunction.LoginFunctions();
            var obj = connector.DoRegister(item);
            if (obj == null)
            {
                lblMessage.Text = "User Id already exist.Please try with different user name.";
                lblMessage.Visible = true;
            }
            else
            {
                lblMessage.Text = "Loged In.";
                lblMessage.Visible = true;
                FormsAuthentication.SetAuthCookie(UserName.Text, createPersistentCookie: false);

                Label lbl = this.Master.FindControl("lblLoginName") as Label;
                lbl.Text = obj.UserId;
                HyperLink link = this.Master.FindControl("hypLogOut") as HyperLink;
                link.Text = obj.UserId;

                lbl.Visible = true;
                link.Visible = true;
                //string continueUrl = RegisterUser.ContinueDestinationPageUrl;
                //if (!OpenAuth.IsLocalUrl(continueUrl))
                //{
                //    continueUrl = "~/";
                //}
                Response.Redirect("~/ScanLoad.aspx?Id=" + item.UserId);
            }
        }
    }
}