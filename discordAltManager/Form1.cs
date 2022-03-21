using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace discordAltManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string filePath { get; set; }
        public static string SelectedToken { get; set; }
        private void dataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedToken = dataGrid.SelectedCells[0].Value.ToString();
        }

        private void loginBTN_Click(object sender, EventArgs e)
        {
            ChromeDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://discord.com/login");
            string loginScript = "function login(token) { \nsetInterval(() => { \ndocument.body.appendChild(document.createElement `iframe`).contentWindow.localStorage.token = `\"${token}\"` \n}, 50); \nsetTimeout(() => { \nlocation.reload(); \n}, 2500); \n} \nlogin(\"" + SelectedToken + "\")";
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            string title = (string)js.ExecuteScript(loginScript);
            
        }

        private void importBTN_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    string[] loginTokens = File.ReadAllLines(filePath);



                    dataGrid.ColumnCount = 1;

                    dataGrid.Columns[0].Name = "loginToken";
                    foreach (string tokens in loginTokens)
                    {
                        dataGrid.Rows.Add(tokens);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGrid.SelectedRows)
            {
                dataGrid.Rows.RemoveAt(row.Index);
            }
        }
    }
}
