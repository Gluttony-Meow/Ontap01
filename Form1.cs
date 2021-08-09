using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ontap01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {
           
          
            
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-GBBCS2R;Initial Catalog=SinhVien;Integrated Security=True");
        private void ketnoicsdl()
        {
            con.Open();
            string sql = "select * from SV";  // lay het du lieu trong bang sinh vien
            SqlCommand com = new SqlCommand(sql, con); //bat dau truy van
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com); //chuyen du lieu ve
            DataTable dt = new DataTable(); //tạo một kho ảo để lưu trữ dữ liệu
            da.Fill(dt);  // đổ dữ liệu vào kho
            con.Close();  // đóng kết nối
            dataGridView1.DataSource = dt; //đổ dữ liệu vào datagridview
        }
        void add() {//nhận dữ liệu từ btn 01

            int gender = 0;
            if (rbtnMale.Checked == true)
                gender = 0;
            else if (rbtnFemale.Checked == true)
                gender = 1;
            string sqlInsert = "INSERT INTO SV(Name ,Sex, Class, FinalGrade, FinalDate, Rank, Achievements) values (@Name,@Sex,@Class,@FinalGrade, @FinalDate,@Rank, @Achievements) ";
            SqlCommand com = new SqlCommand(sqlInsert, con);
            com.Parameters.AddWithValue("@Name", tbName.Text);// dữ liệu 01
            com.Parameters.AddWithValue("@Sex", gender);
            com.Parameters.AddWithValue("@Class", tbClass.Text);
            com.Parameters.AddWithValue("@FinalGrade", numericUpDown1.Text);
            com.Parameters.AddWithValue("@FinalDate", Convert.ToDateTime(dtpFinalDate));
            com.Parameters.AddWithValue("@Rank", cbRank.Text);
            com.Parameters.AddWithValue("@Achievements",listBox1.Text);
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //validation ở đây 
            if (tbName.Text == string.Empty)
                MessageBox.Show("TextBox is Empty");
            if (tbClass.Text == string.Empty)
                MessageBox.Show("TextBox is Empty");
            if (rbtnMale.Checked == false && rbtnFemale.Checked == false)
                MessageBox.Show("Choose Male or Female");
            if (numericUpDown1.Value < 0)
                MessageBox.Show("FinalGrade must be >=0");
            if (dtpFinalDate.Value >= DateTime.Now)
                MessageBox.Show("FinalDate must be greater than now");
            add();//  truyền dữ liệu đã đươc validate vào trong hàm add
            ketnoicsdl();
        }
    }
}
