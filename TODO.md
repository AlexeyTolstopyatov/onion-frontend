# Требуется сделать
Промежуточный слой между ```onion.Core``` и проектом приложения для рабочего стола

### Конкретнее
Внутри "ядра" содержится логика по созданию/удалению/поиску
сборок, модификаций для Minecraft Java издания
Поэтому если внутри ядра будут какие-то функции, которые
возвращают данные, то их надо перехватывать здесь
и обрабатывать

```csharp
napespace Middleware {
    internal interface IMCModProperties {
        public async void GetAsync<T>(ref T modprop); where T : SanyaModel
        public void Get<T>()
    }
    
    internal interface IMCModBuilding {
        bool Get(ref AnotherSanyaModel model);
        bool Set(ref AnotherSanyaModel model);
    }
    
    internal interface IMCModPack{
        // Same
    }
}
```