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
    public partial class Update : Form
    {
        DbManager db = new DbManager();
        public Update(int ID)
        {
            InitializeComponent();
            id = ID;
        }
        public int id { get; set; }
        private void Submit_Click(object sender, EventArgs e)
        {
            DateTime time = dateTimePicker1.Value;
            int status;
            if (completed.Checked) { status = 0; }
            else { status = 1; }
            db.UpdateTask(id, time, status);
            this.Close();
        }
    }
}
