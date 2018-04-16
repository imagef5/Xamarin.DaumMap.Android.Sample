/*
 * 소스 참조 : http://apis.map.daum.net/android/sample/
 */
using System;
using DaumMap.Android.Sample.Demo.Samples;

namespace DaumMap.Android.Sample.Demo
{
    public class DemoListItem
    {
        #region public member fields Area
        public readonly int TitleId;
        public readonly int DescriptionId;
        public readonly Type Activity;
        #endregion

        public DemoListItem(int titleId, int descriptionId, Type activity)
        {
            this.TitleId = titleId;
            this.DescriptionId = descriptionId;
            this.Activity = activity;
        }

        public static readonly DemoListItem[] DemoListITems = 
        { 
            new DemoListItem(Resource.String.map_view_demo_title,
                             Resource.String.map_view_demo_desc, typeof(MapViewDemoActivity)),
            new DemoListItem(Resource.String.marker_demo_title,
                             Resource.String.marker_demo_desc, typeof(MarkerDemoActivity)),
            new DemoListItem(Resource.String.polygon_demo_title,
                             Resource.String.polygon_demo_desc, typeof(PolygonDemoActivity)),
            new DemoListItem(Resource.String.location_demo_title,
                             Resource.String.location_demo_desc, typeof(LocationDemoActivity)),
            new DemoListItem(Resource.String.camera_demo_title,
                             Resource.String.camera_demo_desc, typeof(CameraDemoActivity)),
            new DemoListItem(Resource.String.events_demo_title,
                             Resource.String.events_demo_desc, typeof(EventsDemoActivity))
        };  
    }
}
