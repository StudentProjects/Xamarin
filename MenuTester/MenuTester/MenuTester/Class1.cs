﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace MenuTester
{
	public class HomePage : MasterDetailPage
	{
		public HomePage (bool isAdmin)
		{
			// Set up masterDetailPage
			Label header = new Label {
				Text = "Menu",
				HorizontalOptions = LayoutOptions.Center
			};

			// Have an array of all the pages names
			List<string> NamesOfPages = new List<string> ();
			NamesOfPages.Add ("Google");
			NamesOfPages.Add ("Hitta");

			if (isAdmin) {
				NamesOfPages.Add ("Aftonbladet");
				NamesOfPages.Add ("Tieto");
			}            

			//Create ListView for the master page
			ListView masterList = new ListView {   
				ItemsSource = NamesOfPages,
			};

            ListView optionsList = new ListView
            {   
                ItemsSource = new List<string>() {"Settings","Logout"},
                VerticalOptions = LayoutOptions.End,

            };

			this.Master = new ContentPage {
				Title = "master",
				Content = new StackLayout {
                    Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 5),
					Children = {
						header,
						masterList,
                        optionsList
					},
				}
			};
            WebView myWebView = new WebView {
				        Source = "http://www.google.com"
            };

           	this.Detail = new ContentPage {
				Content = new ContentView {
                    Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0),
	                Content = myWebView
				}
			};
			var myHomePage = new ContentPage ();


			masterList.ItemSelected += (sender, args) => {
				this.Detail.BindingContext = args.SelectedItem;
               
             
				//put go to one of the selected pages
				if (args.SelectedItem.ToString () == "Hitta") {
					myWebView.Source = "http://www.hitta.se";
				} else if (args.SelectedItem.ToString () == "Google") {
					myWebView.Source = "http://www.google.se";
				} else if (args.SelectedItem.ToString () == "Aftonbladet") {
					myWebView.Source = "http://www.aftonbladet.se";
				} else if (args.SelectedItem.ToString () == "Tieto") {
					myWebView.Source = "http://www.tieto.se";
				}
				this.IsPresented = false;

			};

            optionsList.ItemSelected += async (sender, args) =>
            {
                this.Detail.BindingContext = args.SelectedItem;
                //put go to one of the selected pages
                if (args.SelectedItem.ToString() == "Settings")
                {
                    await DisplayAlert("hej", "Här ska det finnas settings", "hejdå");
                }
                else if (args.SelectedItem.ToString() == "Logout")
                {
                    await Navigation.PopModalAsync();
                }
                this.IsPresented = false;
            };
		}
        
	}
}
