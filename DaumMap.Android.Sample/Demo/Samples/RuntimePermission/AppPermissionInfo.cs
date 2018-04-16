/*
 * 소스 참조 : http://apis.map.daum.net/android/sample/
 *
 * 현재 버전의 예제 소스에서는 사용이 안되고 있는듯...
 */
using Android;

namespace DaumMap.Android.Sample.Demo.Samples.RuntimePermission
{
    public class AppPermissionInfo
    {
        public static readonly string[] LOCATION_PERMISSIONS = new string[] 
        {
            Manifest.Permission.AccessFineLocation,
            Manifest.Permission.AccessCoarseLocation
        };

        public static readonly string[] VOICE_SEARCH_PERMISSIONS = new string[] 
        {
            Manifest.Permission.RecordAudio,
            Manifest.Permission.AccessFineLocation,
            Manifest.Permission.AccessCoarseLocation
        };
    }
}