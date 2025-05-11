# HellBlaze Helldivers 2 Randomiser 
![HellBlaze Logo](HellBlaze/wwwroot/images/logo256.png)


[hellblaze.vargur.dev](https://hellblaze.vargur.dev)


HellBlaze is a randomisation tool that will let you create randomised loadouts to play with. Unlike other tools, HellBlaze features loadout style customisation and full-squad setups.

[![ko-fi](https://ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/E1E11EU4JG)


--- 

## Features

- **Squad Management**: Support for up to 4 players with individualised loadouts.
- **Squad Duplicates**: Adjust the slider to determine how random you 
- **Customisable Probability Multipliers**: Adjust factors that influence the randomisation process based on several different stats
- **Loadout Sharing**: Generate and share unique loadout combinations. Includes OG Image support for convenience in Discord
- **Daily Challenge**: Take on a daily challenges with seeded randomisation
- **Stratagem Probability Display**: View the chances of each stratagem being selected with your current settings
- **Local player warbond saving**: HellBlaze saves the warbonds for a player. Re-use a name to reload their warbonds.


## How to Use

1. Add up to 4 players to your squad using the "Add Player" button
2. Adjust randomisation factors or select a preset
3. Adjust player warbonds in the dropdown
4. Click a player name and rename to save their warbonds
5. Click "RANDOMIZE SQUAD LOADOUTS" to generate equipment for all players
6. Click Ready to lock a loadout and save it if you like
7. Share your loadout with friends using the "SHARE" button

*If you would like to make your loadout more random, increase the base factor. This adds a weight to everything, thus reducing the impact of added factors.*


---

## Development

HellBlaze is built with:

- ASP.NET Core
- Blazor WebAssembly
- MudBlazor UI Component Library
- .NET 9.0
- SkiaSharp for OG Image handling

## Presets

HellBlaze includes several preset configurations to quickly generate themed loadouts. Here are some examples:

- **Mixed Loadout**: A well-rounded selection of equipment. Aims to give you some anti-tank and anti-horde with a lesser weighting on support, turrets, precision, explosives. Try this one if you find that your loadouts are just not effective.
- **Cyborg Uprising**: LOTS of lasers, electricity, and turrets. Very robotic/engineer themed.
- **I Like DoTs**: Anything that does damage over time, basically.

You can add more of these to the tool by adding to LoadoutPresets.cs in this format:
```cs
{
            PresetType.WarCrimeWednesday,
            new PresetInfo(
                "War Crime Wednesday",
                Color.Error,
                new Factors { FireFactor = 5, GasFactor = 5, MineFactor = 3, MaxAllowedDupesStratagems = 4 },
                "Geneva Convention? More like Geneva Suggestion!",
                "☠️"
            )
        },
```

## Contributing

Contributions are welcome! Feel free to submit issues or pull requests to help improve HellBlaze.

## Deployment

Deployment can be done using a simple web server as long as you have the dotnet SDK. Pushing new code to main will run a pipeline to publish a new release so, if you like, you can simply use that. Just `wget` the built files, extract, and drop them into a web server with a service pointing at the HellBlaze dll.

## Licence

The code used in project is licensed under an MIT License. Feel free to extend it! I will make it very clear, however, that the static data is not part of this license.
HellBlaze is a fan-made tool and is not affiliated with Arrowhead Game Studios or PlayStation Studios. Helldivers 2 and all related properties are trademarks of their respective owners. 

## Acknowledgements

- Helldivers 2 and related assets are property of Arrowhead Game Studios
- [Helldivers 2 SVGs](https://github.com/nvigneux/Helldivers-2-Stratagems-icons-svg)

---

For Super Earth and Managed Democracy!
