using Android.App;
using Android.Content.PM;
using Android.Views;
using Android.OS;
using Android.Content;
using Android.App.Assist;

namespace IntuneSDKXFSample.Droid
{
    [Activity(Label = "IntuneSDKXFSample", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnMAMCreate(Bundle bundle)
        {
            base.OnMAMCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new IntuneSDKXFSample.App());
        }

        protected override void OnMAMActivityResult(int i, Result j, Intent intent)
        {
            base.OnMAMActivityResult(i, j, intent);
        }

        protected override void OnMAMDestroy()
        {
            base.OnMAMDestroy();
        }

        protected override void OnMAMPause()
        {
            base.OnMAMPause();
        }

        protected override void OnMAMResume()
        {
            base.OnMAMResume();
        }

        protected override void OnMAMNewIntent(Intent intent)
        {
            base.OnMAMNewIntent(intent);
        }

        public override void OnMAMPostCreate(Bundle p0)
        {
            base.OnMAMPostCreate(p0);
        }

        public override void OnMAMPostResume()
        {
            base.OnMAMPostResume();
        }

        public override void OnMAMProvideAssistContent(AssistContent p0)
        {

            base.OnMAMProvideAssistContent(p0);
        }

        public override void OnMAMSaveInstanceState(Bundle p0)
        {
            base.OnMAMSaveInstanceState(p0);
        }

        public override void OnMAMStateNotSaved()
        {
            base.OnMAMStateNotSaved();
        }

        public override bool OnMAMPrepareOptionsMenu(IMenu p0)
        {
            return base.OnMAMPrepareOptionsMenu(p0);
        }

        public override bool OnMAMSearchRequested(SearchEvent p0)
        {
            return base.OnMAMSearchRequested(p0);
        }

        public override Android.Net.Uri OnMAMProvideReferrer()
        {

            return base.OnMAMProvideReferrer();
        }
    }
}

