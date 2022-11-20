# StarryPath

A game about connecting stars together.

Key project workflow points:
- Most of the game data/settings are modified using Scriptable Objects;
- Some of the prefab data is set after entering the Play Mode as currently, I do not have a prefab init. tool setup that reads selected SO object data;
- There is a Systems Singleton prefab that controls every Scene, this allows to run LevelScene without its previous initialization from the Initial Scene, etc.;
- I have tried to decouple most connections to a key connection point, for ex. in the LevelScene the GameManager handles most of the connections to the Systems managers.

Extras:
- The game should work on both landscape/portrait aspect ratios. As this makes it so points are sometimes bunched up closely I made the next point always draw on top of the previously clicked points. Unneeded colliders are also disabled;
- Fullscreen windowed/windowed, continue, back, and exit buttons;
- Simple score count (that after the first point is clicked and ends when the last rope starts to get animated);
- Level finish canvas with the score count and continue to the next level button;
- Level selector has a scrollable list that expands depending on the provided data;
- Game runs both on Windows desktop/Android devices.

https://user-images.githubusercontent.com/54512419/202896366-482ec0a9-0da1-48e1-8adc-9b41eec6498e.mp4
