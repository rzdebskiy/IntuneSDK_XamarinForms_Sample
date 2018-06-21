using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace IntuneSDKXFSample
{
	public partial class MainPage : ContentPage
	{
        public static string clientId = "<<INSERT CLIENT ID HERE>>";
        public static string authority = "https://login.windows.net/common";
        public static string returnUri = "http://IntuneTest";
        private const string graphResourceUri = "https://graph.windows.net";
        private AuthenticationResult authResult = null;

        public MainPage()
		{
			InitializeComponent();
		}

        private async void btnEnroll_Clicked(object sender, EventArgs e)
        {

            try
            {
                //var auth = DependencyService.Get<IAuthenticator>();
                //authResult = await auth.Authenticate(authority, graphResourceUri, clientId, returnUri);
                //var userUPN = authResult.UserInfo.DisplayableId;
                //await DisplayAlert("Token granted for", userUPN, "Ok", "Cancel");

                var enroller = DependencyService.Get<IEnroll>();
                //enroller.Enroll(userUPN);
                enroller.Enroll(txtUser.Text);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }


        }
    }
}
