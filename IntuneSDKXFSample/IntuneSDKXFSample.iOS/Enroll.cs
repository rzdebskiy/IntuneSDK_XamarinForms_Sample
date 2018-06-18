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
            Debug.WriteLine("Enrolling");
            IntuneMAMEnrollmentManager.Instance.EnrollmentRequestWithStatus += Instance_EnrollmentRequestWithStatus;
            IntuneMAMEnrollmentManager.Instance.RegisterAndEnrollAccount(UPN);
            Debug.WriteLine("Enrolled");
        }

        private void Instance_EnrollmentRequestWithStatus(object sender, EventArgs e)
        {
            // Debug.WriteLine(e.ToString());
            ;
        }
    }
}