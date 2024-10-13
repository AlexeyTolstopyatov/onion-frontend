using System.IO;

namespace onion.desktop.Middleware;

public class ExceptionsController : IExceptionsController
{
    public static ExceptionsController Instance() 
        => new ExceptionsController();

    public void Show<T>(ref T e) where T : Exception
    {
        new Wpf.Ui.Controls.MessageBox()
        {
            #if DEBUG
            Title = e.GetType().Name,
            Content = e.ToString()
            #else
            Title = "Остановлено",
            Content = e.Message
            #endif
        }.ShowDialogAsync();
    }

    public void Write<T>(ref T e) where T : Exception
    {
        File.WriteAllText($"{e.HResult}.error", e.ToString());
    }
}