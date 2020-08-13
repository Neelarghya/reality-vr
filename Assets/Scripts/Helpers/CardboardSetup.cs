using Google.XR.Cardboard;
using UnityEngine;

namespace Helpers
{
    public class CardboardSetup : MonoBehaviour
    {
        public void Start()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

#if UNITY_ANDROID && !UNITY_EDITOR
            if (!Api.HasDeviceParams())
                Api.ScanDeviceParams();
#endif
        }
        
#if UNITY_ANDROID && !UNITY_EDITOR
        public void Update()
        {
            if (Api.IsGearButtonPressed)
                Api.ScanDeviceParams();

            if (Api.IsCloseButtonPressed)
                Application.Quit();

            if (Api.HasNewDeviceParams())
                Api.ReloadDeviceParams();
        }
#endif
    }
}