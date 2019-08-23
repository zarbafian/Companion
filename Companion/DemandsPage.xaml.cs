using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using Companion.Models;

namespace Companion
{
    public partial class DemandsPage : ContentPage
    {
        public DemandsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var demands = new List<Demand>();

            var files = Directory.EnumerateFiles(App.FolderPath, "*.demand.txt");
            foreach (var filename in files)
            {
                string[] parts = File.ReadAllText(filename).Split('_');
                demands.Add(new Demand
                {
                    Filename = filename,
                    Title = parts[0],
                    Content = parts[1],
                    Date = File.GetCreationTime(filename)
                });
            }

            listView.ItemsSource = demands
                .OrderBy(d => d.Date)
                .ToList();
        }

        async void OnDemandAddedClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DemandEntryPage
            {
                BindingContext = new Demand()
            });
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new DemandEntryPage
                {
                    BindingContext = e.SelectedItem as Demand
                });
            }
        }
    }
}