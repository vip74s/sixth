using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace sixth
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsn.Text.Trim() == "")
                {
                    labMessage.Text ="用户名不能为空!";
                    txtUsn.Focus();//获取焦点
                    return;
                }
                else if (txtPwd.Text.Trim() == "")
                {
                    labMessage.Text ="密码不能为空!";
                    txtPwd.Focus();
                    return;
                }

                //老师说了：下面这种方法会有通过文本框输入SQL语句来恶意损坏数据库的可能，所以用了参数的形式来获取文本框的值！
                //string sqlStr = "select userName,passWord from user_info where userName='" + txtUsn.Text.Trim() + "'";
                
                string sqlStr = "select userName,passWord from user_info where userName=@userName";
                DataSet ds = new DataSet();
                DBConn.conn.ConnectionString = DBConn.connStr;
                DBConn.conn.Open();
                SqlCommand cmd = new SqlCommand(sqlStr, DBConn.conn);
                cmd.Parameters.Add(new SqlParameter("@userName", SqlDbType.VarChar, 50));//添加参数
                cmd.Parameters["@userName"].Value = txtUsn.Text;//把用户名文本框里的东西给@userName
                SqlDataReader sdr = cmd.ExecuteReader();
                if (!sdr.Read())//因为是通过userName查询数据的，所以如果没有读到这条数据，肯定是用户名不存在
                {
                    labMessage.Text = "用户名不存在！请重新输入";
                    txtUsn.Text = "";//文本框置空
                    txtPwd.Text = "";
                    txtUsn.Focus();
                }
                else if (sdr["passWord"].ToString().Trim() == txtPwd.Text.Trim())
                {
                    labMessage.Text = "恭喜您已成功登录！";
                }
                else
                {
                    labMessage.Text = "密码错误！请重新输入！";
                    txtPwd.Text = "";
                    txtPwd.Focus();
                }
            }
            catch (Exception ex)
            {
                labMessage.Text = "登录异常：" + ex.Message;
                txtUsn.Text = "";
                txtPwd.Text = "";
                txtUsn.Focus();
            }
            finally
            {
                DBConn.conn.Close();//最重要的是要关闭数据库！
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();//既然点了取消就退出吧
        }
    }
}
