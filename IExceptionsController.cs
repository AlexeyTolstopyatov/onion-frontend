namespace onion.desktop.Middleware;

public interface IExceptionsController
{
    /// <summary>
    /// Записывает ошибку в файл, сохраняет в корне проекта
    /// Наверное это будет необходимо для создания отчетов
    /// </summary>
    void Write<T>(ref T e) where T : Exception;
    
    /// <summary>
    /// Вызывает диалоговое окно с информацией об ошибке
    /// </summary>
    void Show<T>(ref T e) where T : Exception;
}