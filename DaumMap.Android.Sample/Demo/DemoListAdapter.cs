/*
 * 소스 참조 : http://apis.map.daum.net/android/sample/
 */
using Android.Content;
using Android.Views;
using Android.Widget;

namespace DaumMap.Android.Sample.Demo
{
    public class DemoListAdapter : ArrayAdapter<DemoListItem>
    {
        public DemoListAdapter(Context context, DemoListItem[] demos) : base(context, Resource.Layout.demo_list_item_view, demos)
        {
        }

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
            var demo = GetItem(position);

            if (convertView == null)
            {
                LayoutInflater inflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);
                convertView = inflater.Inflate(Resource.Layout.demo_list_item_view, null);
            }

            var title = convertView.FindViewById<TextView>(Resource.Id.title);
            title.SetText(demo.TitleId);

            var description = convertView.FindViewById<TextView>(Resource.Id.description);
            description.SetText(demo.DescriptionId);

            return convertView;
		}
	}
}
