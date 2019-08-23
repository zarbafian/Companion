using System;
using System.IO;
using System.Diagnostics;
using Xamarin.Forms;
using Companion.Models;
using Companion.Service;

namespace Companion
{
    public partial class DemandEntryPage : ContentPage
    {
        DemandService _demandeService;

        public DemandEntryPage()
        {
            InitializeComponent();
            _demandeService = new DemandService();
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var demand = (Demand)BindingContext;

            // save locally
            if (string.IsNullOrWhiteSpace(demand.Filename))
            {
                // Save
                var filename = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.demand.txt");
                File.WriteAllText(filename, demand.Title + "_" + demand.Content);
            }
            else
            {
                // Update
                File.WriteAllText(demand.Filename, demand.Title + "_" + demand.Content);
            }

            // send to server
            UserNotification userNotification = await _demandeService.CreateDemandDataAsync(demand);

            Debug.WriteLine($"Data received from server, id={userNotification.Id}, title={userNotification.Title}, content={userNotification.Content}, status={userNotification.Status}");

            await Navigation.PopAsync();
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var demand = (Demand)BindingContext;

            if (File.Exists(demand.Filename))
            {
                File.Delete(demand.Filename);
            }

            await Navigation.PopAsync();
        }
    }
}