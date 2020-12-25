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
    public partial class Info : Form
    {
        DbManager db = new DbManager();
        public Info()
        {
            InitializeComponent();
        }
        //Load the Form
        private void Info_Load(object sender, EventArgs e)
        {
            dataGrid.DataSource = db.GetAllTasks();
        }
        // Filter Tasks
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var clicked = treeView1.SelectedNode.Text;
            dataGrid.DataSource = db.GetFilteredTasks(clicked);
        }
        // Update Task
        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var id = int.Parse(dataGrid.CurrentRow.Cells["task_ID"].Value.ToString());
            Update form = new Update(id);
            form.Show();
        }
        //Search By ID or Name
        private void search_Click(object sender, EventArgs e)
        {   string input = textBox.Text;
            if(!String.IsNullOrWhiteSpace(input))
            {
                if(input.Any(char.IsDigit))
                {
                    try
                    {
                        dataGrid.DataSource = db.GetTasksById(int.Parse(input));
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Invalid query");
                    }
                }
                else
                {
                    dataGrid.DataSource = db.GetTasksByName(input);
                }
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            Add form = new Add();
            form.Show();
        }
    }
}
