﻿# Xamarin Forms Intune SDK Sample and Step by Step Guide

**Important** - here are the links to the official sample apps from Microsoft Intune’s product team:
* [Xamarin.Android, Xamarin.Forms](https://github.com/msintuneappsdk/Taskr-Sample-Intune-Xamarin-Android-Apps)
* [Xamarin.iOS](https://github.com/msintuneappsdk/sample-intune-xamarin-ios) 

This sample and guide shows how to :
* integrate Intune app protection policies support into Xamarin Forms app
* publish app with Microsoft Intune 
* enable clipboard and screenshot (Android) protection for your mobile app in Intune
* enjoy as a result that no one can copy text from your app or take screenshot :-)

1. Visual Studio -> New Project -> Cross-Platform -> Xamarin Forms 
2. .NET Standard as code sharing strategy 
3. Let's add couple of controls to *MainPage.xaml* to test screenshot and clipboard protection:
```xml
    <StackLayout Orientation="Vertical" HorizontalOptions="Center" VerticalOptions="Center">
        <!-- Place new controls here -->
        <Label Text="Confidential Information!" 
           HorizontalOptions="CenterAndExpand"
           VerticalOptions="Center" />
        <Entry x:Name="txtEntry" Text="Very Sensitive Data" />
    </StackLayout>
```

## Android Implementation

4. Go to Android project - > Manage NuGet packages
5. Ensure you have at minimum these versions of Android support packages referenced:
   * Xamarin.Android.Support.Compat (>= 26.1.0.1)
   * Xamarin.Android.Support.v7.AppCompat (>= 26.1.0.1)

![Android Support](Images/AndroidSupport.jpg)

5. Add *Microsoft.Intune.MAM.Xamarin.Android* NuGet package to Android project first.
6. Then add *Microsoft.Intune.MAM.Remapper.Tasks* NuGet package to Android project. This remapper will replace standard classes with MAM (Mobile Application Management) classes which supports Intune policies enforcement and management. These MAM-classes will be injected into hierarchy of commonly used Xamarin.Forms classes like *FormsAppCompatActivity* and *FormsApplicationActivity*. Now before building solution you need to create *MAMApplication* class and override MAM implementation of *OnMAMCreate* function. Also for a lot of cases you will need to use MAM equivalents of number of commonly used functions - [full guide here](https://docs.microsoft.com/en-us/intune/app-sdk-android#mamapplication)
7. Create *MAMApplication* class in your Android Project:
```csharp
using System;
using Android.App;
using Android.Runtime;
using Microsoft.Intune.Mam.Client.App;

namespace IntuneSDKXFSample.Droid
{
    [Application]
    public class IntuneMAMFormsSampleApplication : MAMApplication
    {
        /// <summary>
        /// This is necessary because of a leaky abstraction somewhere up the chain:
        /// http://stackoverflow.com/questions/10593022/monodroid-error-when-calling-constructor-of-custom-view-twodscrollview/10603714#10603714
        /// </summary>
        /// <param name="handle">Java reference</param>
        /// <param name="transfer">Ownership transfer</param>
        public IntuneMAMFormsSampleApplication(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer)
        {
        }

        /// <summary>
        /// New abstract method that we need to override
        /// Documentation at https://microsoft.sharepoint.com/teams/Android_SSP/_layouts/15/WopiFrame2.aspx?sourcedoc=%7b56C60010-40D5-4487-BC70-21471C50D1DD%7d&file=Walled%20Garden%20API%20Guide.docx&action=default says:
        /// If your application does not call AuthenticationSettings.setSecretKey (or does not integrate ADAL at all), you may simply return null. 
        /// </summary>
        /// <returns>The ADAL key</returns>
        public override byte[] GetADALSecretKey()
        {
            return null;
        }
    }
}
```

9. Replace *OnCreate(Bundle bundle)* and other methods in *MainActivity.cs* with the following overrides:
```csharp
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
```
9. Reference two namespaces to fix unresolved methods :
```csharp
using Android.Content;
using Android.App.Assist;
```
10. Build the project to ensure everything compiles successfully. 
11. [Publish the app](https://docs.microsoft.com/en-us/intune/lob-apps-android) with Microsoft Intune with [app protection policy](https://docs.microsoft.com/en-us/intune/app-protection-policies) for [screenshot and clipboard applied](https://docs.microsoft.com/en-us/intune/app-protection-policy-settings-android). 
12. Install the app to the target device through Company Portal App, run it.
13. Check that you are not able to copy text from the app to the other unprotected app (for instance built-in notes apps).

## iOS Implementation

4. Go to iOS project - > Manage NuGet packages
5. Add *Microsoft.Intune.MAM.Xamarin.iOS* NuGet package.
6. Enable keychain sharing in iOS project - Open *Entitlements.plist* file, go to "Keychain" section and check "Enable Keychain" option. 
7. Add 3 keychain groups here with *$(AppIdentifierPrefix)* prefix (if you use XCode it adds prefix automatically):
    1. first is your bundle id - same as specified in *Info.plist* file:
![Info.plist](Images/InfoPlist.jpg)
    2. second is *com.microsoft.intune.mam*
    3. third is *com.microsoft.adalcache*

![Entitlements.plist](Images/Entitlements.jpg)

8. Go to iOS project properties and ensure this *Entitlements.plist* is specified in the "Custom Entitlements" field of the project's "iOS Bundle Signing" options for all the appropriate Configuration/Platform combinations:

![Bundle Signing](Images/BundleSigning.jpg) 

9. To begin receiving app protection policies, we need to explicitly enroll in the Intune MAM service by calling *LoginAndEnrollAccount* and provide user alias(email) as parameter:

```csharp
IntuneMAMEnrollmentManager.Instance.LoginAndEnrollAccount(string UPN)
````
>**Note**: In the [documentation](https://docs.microsoft.com/en-us/intune/app-sdk-xamarin) you can see the recomendation to use the different method of *IntuneMAMEnrollmentManager.Instance.**RegisterAndEnrollAccount**(string identity)* in case your app already uses the Azure Active Directory Authentication Library ([ADAL](https://docs.microsoft.com/en-us/azure/active-directory/develop/active-directory-authentication-libraries)) to authenticate users. Currently this implementation has a limitation - if you use ADAL for .NET it doesn't support token sharing with native Objective-C ADAL library (which Intune iOS SDK actually uses). So *RegisterAndEnrollAccount* just doesn't work in Xamarin Forms - when you call it no exception is thrown but nothing happens and app protection policy doesn't work. Until this issue is resolved and ADAL has a shared cache across ADAL for .NET and native Objective-C you have two options:
>* use *LoginAndEnrollAccount* as advised above. If  you are only using ADAL to authenticate users and your app doesn’t  need to access Azure Active Directory resources, this is the simplest solution
>* [ create Xamarin bindings](https://github.com/Azure-Samples/active-directory-xamarin-ios/tree/archive) for version [2.5.4 of ADAL for Objective-C](https://github.com/AzureAD/azure-activedirectory-library-for-objc/releases/tag/2.5.4) and use them to acquire an access token and call IntuneMAMEnrollmentManager.Instance.RegisterAndEnrollAccount(string identity);

> **Additional Note**: there is no remapper for iOS. Integrating into a Xamarin.Forms app should be the same as for a regular Xamarin.iOS project.

10. To do this platform-specific call we use Dependency Service in Xamarin Forms. Define IEnroll interface in common project:
```csharp
public interface IEnroll
{
    void Enroll(string UPN);
}
````
11. Provide iOS specific implementation in iOS project with *Dependency* assembly attribute (first line after *using* statements):
```csharp
using System;
using Xamarin.Forms;
using Microsoft.Intune.MAM;
using System.Diagnostics;

[assembly: Dependency(typeof(IntuneSDKXFSample.iOS.Enroll))]
namespace IntuneSDKXFSample.iOS
{
    class Enroll : IEnroll
    {
        void IEnroll.Enroll(string UPN)
        {
                IntuneMAMEnrollmentManager.Instance.LoginAndEnrollAccount(UPN);
        }
    }
}
````
12. From the common project call Enroll method with user alias as parameter:
```csharp
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
````
13. Build the project to ensure everything compiles successfully.
14. [Publish the app](https://docs.microsoft.com/en-us/intune/lob-apps-ios) with Microsoft Intune with [app protection policy](https://docs.microsoft.com/en-us/intune/app-protection-policies) for [clipboard applied](https://docs.microsoft.com/en-us/intune/app-protection-policy-settings-ios).
15. Install the app to the target device through Company Portal App, run it.
16. Check that you are not able to copy text from the app to the other unprotected app (for instance built-in notes apps).

Full guide is [here.](https://docs.microsoft.com/en-us/intune/app-sdk-xamarin)



