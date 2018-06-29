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
            try
            {
                Debug.WriteLine("Enrolling");
                IntuneMAMEnrollmentManager.Instance.LoginAndEnrollAccount(UPN);
                Debug.WriteLine("Enroll completed without exceptions");
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Enrollment exceptions: {e.ToString()}");
            }
        }
    }
}