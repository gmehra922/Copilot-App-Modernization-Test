using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace WebApplicationG // <-- Use your actual namespace here
{
    public partial class _Default : System.Web.UI.Page
    {
        // In-memory data store
        private static List<Person> People = new List<Person>
        {
            new Person { Id = 1, Name = "Alice", Email = "alice@example.com" },
            new Person { Id = 2, Name = "Bob", Email = "bob@example.com" }
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindGrid();
        }

        void BindGrid()
        {
            GridView1.DataSource = People;
            GridView1.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int newId = People.Any() ? People.Max(p => p.Id) + 1 : 1;
            People.Add(new Person
            {
                Id = newId,
                Name = txtName.Text,
                Email = txtEmail.Text
            });
            txtName.Text = "";
            txtEmail.Text = "";
            BindGrid();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
            string name = ((TextBox)GridView1.Rows[e.RowIndex].Cells[0].Controls[0]).Text;
            string email = ((TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text;

            var person = People.FirstOrDefault(p => p.Id == id);
            if (person != null)
            {
                person.Name = name;
                person.Email = email;
            }
            GridView1.EditIndex = -1;
            BindGrid();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindGrid();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
            var person = People.FirstOrDefault(p => p.Id == id);
            if (person != null)
                People.Remove(person);
            BindGrid();
        }
    }
}