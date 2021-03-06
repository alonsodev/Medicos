﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medicos
{
    using Xamarin.Forms;
    using Medicos.Views;
    public partial class App : Application
	{
        #region Constructors
        public App()
        {
            InitializeComponent();
            
            NavigationPage np = new NavigationPage(new LoginPage());
            np.BarBackgroundColor = Color.FromRgb(18, 56, 92);
            MainPage = np;
            
        }
        #endregion

        #region Methods
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        #endregion
    }
}
