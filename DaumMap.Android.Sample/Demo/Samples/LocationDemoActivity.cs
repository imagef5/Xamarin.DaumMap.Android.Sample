/*
 * 소스 참조 : http://apis.map.daum.net/android/sample/
 */
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Net.Daum.MF.Map.Api;
using static Net.Daum.MF.Map.Api.MapView;
using static Net.Daum.MF.Map.Api.MapPOIItem;
using static Net.Daum.MF.Map.Api.MapReverseGeoCoder;

namespace DaumMap.Android.Sample.Demo.Samples
{
    [Activity(Label = "LocationDemoActivity", Name = "daum.map.LocationDemoActivity")]
    public class LocationDemoActivity : FragmentActivity, IReverseGeoCodingResultListener
    {
        #region private methods area
        readonly string LOG_TAG = "LocationDemoActivity";
        const int MENU_LOCATION = Menu.First;
        const int MENU_REVERSE_GEO = Menu.First + 1;

        MapView mMapView;
        MapReverseGeoCoder mReverseGeoCoder = null;
        bool isUsingCustomLocationMarker = false;
        #endregion

        #region override methods area
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.demo_nested_mapview);

            mMapView = FindViewById<MapView>(Resource.Id.map_view);
            //mMapView.setCurrentLocationEventListener(this);
            mMapView.CurrentLocationUpdate += OnCurrentLocationUpdate;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            mMapView.SetCurrentLocationTrackingMode(CurrentLocationTrackingMode.TrackingModeOff);
            mMapView.SetShowCurrentLocationMarker(false);
        }

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
            base.OnCreateOptionsMenu(menu);

            menu.Add(0, MENU_LOCATION, Menu.None, "Location");
            menu.Add(0, MENU_REVERSE_GEO, Menu.None, "Reverse Geo-Coding");

            return true;
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
            switch (item.ItemId)
            {
                case MENU_LOCATION:
                    string[] mapMoveMenuItems = 
                    {
                        "User Location On",
                        "User Location On, MapMoving Off",
                        "User Location+Heading On",
                        "User Location+Heading On, MapMoving Off",
                        "User Location+Heading On, MarkerHeading On, MapMoving Off",
                        "Off",
                        (isUsingCustomLocationMarker ? "Default" : "Custom") + " Location Marker",
                        "Show Location Marker",
                        "Hide Location Marker"
                    };
                    var dialog = new AlertDialog.Builder(this);
                    dialog.SetTitle("Location");
                    dialog.SetItems(mapMoveMenuItems,(sender, e) =>   
                    {
           
                        switch (e.Which)
                        {
                            case 0: // User Location On
                                mMapView.SetCurrentLocationTrackingMode(CurrentLocationTrackingMode.TrackingModeOnWithoutHeading);
                                break;
                            case 1: // User Location On, MapMoving Off
                                mMapView.SetCurrentLocationTrackingMode(CurrentLocationTrackingMode.TrackingModeOnWithoutHeadingWithoutMapMoving);
                                break;
                            case 2: // User Location+Heading On
                                mMapView.SetCurrentLocationTrackingMode(CurrentLocationTrackingMode.TrackingModeOnWithHeading);
                                break;
                            case 3: // User Location+Heading On, MapMoving Off
                                mMapView.SetCurrentLocationTrackingMode(CurrentLocationTrackingMode.TrackingModeOnWithHeadingWithoutMapMoving);
                                break;
                            case 4: // "User Location+Heading On, MarkerHeading On, MapMoving Off",
                                mMapView.SetCurrentLocationTrackingMode(CurrentLocationTrackingMode.TrackingModeOnWithMarkerHeadingWithoutMapMoving);
                                mMapView.SetCurrentLocationRadius(0);
                                break;
                            case 5: // Off
                                mMapView.SetCurrentLocationTrackingMode(CurrentLocationTrackingMode.TrackingModeOff);
                                mMapView.SetShowCurrentLocationMarker(false);
                                break;
                            case 6: // Default/Custom Location Marker
                                if (isUsingCustomLocationMarker)
                                {
                                    mMapView.SetCurrentLocationRadius(0);
                                    mMapView.SetDefaultCurrentLocationMarker();
                                }
                                else
                                {
                                    mMapView.SetCurrentLocationRadius(100); // meter
                                    mMapView.SetCurrentLocationRadiusFillColor(Color.Argb(77, 255, 255, 0));
                                    mMapView.SetCurrentLocationRadiusStrokeColor(Color.Argb(77, 255, 165, 0));

                                    var trackingImageAnchorPointOffset = new ImageOffset(28, 28); // 좌하단(0,0) 기준 앵커포인트 오프셋
                                    var directionImageAnchorPointOffset = new ImageOffset(65, 65);
                                    var offImageAnchorPointOffset = new ImageOffset(15, 15);
                                    mMapView.SetCustomCurrentLocationMarkerTrackingImage(Resource.Drawable.custom_arrow_map_present_tracking, trackingImageAnchorPointOffset);
                                    mMapView.SetCustomCurrentLocationMarkerDirectionImage(Resource.Drawable.custom_map_present_direction, directionImageAnchorPointOffset);
                                    mMapView.SetCustomCurrentLocationMarkerImage(Resource.Drawable.custom_map_present, offImageAnchorPointOffset);
                                }
                                isUsingCustomLocationMarker = !isUsingCustomLocationMarker;
                                break;
                            case 7: // Show Location Marker
                                mMapView.SetShowCurrentLocationMarker(true);
                                break;
                            case 8: // Hide Location Marker
                                if (mMapView.IsShowingCurrentLocationMarker)
                                {
                                    mMapView.SetShowCurrentLocationMarker(false);
                                }
                                break;
                        }

                    });
                    dialog.Show();
                    return true;

                case MENU_REVERSE_GEO:
                    var daumMapApikey = AppSettings.GetMetadata(this, AppSettings.DaumMapApiKeyName);
                    mReverseGeoCoder = new MapReverseGeoCoder(daumMapApikey, mMapView.MapCenterPoint, this, this);
                    mReverseGeoCoder.StartFindingAddress();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
		}
		#endregion

		#region implement event area
		//MapView mapView, MapPoint currentLocation, float accuracyInMeters
		void OnCurrentLocationUpdate(object sender, CurrentLocationUpdateEventArgs e)
        {
            var currentLocation = e.P1;
            var accuracyInMeters = e.P2;
            var mapPointGeo = currentLocation.MapPointGeoCoord;
            Log.Info(LOG_TAG, $"MapView onCurrentLocationUpdate ({mapPointGeo.Latitude},{mapPointGeo.Longitude}) accuracy ({accuracyInMeters})");
        }

        //implement IReverseGeoCodingResultListener interface
        public void OnReverseGeoCoderFailedToFindAddress(MapReverseGeoCoder mapReverseGeoCoder)
        {
            onFinishReverseGeoCoding("Fail");
        }

        public void OnReverseGeoCoderFoundAddress(MapReverseGeoCoder mapReverseGeoCoder, string s)
        {
            mapReverseGeoCoder.ToString();
            onFinishReverseGeoCoding(s);
        }

        void onFinishReverseGeoCoding(string result)
        {
            Toast.MakeText(this, $"Reverse Geo-coding : {result}", ToastLength.Short).Show();
        }
        #endregion
    }
}
