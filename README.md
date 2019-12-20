# DependencyIncetionExtensions

Custom Extensions for Microsoft's DependencyInjection Framework. Just experimental.

## Installation

TODO

## Usage

**Metadata Bindings**

Related to https://github.com/ninject/Ninject/wiki/Contextual-Binding

```csharp
// Register service implementations with metadata
var services = new ServiceCollection();
services.Bind<IService, ServiceA>().WithMetadata(BindingKeys.MyKey, "A");
services.Bind<IService, ServiceB>().WithMetadata(BindingKeys.MyKey, "B");

// Resolve service implementations
var serviceProvider = services.BuildServiceProvider();
ServiceA service = serviceProvider.Resolve<IService>(BindingKeys.MyKey, "A");

// Typed metadata keys
public class BindingKeys
{
    public const string MyKey = "MyKey";
}
```

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License

[MIT](https://choosealicense.com/licenses/mit/)