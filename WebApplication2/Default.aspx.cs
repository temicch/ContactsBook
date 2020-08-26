using Application.BLL.Services;
using System;
using System.Web.UI;

namespace WebApplication2
{
    public partial class _Default : Page
    {
        public IContactsService ContactsService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}