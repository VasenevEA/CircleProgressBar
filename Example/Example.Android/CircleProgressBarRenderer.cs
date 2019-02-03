using Example.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Example;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(Example.CircleProgressBar), typeof(CircleProgressBarRenderer))]
namespace Example.Droid
{
#pragma warning disable CS0618 // Тип или член устарел
    public class CircleProgressBarRenderer : ViewRenderer<Example.CircleProgressBar, Example.Droid.CuteCircleProgressBar>
    {
        Example.Droid.CircleProgressBar progress;
        CuteCircleProgressBar progress1;
        protected override void OnElementChanged(ElementChangedEventArgs<Example.CircleProgressBar> e)
        {
            base.OnElementChanged(e);
            var el = e.NewElement;
            if (e.OldElement != null || this.Element == null)
                return;
            /*
             progress = new Example.Droid.CircleProgressBar(Forms.Context)
            {
                Max = 100,
                Progress = el.Value
            };

            SetNativeControl(progress);*/

            progress1 = new CuteCircleProgressBar(Forms.Context);
            SetNativeControl(progress1);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            //progress.Progress = Element.Value;
            progress1.setProgress(Element.Value);
        }
    }
#pragma warning restore CS0618 // Тип или член устарел
}