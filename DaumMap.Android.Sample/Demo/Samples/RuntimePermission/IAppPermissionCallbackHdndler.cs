/*
 * 소스 참조 : http://apis.map.daum.net/android/sample/
 *
 * 현재 버전의 예제 소스에서는 사용이 안되고 있는듯...
 */
namespace DaumMap.Android.Sample.Demo.Samples.RuntimePermission
{
    public interface IAppPermissionCallbackHdndler
    {
        void OnPermissionGranted();
        void OnPermissionDenied();
    }
}
