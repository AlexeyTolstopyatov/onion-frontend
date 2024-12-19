using System.Windows.Controls;
using Wpf.Ui.Controls;

namespace Onion.Desktop.View;

public partial class FileGridViewItem : UserControl
{
    public IconElement FileIcon { get; set; }
    public string FileContent { get; set; }

    public FileGridViewItem(IconElement fileIcon, string fileContent)
    {
        InitializeComponent();
        DataContext = this;
        FileIcon = fileIcon;
        FileContent = fileContent;
    }

    public FileGridViewItem() : this(new IconSourceElement(), "alert24")
    {
        
    }
}