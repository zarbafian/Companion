using System;

using Xamarin.Forms;

namespace Companion
{
    public class DemandsPage : ContentPage
    {
        public DemandsPage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

