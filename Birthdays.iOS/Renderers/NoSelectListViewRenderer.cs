using Birthdays.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ListView), typeof(NoSelectListViewRenderer))]
namespace Birthdays.iOS.Renderers {
    public class NoSelectListViewRenderer : ListViewRenderer {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e) {
            base.OnElementChanged(e);
            Control.AllowsSelection = false;
        }
    }
}
