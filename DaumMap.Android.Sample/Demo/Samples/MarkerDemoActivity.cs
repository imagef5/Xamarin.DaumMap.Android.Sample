/*
 * 소스 참조 : http://apis.map.daum.net/android/sample/
 */
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Net.Daum.MF.Map.Api;
using static Net.Daum.MF.Map.Api.MapPOIItem;

namespace DaumMap.Android.Sample.Demo.Samples
{
    [Activity(Label = "MarkerDemoActivity", Name = "daum.map.MarkerDemoActivity")]
    public class MarkerDemoActivity : FragmentActivity
    {
        #region private member fields area
        const int MENU_DEFAULT_CALLOUT_BALLOON = Menu.First;
        const int MENU_CUSTOM_CALLOUT_BALLOON = Menu.First + 1;
        const int MENU_SHOW_ALL = Menu.First + 2;

        readonly MapPoint CUSTOM_MARKER_POINT = MapPoint.MapPointWithGeoCoord(37.537229, 127.005515);
        readonly MapPoint CUSTOM_MARKER_POINT2 = MapPoint.MapPointWithGeoCoord(37.447229, 127.015515);
        readonly MapPoint DEFAULT_MARKER_POINT = MapPoint.MapPointWithGeoCoord(37.4020737, 127.1086766);

        MapView mMapView;
        MapPOIItem mDefaultMarker;
        MapPOIItem mCustomMarker;
        MapPOIItem mCustomBmMarker;


        #endregion

        #region override method area
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.demo_nested_mapview);
            mMapView = FindViewById<MapView>(Resource.Id.map_view);

            mMapView.CalloutBalloonOfPOIItemTouched += OnCalloutBalloonOfPOIItemTouched;

            // 구현한 CalloutBalloonAdapter 등록
            mMapView.SetCalloutBalloonAdapter(new CustomCalloutBalloonAdapter(this));
            CreateDefaultMarker(mMapView);
            CreateCustomMarker(mMapView);
            CreateCustomBitmapMarker(mMapView);
            ShowAll();

        }

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
            base.OnCreateOptionsMenu(menu);

            menu.Add(0, MENU_DEFAULT_CALLOUT_BALLOON, Menu.None, "Default CalloutBalloon");
            menu.Add(0, MENU_CUSTOM_CALLOUT_BALLOON, Menu.None, "Custom CalloutBalloon");

            return true;
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
            switch (item.ItemId)
            {
                case MENU_DEFAULT_CALLOUT_BALLOON:
                    mMapView.RemoveAllPOIItems();
                    mMapView.SetCalloutBalloonAdapter(null);
                    CreateDefaultMarker(mMapView);
                    CreateCustomMarker(mMapView);
                    ShowAll();
                    return true;
                case MENU_CUSTOM_CALLOUT_BALLOON:
                    mMapView.RemoveAllPOIItems();
                    mMapView.SetCalloutBalloonAdapter(new CustomCalloutBalloonAdapter(this));
                    CreateDefaultMarker(mMapView);
                    CreateCustomMarker(mMapView);
                    ShowAll();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
		}
		#endregion

		#region Implement ICalloutBalloonAdapter Interface
		class CustomCalloutBalloonAdapter : Java.Lang.Object, ICalloutBalloonAdapter
        {
            readonly View mCalloutBalloon;

            public CustomCalloutBalloonAdapter(FragmentActivity activety)
            {
                mCalloutBalloon = activety.LayoutInflater.Inflate(Resource.Layout.custom_callout_balloon, null);
            }

            public View GetCalloutBalloon(MapPOIItem poiItem)
            {
                mCalloutBalloon.FindViewById<ImageView>(Resource.Id.badge).SetImageResource(Resource.Drawable.ic_launcher);
                mCalloutBalloon.FindViewById<TextView>(Resource.Id.title).Text = poiItem.ItemName;
                mCalloutBalloon.FindViewById<TextView>(Resource.Id.desc).Text = "Custom CalloutBalloon";
                return mCalloutBalloon;
            }

            public View GetPressedCalloutBalloon(MapPOIItem poiItem)
            {
                return null;
            }
        }
        #endregion

        #region private method area
        void CreateDefaultMarker(MapView mapView)
        {
            mDefaultMarker = new MapPOIItem();
            var name = "Default Marker";
            mDefaultMarker.ItemName = name;
            mDefaultMarker.Tag = 0;
            mDefaultMarker.MapPoint = DEFAULT_MARKER_POINT;
            mDefaultMarker.POIItemMarkerType = MarkerType.BluePin;
            mDefaultMarker.SelectedMarkerType = MarkerType.RedPin;

            mapView.AddPOIItem(mDefaultMarker);
            mapView.SelectPOIItem(mDefaultMarker, true);
            mapView.SetMapCenterPoint(DEFAULT_MARKER_POINT, false);
        }

        void CreateCustomMarker(MapView mapView)
        {
            mCustomMarker = new MapPOIItem();
            var name = "Custom Marker";
            mCustomMarker.ItemName = name;
            mCustomMarker.Tag = 1;
            mCustomMarker.MapPoint = CUSTOM_MARKER_POINT;

            mCustomMarker.POIItemMarkerType = MarkerType.CustomImage;

            mCustomMarker.CustomImageResourceId = Resource.Drawable.custom_marker_red;
            mCustomMarker.CustomImageAutoscale =false;
            mCustomMarker.SetCustomImageAnchor(0.5f, 1.0f);

            mapView.AddPOIItem(mCustomMarker);
            mapView.SelectPOIItem(mCustomMarker, true);
            mapView.SetMapCenterPoint(CUSTOM_MARKER_POINT, false);
        }

        void CreateCustomBitmapMarker(MapView mapView)
        {
            mCustomBmMarker = new MapPOIItem();
            var name = "Custom Bitmap Marker";
            mCustomBmMarker.ItemName = name;
            mCustomBmMarker.Tag = 2;
            mCustomBmMarker.MapPoint = CUSTOM_MARKER_POINT2;

            mCustomBmMarker.POIItemMarkerType = MarkerType.CustomImage;
            Bitmap bm = BitmapFactory.DecodeResource(Resources, Resource.Drawable.custom_marker_star);
            mCustomBmMarker.CustomImageBitmap = bm;
            mCustomBmMarker.CustomImageAutoscale = false;
            mCustomBmMarker.SetCustomImageAnchor(0.5f, 0.5f);

            mapView.AddPOIItem(mCustomBmMarker);
            mapView.SelectPOIItem(mCustomBmMarker, true);
            mapView.SetMapCenterPoint(CUSTOM_MARKER_POINT, false);
        }

        void ShowAll()
        {
            int padding = 20;
            float minZoomLevel = 7;
            float maxZoomLevel = 10;
            MapPointBounds bounds = new MapPointBounds(CUSTOM_MARKER_POINT, DEFAULT_MARKER_POINT);
            mMapView.MoveCamera(CameraUpdateFactory.NewMapPointBounds(bounds, padding, minZoomLevel, maxZoomLevel));
        }
        #endregion

        #region implement event area
        //MapView mapView, MapPOIItem mapPOIItem
        void OnCalloutBalloonOfPOIItemTouched(object sender, MapView.CalloutBalloonOfPOIItemTouchedEventArgs e)
        {
            var mapPOIItem = e.P1;
            Toast.MakeText(this, "Clicked " + mapPOIItem.ItemName + " Callout Balloon", ToastLength.Short).Show();
        }
        #endregion
    }
}
