using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using Companion.Models;

namespace Companion
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var pages = new List<PageData>
            {
                new PageData
                {
                    Name = "Guides"
                },

                new PageData
                {
                    Name = "Contact"
                }
            };


            listView.ItemsSource = pages;//.OrderBy(p => p.Name).ToList();
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                PageData p = e.SelectedItem as PageData;
                if("Contact".Equals(p.Name))
                {
                    await Navigation.PushAsync(new DemandsPage());
                }
                else if("Guides".Equals(p.Name))
                {
                    // Not implemented
                }
                /*
                await Navigation.PushAsync(new DemandEntryPage
                {
                    BindingContext = e.SelectedItem as Page
                });
                */
            }
        }
    }
}
