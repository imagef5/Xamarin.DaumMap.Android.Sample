/*
 * 소스 참조 : http://apis.map.daum.net/android/sample/
 */
using Android.App;
using Android.OS;
using Android.Support.V4.App;
using Android.Util;
using Android.Widget;
using Net.Daum.MF.Map.Api;
using static Net.Daum.MF.Map.Api.MapView;

namespace DaumMap.Android.Sample.Demo.Samples
{
    [Activity(Label = "EventsDemoActivity", Name = "daum.map.EventsDemoActivity")]
    public class EventsDemoActivity : FragmentActivity
    {
        #region private member fields area
        readonly string LOG_TAG = "EventsDemoActivity";

        MapView mapView;
        TextView mTapTextView;
        TextView mDragTextView;
        TextView mCameraTextView;
        #endregion

        #region override methods area
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.demo_events);
            mapView = FindViewById<MapView>(Resource.Id.daumMapView);
            mapView.MapViewInitialized += OnMapViewInitialized;
            mapView.MapViewCenterPointMoved += OnMapViewCenterPointMoved;
            mapView.MapViewZoomLevelChanged += OnMapViewZoomLevelChanged;
            mapView.MapViewSingleTapped += OnMapViewSingleTapped;
            mapView.MapViewDoubleTapped += OnMapViewDoubleTapped;
            mapView.MapViewLongPressed += OnMapViewLongPressed;
            mapView.MapViewDragStarted += OnMapViewDragStarted;
            mapView.MapViewDragEnded += OnMapViewDragEnded;
            mapView.MapViewMoveFinished += OnMapViewMoveFinished;

            mTapTextView = FindViewById<TextView>(Resource.Id.tap_text);
            mDragTextView = FindViewById<TextView>(Resource.Id.drag_text);
            mCameraTextView = FindViewById<TextView>(Resource.Id.camera_text);
        }
        #endregion

        #region implement event area
        void OnMapViewInitialized(object sender, MapViewInitializedEventArgs e)
        {
            mapView.SetMapCenterPointAndZoomLevel(MapPoint.MapPointWithGeoCoord(33.41, 126.52), 9, true);
            Log.Info(LOG_TAG, "onMapViewInitialized");
        }

        //MapView mapView, MapPoint mapCenterPoint
        void OnMapViewCenterPointMoved(object sender, MapViewCenterPointMovedEventArgs e)
        {
            var mapView = e.P0;
            var mapCenterPoint = e.P1;
            var mapPointGeo = mapCenterPoint.MapPointGeoCoord;
            mCameraTextView.SetText($"camera position target= lat/lng: ({mapPointGeo.Latitude},{mapPointGeo.Longitude}), zoomLevel={mapView.ZoomLevel}", TextView.BufferType.Normal);
            Log.Info(LOG_TAG, $"MapView onMapViewCenterPointMoved ({mapPointGeo.Latitude},{mapPointGeo.Longitude})");
        }

        //MapView mapView, int zoomLevel
        void OnMapViewZoomLevelChanged(object sender, MapViewZoomLevelChangedEventArgs e)
        {
            var mapView = e.P0;
            var zoolLevel = e.P1;
            var mapPointGeo = mapView.MapCenterPoint.MapPointGeoCoord;
            mCameraTextView.SetText($"camera position target= lat/lng: ({mapPointGeo.Latitude},{mapPointGeo.Longitude}), zoomLevel={mapView.ZoomLevel}", TextView.BufferType.Normal);
            Log.Info(LOG_TAG, $"MapView onMapViewZoomLevelChanged ({zoolLevel})");
        }

        //MapView mapView, MapPoint mapPoint
        void OnMapViewSingleTapped(object sender, MapViewSingleTappedEventArgs e)
        {
            var mapPoint = e.P1;
            var mapPointGeo = mapPoint.MapPointGeoCoord;
            var mapPointScreenLocation = mapPoint.MapPointScreenLocation;
            mTapTextView.SetText($"single tapped, point= lat/lng: ({mapPointGeo.Latitude},{mapPointGeo.Longitude}) x/y: ({mapPointScreenLocation.X},{mapPointScreenLocation.Y})", TextView.BufferType.Normal);
            Log.Info(LOG_TAG, $"MapView OnMapViewSingleTapped ({mapPointGeo.Latitude},{mapPointGeo.Longitude})");
        }

        //MapView mapView, MapPoint mapPoint
        void OnMapViewDoubleTapped(object sender, MapViewDoubleTappedEventArgs e)
        {
            var mapPoint = e.P1;
            var mapPointGeo = mapPoint.MapPointGeoCoord;
            var mapPointScreenLocation = mapPoint.MapPointScreenLocation;
            mTapTextView.SetText($"double tapped, point= lat/lng: ({mapPointGeo.Latitude},{mapPointGeo.Longitude}) x/y: ({mapPointScreenLocation.X},{mapPointScreenLocation.Y})", TextView.BufferType.Normal);
            Log.Info(LOG_TAG, $"MapView OnMapViewDoubleTapped ({mapPointGeo.Latitude},{mapPointGeo.Longitude})");
        }

        //MapView mapView, MapPoint mapPoint
        void OnMapViewLongPressed(object sender, MapViewLongPressedEventArgs e)
        {
            var mapPoint = e.P1;
            var mapPointGeo = mapPoint.MapPointGeoCoord;
            var mapPointScreenLocation = mapPoint.MapPointScreenLocation;
            mTapTextView.SetText($"long pressed, point= lat/lng: ({mapPointGeo.Latitude},{mapPointGeo.Longitude}) x/y: ({mapPointScreenLocation.X},{mapPointScreenLocation.Y})", TextView.BufferType.Normal);
            Log.Info(LOG_TAG, $"MapView OnMapViewLongPressed ({mapPointGeo.Latitude},{mapPointGeo.Longitude})");
        }

        //MapView mapView, MapPoint mapPoint
        void OnMapViewDragStarted(object sender, MapViewDragStartedEventArgs e)
        {
            var mapPoint = e.P1;
            var mapPointGeo = mapPoint.MapPointGeoCoord;
            var mapPointScreenLocation = mapPoint.MapPointScreenLocation;
            mDragTextView.SetText($"drag started, point= lat/lng: ({mapPointGeo.Latitude},{mapPointGeo.Longitude}) x/y: ({mapPointScreenLocation.X},{mapPointScreenLocation.Y})", TextView.BufferType.Normal);
            Log.Info(LOG_TAG, $"MapView OnMapViewDragStarted ({mapPointGeo.Latitude},{mapPointGeo.Longitude})");
        }

        //MapView mapView, MapPoint mapPoint
        void OnMapViewDragEnded(object sender, MapViewDragEndedEventArgs e)
        {
            var mapPoint = e.P1;
            var mapPointGeo = mapPoint.MapPointGeoCoord;
            var mapPointScreenLocation = mapPoint.MapPointScreenLocation;
            mDragTextView.SetText($"drag ended, point= lat/lng: ({mapPointGeo.Latitude},{mapPointGeo.Longitude}) x/y: ({mapPointScreenLocation.X},{mapPointScreenLocation.Y})", TextView.BufferType.Normal);
            Log.Info(LOG_TAG, $"MapView OnMapViewDragEnded ({mapPointGeo.Latitude},{mapPointGeo.Longitude})");
        }

        //MapView mapView, MapPoint mapPoint
        void OnMapViewMoveFinished(object sender, MapViewMoveFinishedEventArgs e)
        {
            var mapPoint = e.P1;
            var mapPointGeo = mapPoint.MapPointGeoCoord;
            Toast.MakeText(BaseContext, "MapView move finished", ToastLength.Short).Show();
            Log.Info(LOG_TAG, $"MapView onMapViewMoveFinished ({mapPointGeo.Latitude},{mapPointGeo.Longitude})");
        }
        #endregion
    }
}
