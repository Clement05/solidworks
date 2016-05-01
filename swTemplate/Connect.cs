using System;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swpublished;
using System.Runtime.InteropServices;

namespace swTemplate
{
    /// <summary>
    /// Primary class
    /// </summary>
    public class Connect : ISwAddin
    {
        /// <summary>
        /// Solidworks object
        /// </summary>
        public SldWorks mSWApplication;
        private int mSWCookie;
        /// <summary>
        /// Method called when addin is conencted to SW
        /// </summary>
        /// <param name="ThisSW"></param>
        /// <param name="Cookie"></param>
        /// <returns></returns>
        public bool ConnectToSW(object ThisSW, int Cookie)
        {
            mSWApplication = (SldWorks)ThisSW;
            mSWCookie = Cookie;

            // Set-up add-in call back info
            bool result = mSWApplication.SetAddinCallbackInfo(0, this, Cookie);

            mSWApplication.SendMsgToUser("Hello world");

            return true;
        }
        /// <summary>
        /// Method called when addin is disconnected to SW
        /// </summary>
        /// <returns></returns>
        public bool DisconnectFromSW()
        {
            return true;
        }

        [ComRegisterFunction()]
        private static void ComRegister(Type t)
        {
            string keyPath = String.Format(@"SOFTWARE\SolidWorks\AddIns\{0:b}", t.GUID);

            using (Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(keyPath))
            {
                rk.SetValue(null, 1); // Load at startup
                rk.SetValue("Title", "My SW Template"); // Title
                rk.SetValue("Description", "All your pixels are belong to us"); // Description
            }
        }

        [ComUnregisterFunction()]
        private static void ComUnregister(Type t)
        {
            string keyPath = String.Format(@"SOFTWARE\SolidWorks\AddIns\{0:b}", t.GUID);
            Microsoft.Win32.Registry.LocalMachine.DeleteSubKeyTree(keyPath);
        }

    }
}
