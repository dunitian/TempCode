using System.Data;
using System.Windows.Forms;

namespace BakWin
{
    public partial class DTForm : Form
    {
        private DTForm()
        {
            InitializeComponent();
        }
        public DTForm(DataTable dt) : this()
        {
            dgv.DataSource = dt;
        }
    }
}
