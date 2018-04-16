using Android.App;
using Android.OS;
using Net.Daum.MF.Map.Api;
using DaumMap.Android.Sample.Demo;
using Android.Views;
using Android.Widget;
using Android.Content;
using Android.Support.V4.App;

namespace DaumMap.Android.Sample
{
    [Activity(Label = "다음지도 샘", Icon = "@mipmap/icon", Name ="daum.map.MainActivity")]
    public class MainActivity : ListActivity
    {
        #region private member area
        DemoListAdapter adapter;
        #endregion
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            adapter = new DemoListAdapter(this, DemoListItem.DemoListITems);
            this.ListAdapter = adapter;
        }

		protected override void OnListItemClick(ListView l, View v, int position, long id)
		{
            var demo = adapter.GetItem(position);
            StartActivity(new Intent(this, demo.Activity));
		}
	}
}

