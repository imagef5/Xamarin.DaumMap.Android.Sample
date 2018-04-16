/*
 * 소스 참조 : http://apis.map.daum.net/android/sample/
 */
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Net.Daum.MF.Map.Api;
using static Net.Daum.MF.Map.Api.MapView;

namespace DaumMap.Android.Sample.Demo.Samples
{
    [Activity(Label = "MapViewDemoActivity", Name = "daum.map.MapViewDemoActivity")]
    public class MapViewDemoActivity : FragmentActivity
    {
        #region private member fields area
        const int MENU_MAP_TYPE = Menu.First + 1;
        const int MENU_MAP_MOVE = Menu.First + 2;
        const string LOG_TAG = "MapViewDemoActivity";
        MapView mMapView;
        #endregion

        #region override method area
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.MapView);

            MapLayout mapLayout = new MapLayout(this);
            mMapView = mapLayout.MapView;

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

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
            base.OnCreateOptionsMenu(menu);

		    menu.Add(0, MENU_MAP_TYPE, Menu.None, "MapType");
		    menu.Add(0, MENU_MAP_MOVE, Menu.None, "Move");

		    return true;
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
            
            var itemId = item.ItemId;
            switch (itemId)
            {
                case MENU_MAP_TYPE:
                {

                    //String hdMapTile = mMapView.isHDMapTileEnabled()? "HD Map Tile Off" : "HD Map Tile On";

                    string hdMapTile;

                    if (mMapView.TileMode == MapTileMode.Hd2x)
                    {
                        hdMapTile = "Set to Standard Mode";
                    }
                    else if (mMapView.TileMode == MapTileMode.Hd)
                    {
                        hdMapTile = "Set to HD 2X Mode";
                    }
                    else
                    {
                        hdMapTile = "Set to HD Mode";
                    }

                    string[] mapTypeMenuItems = { "Standard", "Satellite", "Hybrid", hdMapTile, "Clear Map Tile Cache" };

                    AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                    dialog.SetTitle("MapType");
                    dialog.SetItems(mapTypeMenuItems, (object sender, DialogClickEventArgs e) =>  
                                    {
                                        ControlMapTile(e.Which);
                                    });
                    dialog.Show();

                    return true;
                }

                case MENU_MAP_MOVE:
                {
                    string rotateMapMenu = mMapView.MapRotationAngle.Equals(0.0f) ? "Rotate Map 60" : "Unrotate Map";
                    string[] mapMoveMenuItems = { "Move to", "Zoom to", "Move and Zoom to", "Zoom In", "Zoom Out", rotateMapMenu };

                    AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                    dialog.SetTitle("Move");
                    dialog.SetItems(mapMoveMenuItems,(object sender, DialogClickEventArgs e) =>  
                                    {
                                        ControlMapMove(e.Which);
                                    });
                    dialog.Show();

                    return true;
                }
            }
            return base.OnOptionsItemSelected(item);
		}
    #endregion

        #region private method area
        private void ControlMapMove(int which)
        {
            switch (which)
            {
                case 0: // Move to
                    {
                        mMapView.SetMapCenterPoint(MapPoint.MapPointWithGeoCoord(37.53737528, 127.00557633), true);
                    }
                    break;
                case 1: // Zoom to
                    {
                        mMapView.SetZoomLevel(7, true);
                    }
                    break;
                case 2: // Move and Zoom to
                    {
                        mMapView.SetMapCenterPointAndZoomLevel(MapPoint.MapPointWithGeoCoord(33.41, 126.52), 9, true);
                    }
                    break;
                case 3: // Zoom In
                    {
                        mMapView.ZoomIn(true);
                    }
                    break;
                case 4: // Zoom Out
                    {
                        mMapView.ZoomOut(true);
                    }
                    break;
                case 5: // Rotate Map 60, Unrotate Map
                    {
                        if (mMapView.MapRotationAngle.Equals(0.0f))
                        {
                            mMapView.SetMapRotationAngle(60.0f, true);
                        }
                        else
                        {
                            mMapView.SetMapRotationAngle(0.0f, true);
                        }
                    }
                    break;
            }
        }

        /**
         * 지도 타일 컨트롤.
        */
        private void ControlMapTile(int which)
        {
            switch (which)
            {
                case 0: // Standard
                    {
                        mMapView.Maptype = MapType.Standard;
                    }
                    break;
                case 1: // Satellite
                    {
                        mMapView.Maptype = MapType.Satellite;
                    }
                    break;
                case 2: // Hybrid
                    {
                        mMapView.Maptype = MapType.Hybrid;
                    }
                    break;
                case 3: // HD Map Tile On/Off
                    {
                        if (mMapView.TileMode == MapTileMode.Hd2x)
                        {
                            //Set to Standard Mode
                            mMapView.TileMode = MapTileMode.Standard;
                        }
                        else if (mMapView.TileMode == MapTileMode.Hd)
                        {
                            //Set to HD 2X Mode
                            mMapView.TileMode = MapTileMode.Hd2x;
                        }
                        else
                        {
                            //Set to HD Mode
                            mMapView.TileMode = MapTileMode.Hd;
                        }
                    }
                    break;
                case 4: // Clear Map Tile Cache
                    {
                        MapView.ClearMapTilePersistentCache();
                    }
                    break;
            }
        }
        #endregion

        #region MapView event area
        void OnOpenAPIKeyAuthenticationResult(object sender, MapView.OpenAPIKeyAuthenticationResultEventArgs e)
        {
            var resultCode = e.P1;
            var resultMessage = e.P2;
            Log.Info(LOG_TAG, $"Open API Key Authentication Result : code={resultCode}, message={resultMessage}");
        }

        void OnMapViewInitialized(object sender, MapView.MapViewInitializedEventArgs e)
        {
            Log.Info(LOG_TAG, "MapView had loaded. Now, MapView APIs could be called safely");
            //mMapView.setCurrentLocationTrackingMode(MapView.CurrentLocationTrackingMode.TrackingModeOnWithoutHeading);
            if (e.P0 is MapView mapView)
            {
                mapView.SetMapCenterPointAndZoomLevel(MapPoint.MapPointWithGeoCoord(37.537229, 127.005515), 2, true);
            }
        }

        void OnMapViewCenterPointMoved(object sender, MapView.MapViewCenterPointMovedEventArgs e)
        {
            var mapCenterPoint = e.P1;
            MapPoint.GeoCoordinate mapPointGeo = mapCenterPoint.MapPointGeoCoord;
            Log.Info(LOG_TAG, $"MapView onMapViewCenterPointMoved ({mapPointGeo.Latitude},{mapPointGeo.Longitude})");
        }

        void OnMapViewDoubleTapped(object sender, MapView.MapViewDoubleTappedEventArgs e)
        {
            var mapPoint = e.P1;
            MapPoint.GeoCoordinate mapPointGeo = mapPoint.MapPointGeoCoord;

            AlertDialog.Builder alertDialog = new AlertDialog.Builder(this);
            alertDialog.SetTitle("DaumMapLibrarySample");
            alertDialog.SetMessage($"Double-Tap on ({mapPointGeo.Latitude},{mapPointGeo.Longitude})");
            alertDialog.SetPositiveButton("OK", listener:null);
            alertDialog.Show();
        }

        void OnMapViewLongPressed(object sender, MapView.MapViewLongPressedEventArgs e)
        {
            var mapPoint = e.P1;
            MapPoint.GeoCoordinate mapPointGeo = mapPoint.MapPointGeoCoord;

            AlertDialog.Builder alertDialog = new AlertDialog.Builder(this);
            alertDialog.SetTitle("DaumMapLibrarySample");
            alertDialog.SetMessage($"Long-Press on ({mapPointGeo.Latitude},{mapPointGeo.Longitude})");
            alertDialog.SetPositiveButton("OK", listener: null);
            alertDialog.Show();
        }

        void OnMapViewSingleTapped(object sender, MapView.MapViewSingleTappedEventArgs e)
        {
            var mapPoint = e.P1;
            MapPoint.GeoCoordinate mapPointGeo = mapPoint.MapPointGeoCoord;
            Log.Info(LOG_TAG,$"MapView onMapViewSingleTapped ({mapPointGeo.Latitude},{mapPointGeo.Longitude})");
        }

        void OnMapViewDragStarted(object sender, MapView.MapViewDragStartedEventArgs e)
        {
            var mapPoint = e.P1;
            MapPoint.GeoCoordinate mapPointGeo = mapPoint.MapPointGeoCoord;
            Log.Info(LOG_TAG, $"MapView onMapViewDragStarted ({mapPointGeo.Latitude},{mapPointGeo.Longitude})");
        }

        void OnMapViewDragEnded(object sender, MapView.MapViewDragEndedEventArgs e)
        {
            var mapPoint = e.P1;
            MapPoint.GeoCoordinate mapPointGeo = mapPoint.MapPointGeoCoord;
            Log.Info(LOG_TAG, $"MapView onMapViewDragEnded ({mapPointGeo.Latitude},{mapPointGeo.Longitude})");
        }

        void OnMapViewMoveFinished(object sender, MapView.MapViewMoveFinishedEventArgs e)
        {
            var mapPoint = e.P1;
            MapPoint.GeoCoordinate mapPointGeo = mapPoint.MapPointGeoCoord;
            Log.Info(LOG_TAG, $"MapView onMapViewMoveFinished ({mapPointGeo.Latitude},{mapPointGeo.Longitude})");
        }

        void OnMapViewZoomLevelChanged(object sender, MapView.MapViewZoomLevelChangedEventArgs e)
        {
            var zoomLevel = e.P1;
            Log.Info(LOG_TAG, $"MapView  onMapViewZoomLevelChanged ({zoomLevel})");
        }
        #endregion
    }
}
