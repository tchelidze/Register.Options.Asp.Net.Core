# Register.Options.Asp.Net.Core

Automatic `IOptions` registration for ASP.NET Core 2


# [Nuget](https://www.nuget.org/packages/Register.Options.Asp.Net.Core)

`PM> Install-Package Register.Options.Asp.Net.Core`


Having following *appsettings* file, *IDEOptions* and *PersonOptions* classes.

*appsettings.json*
```JSON
"PersonOptions": {
    "FullName": "Bitchiko Tchelidze",
    "Age": 22
},
{
    "IDEOptions": {
    "Name": "Visual Studio",
    "Version": "15.6.6"
}
```

*IDEOptions.cs*
```c#
public class IDEOptions
{
    public string Name { get; set; }

    public string Version { get; set; }
}
```

*PersonOptions*
```c#
public class PersonOptions
{
   public string FullName { get; set; }

   public int Age { get; set; }
}
```

Register all the options in assembly conventionaly with single call. 

Convention : **settings's key in *appsettings* file must match the name of class.**

*Startup.cs*
```c#
public void ConfigureServices(IServiceCollection services)
{
       services.ConfigureOptionsFromEntyAssembly(Configuration);
}
```

Or specify assemblies explicitly

```c#

public void ConfigureServices(IServiceCollection services)
{
    services.ConfigureOptionsFromAssemblies(configuration, new List<Assembly>()
    {
        typeof(IDEOptions).Assembly,
        typeof(PersonOptions).Assembly               
    });
}
```

Or register options one by one

```c#
public void ConfigureServices(IServiceCollection services)
{
     services.ConfigureOption<IDEOptions>(Configuration);
     services.ConfigureOption<PersonOptions>(Configuration);
}
```

See [Demo](https://github.com/tchelidze/Register.Options.Asp.Net.Core/tree/master/samples/RegisterOptionsDemo) and [Tests](https://github.com/tchelidze/Register.Options.Asp.Net.Core/tree/master/test/Register.Options.Asp.Net.Core.Tests) projects. 
