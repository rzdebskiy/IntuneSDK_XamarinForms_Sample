using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
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
                //IntuneMAMEnrollmentManager.Instance.EnrollmentRequestWithStatus += Instance_EnrollmentRequestWithStatus;
                //IntuneMAMEnrollmentManager.Instance.RegisterAndEnrollAccount(UPN);
                IntuneMAMEnrollmentManager.Instance.LoginAndEnrollAccount(UPN);
                Debug.WriteLine("Enroll completed without exceptions");
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Enrollment exceptions: {e.ToString()}");
            }
        }

        //private void Instance_EnrollmentRequestWithStatus(object sender, EventArgs e)
        //{
        //    // Debug.WriteLine(e.ToString());
        //    ;
        //}
    }
}