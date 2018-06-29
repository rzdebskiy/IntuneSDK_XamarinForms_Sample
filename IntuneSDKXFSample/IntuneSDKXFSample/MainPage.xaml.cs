using System;
using Xamarin.Forms;


namespace IntuneSDKXFSample
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnEnroll_Clicked(object sender, EventArgs e)
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                try
                {
                    var enroller = DependencyService.Get<IEnroll>();
                    enroller.Enroll(txtUser.Text);
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "Ok");
                }
            }
            else await DisplayAlert("Warning", "For Android you don't need to call LoginAndEnrollAccount", "OK");
        }



    }
}

