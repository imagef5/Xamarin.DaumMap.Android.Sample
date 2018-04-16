/*
 * 소스 참조 : http://apis.map.daum.net/android/sample/
 */
using System;

using Android.App;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Net.Daum.MF.Map.Api;
using static Net.Daum.MF.Map.Api.MapView;
using static Net.Daum.MF.Map.Api.MapPOIItem;

namespace DaumMap.Android.Sample.Demo.Samples
{
    [Activity(Label = "CameraDemoActivity", Name = "daum.map.CameraDemoActivity")]
    public class CameraDemoActivity : FragmentActivity
    {
        #region private member method area
        const int MENU_CAMERA_1 = Menu.First;
        const int MENU_CAMERA_2 = Menu.First + 1;
        const int MENU_CAMERA_3 = Menu.First + 2;
        const int MENU_CAMERA_4 = Menu.First + 3;
        const int MENU_CAMERA_5 = Menu.First + 4;
        MapView mapView;
        //Button hannamButton;
        //Button pangyoButton;

        readonly MapPoint MAP_POINT_POI1 = MapPoint.MapPointWithGeoCoord(37.537229, 127.005515);
        readonly MapPoint MAP_POINT_POI2 = MapPoint.MapPointWithGeoCoord(37.4020737, 127.1086766);

        readonly CameraPosition CAMERA_POSITION_HANNAM = new CameraPosition(MapPoint.MapPointWithGeoCoord(37.537229, 127.005515), 2);
        readonly CameraPosition CAMERA_POSITION_PANGYO = new CameraPosition(MapPoint.MapPointWithGeoCoord(37.4020737, 127.1086766), 2);
        #endregion

        #region override methods area
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.demo_camera);
            mapView = FindViewById<MapView>(Resource.Id.map_view);
            //hannamButton = FindViewById<Button>(Resource.Id.hannam_button);
            //pangyoButton = FindViewById<Button>(Resource.Id.pangyo_button);
            //hannamButton.Click += HannamButton_Click;
            //pangyoButton.Click += PangyoButton_Click;
            mapView.MapViewInitialized += OnMapViewInitialized;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            base.OnCreateOptionsMenu(menu);

            menu.Add(0, MENU_CAMERA_1, Menu.None, "Point 1");
            menu.Add(0, MENU_CAMERA_2, Menu.None, "Point 2, Zoom Lv 7");
            menu.Add(0, MENU_CAMERA_3, Menu.None, "Point 1, Diameter");
            menu.Add(0, MENU_CAMERA_4, Menu.None, "Bounds, Padding");
            menu.Add(0, MENU_CAMERA_5, Menu.None, "Bounds, Padding, Zoom min/max");

            return true;
        }

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
            switch (item.ItemId)
            {
                case MENU_CAMERA_1:
                    toast(item.TitleFormatted);
                    mapView.MoveCamera(CameraUpdateFactory.NewMapPoint(MAP_POINT_POI1));
                    return true;
                case MENU_CAMERA_2:
                    toast(item.TitleFormatted);
                    mapView.MoveCamera(CameraUpdateFactory.NewMapPoint(MAP_POINT_POI2, 7f));
                    return true;
                case MENU_CAMERA_3:
                    toast(item.TitleFormatted);
                    mapView.MoveCamera(CameraUpdateFactory.NewMapPointAndDiameter(MAP_POINT_POI1, 500f));
                    return true;
                case MENU_CAMERA_4:
                    {
                        toast(item.TitleFormatted);
                        var bounds = new MapPointBounds(MAP_POINT_POI1, MAP_POINT_POI2);
                        mapView.MoveCamera(CameraUpdateFactory.NewMapPointBounds(bounds, 100));
                        return true;
                    }
                case MENU_CAMERA_5:
                    {
                        toast(item.TitleFormatted);
                        var bounds = new MapPointBounds(MAP_POINT_POI1, MAP_POINT_POI2);
                        mapView.MoveCamera(CameraUpdateFactory.NewMapPointBounds(bounds, 100, 3, 7));
                        return true;
                    }
            }

            return base.OnOptionsItemSelected(item);
		}

        void toast(Java.Lang.ICharSequence msg)
        {
            Toast.MakeText(this, msg, ToastLength.Short).Show();
        }
        #endregion

        #region Resource.Layout.demo_camera Button Click
        class HannamCancelableCallback : Java.Lang.Object, ICancelableCallback
        {
            FragmentActivity activity;

            public HannamCancelableCallback(FragmentActivity activity)
            {
                this.activity = activity;
            }

            public void OnFinish()
            {
                Toast.MakeText(activity.BaseContext, "Animation to Hannam complete", ToastLength.Short).Show();
            }

            public void OnCancel()
            {
                Toast.MakeText(activity.BaseContext, "Animation to Hannam canceled", ToastLength.Short).Show();
            }
        }

        class PangyoCancelableCallback : Java.Lang.Object, ICancelableCallback
        {
            FragmentActivity activity;

            public PangyoCancelableCallback(FragmentActivity activity)
            {
                this.activity = activity;
            }

            public void OnFinish()
            {
                Toast.MakeText(activity.BaseContext, "Animation to Pangyo complete", ToastLength.Short).Show();
            }

            public void OnCancel()
            {
                Toast.MakeText(activity.BaseContext, "Animation to Pangyo canceled", ToastLength.Short).Show();
            }
        }

        /*
         * 안드로이드의 xml 레이아웃에 onClick 이벤트를 등록하여 직접 호출하는 경우
         * Modo.Android.Export.dll을 참조후 
         * [Java.Interop.Export("OnGoToHannam")] 와 같이 ExportAttribute를 이용하면 호출가능
         */
        [Java.Interop.Export("OnGoToHannam")]
        public void OnGoToHannam(View view)
        {
            mapView.AnimateCamera(CameraUpdateFactory.NewCameraPosition(CAMERA_POSITION_HANNAM), 1000, new HannamCancelableCallback(this));
        }

        [Java.Interop.Export("OnGoToPangyo")]
        public void OnGoToPangyo(View view)
        {
            mapView.AnimateCamera(CameraUpdateFactory.NewCameraPosition(CAMERA_POSITION_PANGYO), 1000, new PangyoCancelableCallback(this));
        }
        /*
            버튼 클릭 이벤트를 프로그램에서 직접 구현하는 경우 아래와같이 등록 해서 호출 
            hannamButton = FindViewById<Button>(Resource.Id.hannam_button);
            pangyoButton = FindViewById<Button>(Resource.Id.pangyo_button);
            hannamButton.Click += HannamButton_Click;
            pangyoButton.Click += PangyoButton_Click;

            ExportAttrubite or 직접 구현하든 각자 편한 방식으로...
         */
        void HannamButton_Click(object sender, EventArgs e)
        {
            mapView.AnimateCamera(CameraUpdateFactory.NewCameraPosition(CAMERA_POSITION_HANNAM), 1000, new HannamCancelableCallback(this));
        }

        void PangyoButton_Click(object sender, EventArgs e)
        {
            mapView.AnimateCamera(CameraUpdateFactory.NewCameraPosition(CAMERA_POSITION_PANGYO), 1000, new PangyoCancelableCallback(this));
        }
        #endregion

        #region implement events area
        void OnMapViewInitialized(object sender, MapViewInitializedEventArgs e)
        {
            var poiItem1 = new MapPOIItem();
            poiItem1.ItemName = "POI1";
            poiItem1.MapPoint = MAP_POINT_POI1;
            poiItem1.POIItemMarkerType = MarkerType.BluePin;
            mapView.AddPOIItem(poiItem1);

            var poiItem2 = new MapPOIItem();
            poiItem2.ItemName = "POI2";
            poiItem2.MapPoint = MAP_POINT_POI2;
            poiItem2.POIItemMarkerType = MarkerType.YellowPin;
            mapView.AddPOIItem(poiItem2);
        }
        #endregion
    }
}
