using System.Security.Principal;
using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Controls;

namespace onion.desktop.Middleware;

public interface IWindow
{
    /// <summary>
    /// Показывает указанную форму в указанной рамке
    /// </summary>
    /// <param name="frame"></param>
    /// <param name="page"></param>
    void ShowIn(
        ref Page frame,
        ref Frame page) => frame.Content = page;

    /// <summary>
    /// Показывает переданное сюда окно
    /// Использую такой ужас для сокращения
    /// </summary>
    /// <param name="window"></param>
    void Show(FluentWindow window) => window.Show();

    /// <summary>
    /// 
    /// </summary>
    void ShowReportWindow();
}