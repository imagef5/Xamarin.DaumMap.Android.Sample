using Android.Content;
using Android.Content.PM;
using Android.Util;

namespace DaumMap.Android.Sample
{
    public class AppSettings
    {
        const string TAG = "AppSettings";
        public const string DaumMapApiKeyName = "com.kakao.sdk.AppKey";

        public static string GetMetadata(Context context, string name)
        {
            try
            {
                var appInfo = context.PackageManager.GetApplicationInfo(context.PackageName, PackageInfoFlags.MetaData);
                if (appInfo.MetaData != null)
                {
                    return appInfo.MetaData.GetString(name);
                }
            }
            catch (PackageManager.NameNotFoundException e)
            {
                Log.Error(TAG, $"can't find it : {e.Message}");
            }

            return null;
        }
    }
}
