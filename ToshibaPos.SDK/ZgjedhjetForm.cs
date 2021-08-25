using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ToshibaPOS
{
    public partial class ZgjedhjetForm : Form
    {
        #region konstruktoret

        public ZgjedhjetForm()
        {
            InitializeComponent();

            this.CenterToParent();
        }

        #endregion

        #region deklarimet

        private DataTable _zgjidhja;
        private DataRow _rreshti;
        private string[] _kolonat;

        public DataTable Zgjidhja
        {
            get { return _zgjidhja; }
            set { _zgjidhja = value; }
        }

        public DataRow Rreshti
        {
            get { return _rreshti; }
            set { _rreshti = value; }
        }

        public string[] KolonatPerShfaqje
        {
            get { return _kolonat; }
            set { _kolonat = value; }
        }

        BindingSource bsZgjidhja = new BindingSource();

        #endregion

        #region ngjarjet e formes

        private void ZgjedhjetForm_Load(object sender, EventArgs e)
        {
            bsZgjidhja.DataSource = Zgjidhja;
            dataGridView1.DataSource = bsZgjidhja;

            if (Zgjidhja.Rows.Count > 0)
                _rreshti = Zgjidhja.Rows[0];

            if (_kolonat == null)
                _kolonat = new string[2] { "Shifra", "Pershkrimi" };

            foreach (DataGridViewColumn t in dataGridView1.Columns)
            {
                bool uGjet = false;
                foreach (string clm in _kolonat)
                {
                    if (t.Name.ToString() == clm)
                    {
                        uGjet = true;
                        t.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        break;
                    }
                }

                if (uGjet)
                    t.Visible = true;
                else
                    t.Visible = false;
            }
        }

        #endregion

        #region ngjarjet e butonit

        private void OKbutton_Click(object sender, EventArgs e)
        {
            PershkrimitextBox_KeyDown(sender, new KeyEventArgs(Keys.Enter));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Text = "Anulo";
            this.Close();
        }

        #endregion

        #region ngjarjet e textboxave

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //source1.Filter = "artist = 'Dave Matthews' OR cd = 'Tigerlily'";

            //ndertojme nje query dinamik
            string query = "";
            foreach (DataGridViewColumn t in dataGridView1.Columns)
            {
                if (t.ValueType == typeof(System.String))
                {
                    for (int i = 0; i < _kolonat.Length; i++)
                    {
                        if (t.Name.ToString() == _kolonat[i])
                        {
                            query = query + string.Format("{0} like '%" + PershkrimitextBox.Text.ToString().Replace("'", "''") + "%' or ", _kolonat[i]);
                            break;
                        }
                    }
                }
            }

            if (query.Length > 4)
                bsZgjidhja.Filter = query.Substring(0, query.Length - 4);
            else
                bsZgjidhja.Filter = "";

            // "Pershkrimi like '%" + PershkrimitextBox.Text.ToString() + "%'";
        }

        private void PershkrimitextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsNumber(e.KeyChar) && !char.IsSeparator(e.KeyChar)
                && e.KeyChar != '\b')
            {
                e.Handled = true;
                return;
            }
        }

        private void PershkrimitextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                bsZgjidhja.MovePrevious();
                DataRowView drv = bsZgjidhja.Current as DataRowView;
                if (drv != null)
                    _rreshti = drv.Row as DataRow;
            }
            else if (e.KeyCode == Keys.Down)
            {
                bsZgjidhja.MoveNext();
                DataRowView drv = bsZgjidhja.Current as DataRowView;
                if (drv != null)
                    _rreshti = drv.Row as DataRow;
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                bsZgjidhja.MoveLast();
                DataRowView drv = bsZgjidhja.Current as DataRowView;
                if (drv != null)
                    _rreshti = drv.Row as DataRow;
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                bsZgjidhja.MoveFirst();
                DataRowView drv = bsZgjidhja.Current as DataRowView;
                if (drv != null)
                    _rreshti = drv.Row as DataRow;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Text = "Anulo";
                this.Close();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (dataGridView1.Rows.Count < 1)
                {
                    this.Close();
                    return;
                }

                try
                {
                    this.Text = dataGridView1.CurrentRow.Cells["Id"].Value.ToString();
                    DataRowView drv = bsZgjidhja.Current as DataRowView;
                    _rreshti = drv.Row as DataRow;
                    this.Close();
                }
                catch (Exception ex)
                {
                    if (ex.Message.ToString() == "Object reference not set to an instance of an object.")
                    {
                        this.Text = dataGridView1.Rows[0].Cells["Id"].Value.ToString();
                        this.Close();
                    }

                }

            }
        }

        #endregion

        #region ngjarjet e gridit

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                this.Close();
                return;
            }

            try
            {
                this.Text = dataGridView1.CurrentRow.Cells["Id"].Value.ToString();
                DataRowView drv = bsZgjidhja.Current as DataRowView;
                _rreshti = drv.Row as DataRow;
                this.Close();
            }
            catch (Exception ex)
            {
                if (ex.Message.ToString() == "Object reference not set to an instance of an object.")
                {
                    this.Text = dataGridView1.Rows[0].Cells["Id"].Value.ToString();
                    this.Close();
                }

            }
        }

        #endregion
    }
}
