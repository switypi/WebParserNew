using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebParser.DAL.Model;

namespace WebParser.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register";
            // OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];

            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }
            if (!Page.IsPostBack)
            {
                Label lbl = this.Master.FindControl("lblLoginName") as Label;
              
                HyperLink link = this.Master.FindControl("hypLogOut") as HyperLink;
             

                lbl.Visible = false;
                link.Visible = false;
            }

        }

        protected void login_Click(object sender, EventArgs e)
        {
            LoginDTO item = new LoginDTO();
            item.UserId = UserName.Text;
            item.Password = Password.Text;
            var connection = new WebParser.DAL.DataFunction.LoginFunctions();
            var obj = connection.DoLogin(item.UserId, item.Password);
            if (obj == null)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Login failed.";
            }
            else
            {
                Label lbl = this.Master.FindControl("lblLoginName") as Label;
                lbl.Text = obj.UserId;
                HyperLink link = this.Master.FindControl("hypLogOut") as HyperLink;
                link.Text = obj.UserId;

                lbl.Visible = true;
                link.Visible = true;
                Session["UserName"] = obj.UserId;
                FormsAuthentication.SetAuthCookie(item.UserId, createPersistentCookie: false);
                if (!obj.IsAdmin)
                    Response.Redirect("~/ScanLoad.aspx?Id="+obj.UserId);
                else
                    Response.Redirect("~/Admin.aspx?Id=" + obj.UserId);

            }
        }
    }
}