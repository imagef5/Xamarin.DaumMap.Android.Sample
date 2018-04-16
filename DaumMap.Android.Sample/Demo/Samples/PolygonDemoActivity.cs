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
    [Activity(Label = "PolygonDemoActivity", Name = "daum.map.PolygonDemoActivity")]
    public class PolygonDemoActivity : FragmentActivity
    {
        #region private member fields area
        const int MENU_ADD_POLYLINE1 = Menu.First;
        const int MENU_ADD_POLYLINE2 = Menu.First + 1;
        const int MENU_REMOVE_POLYLINES = Menu.First + 2;
        const int MENU_ADD_CIRCLES = Menu.First + 3;
        const int MENU_REMOVE_CIRCLE = Menu.First + 4;

        MapView mMapView;
        MapPoint[] mPolyline2Points;
        bool isMapViewInitialized = false;
        #endregion

        #region override methods area
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.demo_nested_mapview);
            mMapView = FindViewById<MapView>(Resource.Id.map_view);
            mMapView.MapViewInitialized += (s, e) =>
            {
                isMapViewInitialized = true;
            };

            mPolyline2Points = new MapPoint[]{
                MapPoint.MapPointWithWCONGCoord(475334.0,1101210.0),
                MapPoint.MapPointWithWCONGCoord(474300.0,1104123.0),
                MapPoint.MapPointWithWCONGCoord(474300.0,1104123.0),
                MapPoint.MapPointWithWCONGCoord(473873.0,1105377.0),
                MapPoint.MapPointWithWCONGCoord(473302.0,1107097.0),
                MapPoint.MapPointWithWCONGCoord(473126.0,1109606.0),
                MapPoint.MapPointWithWCONGCoord(473063.0,1110548.0),
                MapPoint.MapPointWithWCONGCoord(473435.0,1111020.0),
                MapPoint.MapPointWithWCONGCoord(474068.0,1111714.0),
                MapPoint.MapPointWithWCONGCoord(475475.0,1112765.0),
                MapPoint.MapPointWithWCONGCoord(476938.0,1113532.0),
                MapPoint.MapPointWithWCONGCoord(478725.0,1114391.0),
                MapPoint.MapPointWithWCONGCoord(479453.0,1114785.0),
                MapPoint.MapPointWithWCONGCoord(480145.0,1115145.0),
                MapPoint.MapPointWithWCONGCoord(481280.0,1115237.0),
                MapPoint.MapPointWithWCONGCoord(481777.0,1115164.0),
                MapPoint.MapPointWithWCONGCoord(482322.0,1115923.0),
                MapPoint.MapPointWithWCONGCoord(482832.0,1116322.0),
                MapPoint.MapPointWithWCONGCoord(483384.0,1116754.0),
                MapPoint.MapPointWithWCONGCoord(484401.0,1117547.0),
                MapPoint.MapPointWithWCONGCoord(484893.0,1117930.0),
                MapPoint.MapPointWithWCONGCoord(485016.0,1118034.0)
            };
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            base.OnCreateOptionsMenu(menu);

            menu.Add(0, MENU_ADD_POLYLINE1, Menu.None, "Add Polyline1");
            menu.Add(0, MENU_ADD_POLYLINE2, Menu.None, "Add Polyline2");
            menu.Add(0, MENU_REMOVE_POLYLINES, Menu.None, "Remove All Polylines");
            menu.Add(0, MENU_ADD_CIRCLES, Menu.None, "Add Circles");
            menu.Add(0, MENU_REMOVE_CIRCLE, Menu.None, "Remove All Circles");

            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (!isMapViewInitialized)
            {
                Toast.MakeText(this, "MapView is not initialized", ToastLength.Short).Show();
                return base.OnOptionsItemSelected(item);
            }

            switch (item.ItemId)
            {
                case MENU_ADD_POLYLINE1:
                    AddPolyline1();
                    return true;
                case MENU_ADD_POLYLINE2:
                    AddPolyline2();
                    return true;
                case MENU_REMOVE_POLYLINES:
                    mMapView.RemoveAllPOIItems();
                    mMapView.RemoveAllPolylines();
                    return true;
                case MENU_ADD_CIRCLES:
                    AddCircles();
                    return true;
                case MENU_REMOVE_CIRCLE:
                    mMapView.RemoveAllCircles();
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }
        #endregion

        #region private method area
        void AddPolyline1()
        {
            var existingPolyline = mMapView.FindPolylineByTag(1000);
            if (existingPolyline != null)
            {
                mMapView.RemovePolyline(existingPolyline);
            }

            var polyline1 = new MapPolyline();
            polyline1.Tag = 1000;
            polyline1.LineColor = Color.Argb(128, 255, 51, 0);
            polyline1.AddPoint(MapPoint.MapPointWithGeoCoord(37.537229, 127.005515));
            polyline1.AddPoint(MapPoint.MapPointWithGeoCoord(37.545024, 127.03923));
            polyline1.AddPoint(MapPoint.MapPointWithGeoCoord(37.527896, 127.036245));
            polyline1.AddPoint(MapPoint.MapPointWithGeoCoord(37.541889, 127.095388));
            mMapView.AddPolyline(polyline1);

            var mapPointBounds = new MapPointBounds(polyline1.GetMapPoints());
            int padding = 100; // px
            mMapView.MoveCamera(CameraUpdateFactory.NewMapPointBounds(mapPointBounds, padding));
        }

        void AddPolyline2()
        {
            var existingPOIItemStart = mMapView.FindPOIItemByTag(10001);
            if (existingPOIItemStart != null)
            {
                mMapView.RemovePOIItem(existingPOIItemStart);
            }

            var existingPOIItemEnd = mMapView.FindPOIItemByTag(10002);
            if (existingPOIItemEnd != null)
            {
                mMapView.RemovePOIItem(existingPOIItemEnd);
            }

            var existingPolyline = mMapView.FindPolylineByTag(2000);
            if (existingPolyline != null)
            {
                mMapView.RemovePolyline(existingPolyline);
            }

            var poiItemStart = new MapPOIItem();
            poiItemStart.ItemName = "Start";
            poiItemStart.Tag = 10001;
            poiItemStart.MapPoint = MapPoint.MapPointWithWCONGCoord(475334.0, 1101210.0);
            poiItemStart.POIItemMarkerType = MarkerType.CustomImage;
            poiItemStart.AnimationType = ShowAnimationType.SpringFromGround;
            poiItemStart.ShowCalloutBalloonOnTouch = false;
            poiItemStart.CustomImageResourceId = Resource.Drawable.custom_poi_marker_start;
            poiItemStart.CustomImageAnchorPointOffset = new ImageOffset(29, 2);
            mMapView.AddPOIItem(poiItemStart);

            var poiItemEnd = new MapPOIItem();
            poiItemEnd.ItemName = "End";
            poiItemEnd.Tag = 10001;
            poiItemEnd.MapPoint = MapPoint.MapPointWithWCONGCoord(485016.0, 1118034.0);
            poiItemEnd.POIItemMarkerType = MarkerType.CustomImage;
            poiItemEnd.AnimationType = ShowAnimationType.SpringFromGround;
            poiItemEnd.ShowCalloutBalloonOnTouch = false;
            poiItemEnd.CustomImageResourceId = Resource.Drawable.custom_poi_marker_end;
            poiItemEnd.CustomImageAnchorPointOffset = new ImageOffset(29, 2);
            mMapView.AddPOIItem(poiItemEnd);

            var polyline2 = new MapPolyline(21);
            polyline2.Tag = 2000;
            polyline2.LineColor = Color.Argb(128, 0, 0, 255);
            polyline2.AddPoints(mPolyline2Points);
            mMapView.AddPolyline(polyline2);

            var mapPointBounds = new MapPointBounds(mPolyline2Points);
            int padding = 200; // px
            mMapView.MoveCamera(CameraUpdateFactory.NewMapPointBounds(mapPointBounds, padding));
        }

        void AddCircles()
        {
            var circle1 = new MapCircle
                (
                    MapPoint.MapPointWithGeoCoord(37.537094, 127.005470), // center
                    500, // radius
                    Color.Argb(128, 255, 0, 0), // strokeColor 
                    Color.Argb(128, 0, 255, 0) // fillColor
                );
            circle1.Tag = 1234;
            mMapView.AddCircle(circle1);
            var circle2 = new MapCircle
                (
                    MapPoint.MapPointWithGeoCoord(37.551094, 127.019470), // center
                    1000, // radius
                    Color.Argb(128, 255, 0, 0), // strokeColor 
                    Color.Argb(128, 255, 255, 0) // fillColor
                );
            circle2.Tag = 5678;
            mMapView.AddCircle(circle2);

            // 지도뷰의 중심좌표와 줌레벨을 Circle이 모두 나오도록 조정.
            MapPointBounds[] mapPointBoundsArray = { circle1.Bound, circle2.Bound };
            MapPointBounds mapPointBounds = new MapPointBounds(mapPointBoundsArray);
            int padding = 50; // px
            mMapView.MoveCamera(CameraUpdateFactory.NewMapPointBounds(mapPointBounds, padding));
        }
        #endregion
    }
}
