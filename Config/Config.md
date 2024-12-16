# Config API
For more detailed configuration was decided to create shared Interface
of basic functions and little details, which includes to Onion
At the moment of December 2024, I have idea to connect other details using
once configuration file. 
Basicly Configuration file represents 2 important options:
 - Minecraft catalog
 - Your mods catalog (where you store all ```*.jar``` files)
Other modules are connecting using tag-type of itself (ex. 1)
### Including Culture API
For example I want to connect Russian language support:

```xml
<Config>
    <Minecraft>D:\Program Files\TLauncher\Minecraft\mods</Minecraft>
    <Modifications>D:\mods</Modifications>
    
    <!-- Now for including Russian, i need to set-up Culture API tag 
        And write only name of .xml package, just because
        All packages (at the alpha-moment) stores in Application's Domain
    -->
    <Culture>Onion.English.xml</Culture>
</Config>
```

### What if
What if I don't want connecting language support?
You'll get English-package by default, because it stores inside Application
Connecting language modules is optional feature  