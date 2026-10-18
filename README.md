# Nickest Jump

A 2D endless runner built with Unity and C#. Start a run, jump over incoming enemies, and try to beat your personal best as the game speeds up.

## Features

- Single-action controls for starting, jumping, and restarting.
- Animated player with sound effects and dust particles.
- Repeating enemy generation and scrolling parallax scenery.
- Increasing difficulty through a progressively faster game time scale.
- Current score and a personal best stored locally with Unity `PlayerPrefs`.
- Localized HUD text using Unity Localization.

## Requirements

- Unity Hub and **Unity Editor 6000.6.6f1**, the version recorded in [ProjectVersion.txt](ProjectSettings/ProjectVersion.txt).
- Internet access for the initial Unity package restore.
- The Unity build support module for your intended target platform, if creating a build.

Package dependencies are declared in [Packages/manifest.json](Packages/manifest.json) and restored by Unity when the project opens.

## Getting started

1. Clone or download this repository.
2. In Unity Hub, add the repository root as an existing project. Select the folder containing `Assets`, `Packages`, and `ProjectSettings`.
3. Open it with Unity **6000.6.6f1**.
4. Wait for Unity to import assets, restore packages, and compile scripts.
5. Open [Assets/Scenes/Main.unity](Assets/Scenes/Main.unity).
6. Enter Play mode and focus the Game view.

## Controls

Use **Space**, **Up Arrow**, or the **left mouse button** for all actions:

| Game state | Action |
| --- | --- |
| Waiting to start | Start a run |
| Playing | Jump, if a jump is not already in progress |
| After the death animation finishes | Reload the scene to prepare another run |

After reloading, press a control again to start the next run. Colliding with an enemy ends the current run; entering a point trigger adds one point.

## Creating a build

1. Open `Assets/Scenes/Main.unity`.
2. Open **File > Build Profiles** in Unity.
3. Create or select a profile for your target platform and install its build support module if needed.
4. Ensure `Assets/Scenes/Main.unity` is enabled in the profile's scene list and is the first scene.
5. Select **Build** or **Build and Run** and choose an output folder such as `Builds/`.

Keep the scene name `Main` unless you also update the restart logic in `GameController`, which reloads it by name. The `Build/` and `Builds/` folders are ignored by Git.

## Project structure

```text
Assets/
  AddressableAssetsData/  Addressables configuration
  Animaciones/           Player animations and animation controllers
  Audio/                 Music and sound effects
  Backgrounds/           Background artwork
  Fonts/                 UI fonts
  Localization/          Localization assets
  Prefab/                Reusable game objects
  Scenes/                Main gameplay scene
  Scripts/
    Enemies/             Enemy movement and spawning
    Game/                Game state and run lifecycle
    Input/               Keyboard and mouse input
    Player/              Player actions, animation, and collision handling
    Presentation/        HUD and parallax scrolling
    Scoring/             Current score and best-score persistence
  Sprites/               Sprite artwork
Packages/                Unity package manifest and lock file
ProjectSettings/         Shared Unity project configuration
```

## Development

Gameplay components communicate through C# events. `GameController` coordinates input, the player, enemy spawning, scrolling, the HUD, and scoring. Tune serialized component fields in the Inspector, including enemy speed and spawn interval, parallax speed, and the time-scale increase interval and amount.

Player jumps and death completion depend on animation callbacks in `PlayerController`. Preserve those animation events when editing clips. Collision handling uses the `Enemigo`, `Puntos`, and `Destructor` tags.

When contributing:

- Keep changes focused and describe how you verified them.
- Commit asset changes together with their `.meta` files to preserve Unity references.
- Commit intentional package and project setting changes.
- Leave generated folders such as `Library`, `Temp`, `Logs`, and build outputs out of version control.

### Manual verification

For gameplay changes, run the main scene and check that:

1. Each supported control starts a run and triggers a jump during play.
2. A second jump cannot begin while the player is already jumping.
3. Point triggers increase the score and update the best score when exceeded.
4. An enemy collision ends the run, stops spawning and scrolling, and plays the death animation.
5. Input after the death animation reloads the scene, and a new run can start.
6. The best score remains available after restarting the application.

## Troubleshooting

- **Packages or scripts fail to load:** confirm the editor version matches `ProjectVersion.txt`, let package restoration finish, and inspect Unity's Console and Package Manager for errors.
- **Controls do not respond in the editor:** focus the Game view and check the Console for errors. Restart input is accepted only after the death animation finishes.
- **Restart fails in a build:** ensure the scene named `Main` is enabled in the active build profile's scene list.

## License

No project-level license is currently included. Obtain permission from the rights holders before reusing or distributing the project's code or assets. Third-party components may include their own license files.
