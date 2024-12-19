# Culture API

First version of Culture API includes tags of all controls (UI elements).
For example I can show English language support

### Naming Rules
On the alpha-state Application has only 3 pages and every UI-Control's name includes Page's name firstly (ex. 1)

<h4>Navigation Items (ex. 1)</h4>

 - NavigationAbout
 - NavigationExplorer
 - NavigationSettings

I show here all tags (avaliable at the moment of Dec 2024)

```xml
<?xml version="1.0"?>
<Culture>
    <NavigationSettings>Your settings</NavigationSettings>
    <NavigationExplorer>Your projects</NavigationExplorer>
    <NavigationAbout>Main</NavigationAbout>

    <SettingsMinecraftCatalog>Where is Minecraft catalog?</SettingsMinecraftCatalog>
    <SettingsModCatalog>Where you store mods?</SettingsModCatalog>
    <SettingsLanguage>Choose language</SettingsLanguage>
    
    <AboutMessage>About Onion textblock here</AboutMessage>
</Culture>
```