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
		public MainPage()
		{
			InitializeComponent();
		}

        private void btnEnroll_Clicked(object sender, EventArgs e)
        {
            var enroller = DependencyService.Get<IEnroll>();
            enroller.Enroll(txtUser.Text);
        }
    }
}
