# Xamarin Project with Daum Map Android SDK 

## SDK API 문서 및 다운 로드
- SDK 다운로드및 API 신청 : [문서 참조및 API 신청][0]
- Sample 다운로드 : [다운로드][1] 

## 프로젝트 구성 
- Library : Xamarin.Android Binding Library 용 **libDaumMapAndroid.jar** Convert 프로젝트
- App : Xamarin Android 용 다음 지도 Sample 

## Native Binding 프로젝트
1. [Android jar 바인딩 참조][2] 
2. Native Binding 프로젝트 생성
    - New Solution -> Android 라이브러리 -> 바인딩 라이브러리 생성
3. 바인딩 프로젝트 -> Jars 폴더 우측마우스 클릭 -> libDaumMapAndroid.jar 참조<br/>
    - [다음지도 가이드][1]에 설명중 라이브러리 추가 파일 -libMapEngineApi.so- 을 등록해 줌<br/>
    - [Using Native Libraries][3] 설명중 첫번째 방법인 Path "sniffing" 을 통해 등록하였음 <br/>
    _`x86 에뮬에서 동작하지 않음.`_

![xamarin android aar](https://dongsasubstorage.blob.core.windows.net/images/uploads/daum_map_android_setting_0.png)
![xamarin android aar setting](https://dongsasubstorage.blob.core.windows.net/images/uploads/daum_map_android_setting_1.png)

4. [Java Binding Metadata][5]
- 라이브러리 추가 후 Metadata.xml 파일 수정 없이 빌드하게 되면 error 내용을 확인 할 수 있고, /obj/Debug/generated/src 폴더에 자동으로 generated 된 C# 파일을 확인 할 수 있다. error 내용과 generated 된 C# 파일을 참조하여 Metadata.xml 파일 수정.
- Transforms 폴더 -> Metadata.xml 파일 내용수정
  ```
    <?xml version="1.0" encoding="UTF-8"?>
    <metadata>
        <!--
        이벤트명이 동일한 이름으로 중복 생성 되어 빌드시 error 발생 -> 이벤트명 변경
        -->
        <attr path="/api/package[@name='net.daum.mf.map.api']/interface[@name='MapView.POIItemEventListener']/
                method[@name='onCalloutBalloonOfPOIItemTouched' and count(parameter)=3 
                and parameter[1][@type='net.daum.mf.map.api.MapView'] and parameter[2][@type='net.daum.mf.map.api.MapPOIItem']
                and parameter[3][@type='net.daum.mf.map.api.MapPOIItem.CalloutBalloonButtonType']]"
                name="managedName">OnCalloutBalloonOfPOIItemWithButtonTypeTouched</attr>
        <!-- 
            MapView.MapTileMode : change property name  
            동일한 Property 명과 enum 명이 동일한 클래스에 존재하여 
            C# 형태의 getter/setter 의 역활을 하는 Property 로 설정이 되지 않고
            GetMethod(), SetMethod() 의 형태로 표현이 되는것을 변경하고자 propertyName을 변경

            아래 내용은 설정해 주지 않아도 상관없음(C# 스타일로 변경하고자 설정)
        -->
        <attr path="/api/package[@name='net.daum.mf.map.api']/class[@name='MapView']/
                method[@name='setMapTileMode' and count(parameter)=1 
                and parameter[1][@type='net.daum.mf.map.api.MapView.MapTileMode']]"
                name="propertyName">TileMode</attr>
        <attr path="/api/package[@name='net.daum.mf.map.api']/class[@name='MapView']/
                method[@name='getMapTileMode' and count(parameter)=0]"
                name="propertyName">TileMode</attr>
        <!-- MapView.MapType : change property name  -->
        <attr path="/api/package[@name='net.daum.mf.map.api']/class[@name='MapView']/
                method[@name='setMapType' and count(parameter)=1 
                and parameter[1][@type='net.daum.mf.map.api.MapView.MapType']]"
                name="propertyName">Maptype</attr>
        <attr path="/api/package[@name='net.daum.mf.map.api']/class[@name='MapView']/
                method[@name='getMapType' and count(parameter)=0]"
                name="propertyName">Maptype</attr>
        <!-- MapPOIItem.MarkerType : change property name  -->
        <attr path="/api/package[@name='net.daum.mf.map.api']/class[@name='MapPOIItem']/
                method[@name='setMarkerType' and count(parameter)=1 
                and parameter[1][@type='net.daum.mf.map.api.MapPOIItem.MarkerType']]"
                name="propertyName">POIItemMarkerType</attr>
        <attr path="/api/package[@name='net.daum.mf.map.api']/class[@name='MapPOIItem']/
                method[@name='getMarkerType' and count(parameter)=0]"
                name="propertyName">POIItemMarkerType</attr>
        <!-- MapPOIItem.ShowAnimationType : change property name  -->
        <attr path="/api/package[@name='net.daum.mf.map.api']/class[@name='MapPOIItem']/
                method[@name='setShowAnimationType' and count(parameter)=1 
                and parameter[1][@type='net.daum.mf.map.api.MapPOIItem.ShowAnimationType']]"
                name="propertyName">AnimationType</attr>
        <attr path="/api/package[@name='net.daum.mf.map.api']/class[@name='MapPOIItem']/
                method[@name='getShowAnimationType' and count(parameter)=0]"
                name="propertyName">AnimationType</attr>
    </metadata>
  ```


5. 프로젝트 빌드 
6. Andorid 용 App 프로젝트 생성 ->  Native Binding 프로젝트 참조 
 
 - 다음 API 신청시 네이티브 앱 키 발급 및 **_`*키 해시 등록`_** 하여야 제대로 동작함. <br/>
    [다음지도 가이드][0], [안드로이드 앱 서명][7] 참조
 
   - 프로젝트 -> Properties 폴더 -> AndroidManifest.xml -> 패키지이름 <br/>
   - AndroidManifest.xml 에 Permission 과 APP KEY 추가
  ```
<manifest xmlns:android="http://schemas.android.com/apk/res/android" android:versionCode="1" android:versionName="1.0" package="com.companyname.DaumMapAndroidSample">
    <uses-permission android:name="android.permission.INTERNET">
    </uses-permission>
    <uses-permission android:name="android.permission.ACCESS_FINE_LOCATION">
    </uses-permission>
    <application>
	    ...

		<meta-data android:name="com.kakao.sdk.AppKey" 
                   android:value="YOUR_KAKAO_APPKEY" />
	</application>
</manifest>
  ```

  ``` 
  코드 예제

  지도화면 보기 예:
 using Net.Daum.MF.Map.Api;
 using static Net.Daum.MF.Map.Api.MapView;

  ...
            [Activity(Label = "MapViewDemoActivity", Name = "daum.map.MapViewDemoActivity")]
            public class MapViewDemoActivity : FragmentActivity
            {
                ...
                MapView mMapView;

                protected override void OnCreate(Bundle savedInstanceState)
                {
                    base.OnCreate(savedInstanceState);

                    // Create your application here
                    SetContentView(Resource.Layout.MapView);

                    MapLayout mapLayout = new MapLayout(this);
                    mMapView = mapLayout.MapView;

                    //android 의 Listerner 방식 or .Net 의 Event 방식으로 등록 가능
                    //mMapView.SetOpenAPIKeyAuthenticationResultListener(this);
                    mMapView.OpenAPIKeyAuthenticationResult += OnOpenAPIKeyAuthenticationResult;
                    //mMapView.SetMapViewEventListener(this);
                    mMapView.MapViewInitialized += OnMapViewInitialized;
                    mMapView.MapViewCenterPointMoved += OnMapViewCenterPointMoved;
                    mMapView.MapViewDoubleTapped += OnMapViewDoubleTapped;
                    mMapView.MapViewLongPressed += OnMapViewLongPressed;
                    mMapView.MapViewSingleTapped += OnMapViewSingleTapped;
                    mMapView.MapViewDragStarted += OnMapViewDragStarted;
                    mMapView.MapViewDragEnded += OnMapViewDragEnded;
                    mMapView.MapViewMoveFinished += OnMapViewMoveFinished;
                    mMapView.MapViewZoomLevelChanged += OnMapViewZoomLevelChanged;
                    mMapView.Maptype = MapType.Standard;

                    var mapViewContainer = FindViewById<ViewGroup>(Resource.Id.map_view);
                    mapViewContainer.AddView(mapLayout);

                }
            }
  
  ```
  
## Screenshot
![스크린샷1](https://dongsasubstorage.blob.core.windows.net/images/uploads/daum_map_android_0.png)
![스크린샷2](https://dongsasubstorage.blob.core.windows.net/images/uploads/daum_map_android_hybrid.png)
![스크린샷3](https://dongsasubstorage.blob.core.windows.net/images/uploads/daum_map_android_marker.png)
![스크린샷4](https://dongsasubstorage.blob.core.windows.net/images/uploads/daum_map_android_polyline.png)
![스크린샷5](https://dongsasubstorage.blob.core.windows.net/images/uploads/daum_map_android_circle.png)
![스크린샷6](https://dongsasubstorage.blob.core.windows.net/images/uploads/daum_map_android_location.png)
![스크린샷7](https://dongsasubstorage.blob.core.windows.net/images/uploads/daum_map_android_camera.png)
![스크린샷8](https://dongsasubstorage.blob.core.windows.net/images/uploads/daum_map_android_event.png)

## Reference
* [Binding a Java Library][4]
* [Customizing Bindings][5]
* [Java Bindings Metadata][5]
* [Troubleshooting Bindings][6]

[0]:http://apis.map.daum.net/android/guide/
[1]:http://apis.map.daum.net/android/sample/
[2]:https://docs.microsoft.com/en-us/xamarin/android/platform/binding-java-library/binding-a-jar
[3]:https://docs.microsoft.com/en-us/xamarin/android/platform/native-libraries
[3]:https://docs.microsoft.com/en-us/xamarin/android/platform/binding-java-library/
[4]:https://docs.microsoft.com/en-us/xamarin/android/platform/binding-java-library/customizing-bindings/
[5]:https://docs.microsoft.com/en-us/xamarin/android/platform/binding-java-library/customizing-bindings/java-bindings-metadata
[6]:https://docs.microsoft.com/en-us/xamarin/android/platform/binding-java-library/troubleshooting-bindings
[7]:https://developer.android.com/studio/publish/app-signing.html?hl=ko
