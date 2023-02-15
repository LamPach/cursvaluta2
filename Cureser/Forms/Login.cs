using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Cureser.Forms
{
    public partial class Login : Form
    {
        public Login(UserContext userContext)
        {
            InitializeComponent();
            this.userContext = userContext;
        }

        private UserContext userContext;

        private string HashPassword(string password, string salt)
        {
            using (MD5 hash = MD5.Create())
            {
                byte[] input = System.Text.Encoding.ASCII.GetBytes($"{password}{salt}");
                byte[] hashed = hash.ComputeHash(input);

                return Convert.ToBase64String(hashed);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            var query = 
                from u in this.userContext.Users
                where u.Name == this.textBox1.Text
                select u;

            if (query.Count() > 0)
            {
                var user = query.First();

                if (user.Password == HashPassword(this.textBox2.Text, salt))
                {
                    UserData userData = new UserData(user, userContext);
                    Console.WriteLine("Вы вошли");
                    Exchange exchangeWindow = new Exchange(userData);
                    this.Hide();
                    exchangeWindow.Show();
                    exchangeWindow.FormClosed += ((_a,_b) => this.Close());
                }
            } else
            {
                Console.WriteLine("Такого пользователя не существует");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var query =
                from u in this.userContext.Users
                where u.Name == this.textBox1.Text
                select u;

            if (query.Count() > 0) 
            {
                Console.WriteLine("Такой пользователь уже существует");
            } else
            {
                string name = this.textBox1.Text;
                string password = this.textBox2.Text;
                userContext.Users.Add(new UserModel { Name = name, Password = password });
                userContext.SaveChanges();
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
        private string salt = "lopata";
    }
}
