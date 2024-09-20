using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;
using System.Text.Json;

namespace OOP_AS2_VincentLloyd_Opiana
{
    public partial class Form1 : Form
    {
        private User userData;
        private string path;
        public Form1()
        {
            InitializeComponent();
            userData = new User();
            path = userData.filePath;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            userData.LoadUsersFromJson(path);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            if (userData.IsValid(username, password))
            {
                label3.Text = "Login successful!";
                label3.ForeColor = System.Drawing.Color.Blue;
            }
            else
            {
                label3.Text = "Invalid username or password.";
                label3.ForeColor = System.Drawing.Color.Red;
            }
            label3.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string username = textBox3.Text;
            string password = textBox4.Text;
            try
            {
                userData.AddUser(username, password, path);
                label6.Visible = true;
                label6.Text = "Account Created Successfully";
            }
            catch
            {
                label6.Text = "Account Already Exist";
                label6.Visible = true;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }
    }
}

2.
using System;

public class Identity
{
    public string Username { get; set; }
    public string Password { get; set; }

    public Identity(string username, string password)
    {
        Username = username;
        Password = password;
    }
}

1.
using System;
using System.Text.Json;

public class User
{
    private List<Identity> users = new List<Identity>();
    public string filePath = "user.json";
    public void LoadUsersFromJson(string filePath)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            users = JsonSerializer.Deserialize<List<Identity>>(json);
        }
        else
        {
            users.Add(new Identity("admin", "password"));
            SaveUsersToJson(filePath);
        }
    }

    public void SaveUsersToJson(string filePath)
    {
        string json = JsonSerializer.Serialize(users);
        File.WriteAllText(filePath, json);
    }

    public bool IsValid(string username, string password)
    {
        foreach (var user in users)
        {
            if (user.Username == username && user.Password == password)
            {
                return true;
            }
        }
        return false;
    }

    public void AddUser(string username, string password, string filepath)
    {
        foreach (var user in users)
        {
            if (user.Username == username)
            {
                throw new InvalidOperationException("User already exists.");
            }
        }
        users.Add(new Identity(username, password));
        SaveUsersToJson(filepath);
    }


}

2.
< Project Sdk = "Microsoft.NET.Sdk" >

  < PropertyGroup >
    < OutputType > WinExe </ OutputType >
    < TargetFramework > net8.0 - windows </ TargetFramework >
    < Nullable > enable </ Nullable >
    < UseWindowsForms > true </ UseWindowsForms >
    < ImplicitUsings > enable </ ImplicitUsings >
  </ PropertyGroup >

</ Project >


3.
[{ "Username":"admin","Password":"password"},{ "Username":"Jeb","Password":"6789"},{ "Username":"Test101","Password":"Ley"},{ "Username":"yes","Password":"678"},{ "Username":"admin1","Password":"password"}]
}
