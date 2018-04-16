/*
 * 소스 참조 : http://apis.map.daum.net/android/sample/
 *
 * 현재 버전의 예제 소스에서는 사용이 안되고 있는듯...
 */
using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Util;

namespace DaumMap.Android.Sample.Demo.Samples.RuntimePermission
{
    public class AppPermissionHelper
    {
        #region private member fields area
        public static readonly string TAG = "AppPermissionHelper";
        public static readonly int REQUEST_CODE = 0xbeaf;
        static readonly bool useHandlerActivity = true;

        IAppPermissionCallbackHdndler callbackHandler;
        static Lazy<AppPermissionHelper> appHelper = new Lazy<AppPermissionHelper>();
        #endregion

        private AppPermissionHelper()
        {
        }

        #region property area
        public static AppPermissionHelper Instance
        {
            get
            {
                return appHelper.Value;
            }
        }
        #endregion

        #region public methods area
        public void CheckAndRequestPermisson(Activity activity, string permission, IAppPermissionCallbackHdndler handler)
        {
            CheckAndRequestPermissons(activity, new string[] { permission }, handler);
        }

        public void CheckAndRequestPermissons(Activity activity, string[] permissions, IAppPermissionCallbackHdndler handler)
        {
            if (activity == null) return;
            if (permissions == null) return;

            if (CheckForPermissions(activity, permissions))
            {
                if (handler != null)
                {
                    handler.OnPermissionGranted();
                }
            }
            else
            {
                RequestPermissions(activity, permissions, handler);
            }
        }

        public bool CheckForPermission(Context context, string permission)
        {
            return ContextCompat.CheckSelfPermission(context, permission) == Permission.Granted;
        }

        public bool CheckForPermissions(Context context, string[] permissions)
        {
            if (context == null) return false;
            if (permissions == null) return false;
            if (permissions.Length <= 0) return false;

            bool hasPermissions = false;
            foreach (var permission in permissions)
            {
                hasPermissions = CheckForPermission(context, permission);
                if (hasPermissions == false)
                {
                    return false;
                }
            }
            return hasPermissions;
        }

        public void RequestPermission(Activity activity, string permission, IAppPermissionCallbackHdndler handler)
        {
            RequestPermissions(activity, new String[] { permission }, handler);
        }

        public void RequestPermissions(Activity activity, string[] permissions, IAppPermissionCallbackHdndler handler)
        {

            callbackHandler = handler;

            if (useHandlerActivity)
            {
                StartAppPermissionHandlerActivity(activity, permissions);
            }
            else
            {
                ActivityCompat.RequestPermissions(activity, permissions, REQUEST_CODE);
            }
        }

        public void OnPermissionRequest(int requestCode, Permission[] grantResults)
        {
            if (callbackHandler == null)
            {
                return;
            }

            bool permissionStatus = false;
            for (int i = 0, size = grantResults.Length; i < size; i++)
            {
                permissionStatus = grantResults[i] == Permission.Granted;
                if (permissionStatus == false) break;
            }

            bool isPermissionGranted = permissionStatus;
            Handler handler = new Handler(Looper.MainLooper);
            handler.Post(() => 
            {
                if(isPermissionGranted)
                {
                    callbackHandler.OnPermissionGranted();
                }
                else
                {
                    callbackHandler.OnPermissionDenied();
                }
                callbackHandler = null;
            });
        }
        #endregion

        #region private methods area
        private void StartAppPermissionHandlerActivity(Context ctx, string[] permissions)
        {
            //Log.d(TAG, "startAppPermissionHandlerActivity");
            var cls = typeof(AppPermissionHandlerActivity);

            Intent intent = new Intent(ctx, cls);
            intent.PutExtra(AppPermissionHandlerActivity.PERMISSION_STRINGS, permissions);
            //intent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
            intent.AddFlags(ActivityFlags.SingleTop | ActivityFlags.ReorderToFront | ActivityFlags.NoAnimation);

            try {
                //Log.d(TAG, "startAppPermissionHandlerActivity startActivity");
                ctx.StartActivity(intent);
            } catch (Exception e) {
                Log.Debug(TAG, "Unable to launch AppPermissionHandlerActivity", e);
            }
        }
        #endregion
    }
}
