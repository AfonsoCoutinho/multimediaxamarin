using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Multimedia.ViewModels;

namespace Multimedia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoticeDetailPage : ContentPage
    {
        public NoticeDetailPage(int Id)
        {
            InitializeComponent();
            BindingContext = new NoticeViewModel { SelectedId = Id };
            ((NoticeViewModel)BindingContext).GetNoticeComand.Execute(null);
        }
    }
}