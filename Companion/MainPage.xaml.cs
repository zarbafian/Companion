using System;
using System.IO;
using Xamarin.Forms;

namespace Companion
{
    public partial class MainPage : ContentPage
    {
        string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "notes.txt");

        public MainPage()
        {
            InitializeComponent();

            if (File.Exists(_fileName))
            {
                string titleAndContent = File.ReadAllText(_fileName);
                string[] parts = titleAndContent.Split('\n');
                titleEditor.Text = parts[0];
                contentEditor.Text = parts[1];
            }
        }

        void OnSaveButtonClicked(object sender, EventArgs e)
        {
            File.WriteAllText(_fileName, titleEditor.Text + "\n" + contentEditor.Text);
        }

        void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            if (File.Exists(_fileName))
            {
                File.Delete(_fileName);
            }
            titleEditor.Text = string.Empty;
            contentEditor.Text = string.Empty;
        }
    }
}