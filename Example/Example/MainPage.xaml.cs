using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Example
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            TapCommand = new Xamarin.Forms.Command((ob) =>
            {
                var progress = ob as CircleProgressBar;
                if (progress !=null)
                {
                    progress.Value += 10;
                }
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Task.Run(async () =>
            {
                while(true)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        progressBar.Value += 1;
                        progressBar1.Value += 2;
                        progressBar2.Value += 3;

                        if (progressBar.Value > 100)
                            progressBar.Value = 0;
                        if (progressBar1.Value > 100)
                            progressBar1.Value = 0;
                        if (progressBar2.Value > 100)
                            progressBar2.Value = 0;
                    });
                    await Task.Delay(200);
                }
            });
        }
        public ICommand TapCommand { get; set; }
    }
}
