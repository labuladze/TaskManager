using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskManager
{
    public partial class Add : Form
    {
        DbManager db = new DbManager();
        public Add()
        {
            InitializeComponent();
            var list = db.GetEmployees();
            foreach (var item in list)
            {
                checkedListBox.Items.Add($"{item.name}  ID:{item.ID}", false);
            }

        }
        // Adding Tasks and Relationship 
        private void addButton_Click(object sender, EventArgs e)
        {   List<int> list_ID = new List<int>();
            var checked_list = checkedListBox.CheckedItems;
            foreach (var item in checked_list)
            {   string s = item.ToString();
                int b = s.IndexOf(":") +1;
                int l = s.Length - b;
                list_ID.Add(int.Parse(s.Substring(b, l)));
            }
            var table = db.GetAllTasks();
            DataRow lastRow = table.Rows[table.Rows.Count - 1];
            int task_id = int.Parse(lastRow["task_ID"].ToString()) + 1;
            db.AddTask(creationDate.Value, dueDate.Value, descriptionText.Text);
            foreach (var item in list_ID)
            {
                db.AddRelationship(task_id, item);
            }
            this.Close();
        }
    }
}
