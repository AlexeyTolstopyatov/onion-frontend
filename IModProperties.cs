namespace onion.desktop.Middleware;

public interface IModProperties
{
    /// <summary>
    /// Должен получать модель свойств о моде и создать PropertiesWindow
    /// </summary>
    /// <typeparam name="T">Тип модели данных, которую оно должно вернуть</typeparam>
    /// <returns>Состояние операции. и создает окно</returns>
    bool Get<T>();
    
}