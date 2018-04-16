/*
 * 소스 참조 : http://apis.map.daum.net/android/sample/
 *
 * 현재 버전의 예제 소스에서는 사용이 안되고 있는듯...
 */
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;

namespace DaumMap.Android.Sample.Demo.Samples.RuntimePermission
{
    [Activity(Label = "AppPermissionHandlerActivity", Name = "daum.map.permission.AppPermissionHandlerActivity")]
    public class AppPermissionHandlerActivity : Activity
    {
        #region public member fields area
        public static readonly string PERMISSION_STRINGS = "permissions";
        #endregion

        #region override methods area
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            if (savedInstanceState == null)
            {
                HandleIntent(Intent);
            }
        }

        protected override void OnNewIntent(Intent intent)
        {
            HandleIntent(intent);
        }

		protected override void OnDestroy()
		{
            base.OnDestroy();
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
		{
            AppPermissionHelper.Instance.OnPermissionRequest(requestCode, grantResults);
            Finish();
		}
		#endregion

		#region private methods area
		private void HandleIntent(Intent intent)
        {
            string[] permissions = intent.GetStringArrayExtra(PERMISSION_STRINGS);
            ActivityCompat.RequestPermissions(this, permissions, AppPermissionHelper.REQUEST_CODE);
        }
        #endregion
    }
}
