using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace DXGrid_GridLayout {
    public partial class Window1 : Window {
        public static readonly DependencyProperty IsLayoutSavedProperty =
            DependencyProperty.Register("IsLayoutSaved", typeof(bool), typeof(Window1), null);
        public bool IsLayoutSaved {
            get { return (bool)GetValue(IsLayoutSavedProperty); }
            set { SetValue(IsLayoutSavedProperty, value); }
        }
        MemoryStream layoutStream;
        public Window1() {
            DataContext = this;
            InitializeComponent();
            IsLayoutSaved = false;
            grid.ItemsSource = IssueList.GetData();
        }

        void OnSaveLayout(object sender, RoutedEventArgs e) {
            layoutStream = new MemoryStream();
            grid.SaveLayoutToStream(layoutStream);
            IsLayoutSaved = true;
        }
        void OnRestoreLayout(object sender, RoutedEventArgs e) {
            layoutStream.Position = 0;
            grid.RestoreLayoutFromStream(layoutStream);
        }
        void OnAddNewColumn(object sender, RoutedEventArgs e) {
            grid.Columns.Add(new DevExpress.Xpf.Grid.GridColumn() { FieldName = "IsPrivate" });
        }
    }

    public class IssueList {
        static public ObservableCollection<IssueDataObject> GetData() {
            ObservableCollection<IssueDataObject> data = new ObservableCollection<IssueDataObject>();
            data.Add(new IssueDataObject() { IssueName = "Transaction History", IssueType = "Bug", IsPrivate = true });
            data.Add(new IssueDataObject() { IssueName = "Ledger: Inconsistency", IssueType = "Bug", IsPrivate = false });
            data.Add(new IssueDataObject() { IssueName = "Data Import", IssueType = "Request", IsPrivate = false });
            data.Add(new IssueDataObject() { IssueName = "Data Archiving", IssueType = "Request", IsPrivate = true });
            return data;
        }
    }
    public class IssueDataObject {
        public string IssueName { get; set; }
        public string IssueType { get; set; }
        public bool IsPrivate { get; set; }
    }
}
