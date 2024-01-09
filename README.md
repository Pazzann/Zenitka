# Zenitka

Since February 24, 2022, the enemy has used tens of thousands of missiles, drones, shells, as well as hundreds of aircraft on our territory. Many of them, due to the shortage of anti-aircraft systems, hit military and residential facilities. Unfortunately, Ukraine does not have enough of its own means of air defense, so their development is currently more appropriate and relevant than ever.

The project is an anti-air system tester, created in accordance with the task of the Tournament of Young Informatics 2023-2024. It implements detailed simulation parameters (According to task numbers: 1.1, 1.2, 1.3, 1.4, 1.5, 1.7, 2.4, 3.1, 3.2, 3.3, 3.4, 4.1, 4.3, 4.5, 5.1, 5.2, 5.3, 5.4, 5.5 ), 2D and 3D visualization (and a control panel for their parameters), and other features.

## System Requirements

For the best experience your device is recommended to meet the following requierements:

- **Processor**: > 1 GHz
- **Processor cores number**: > 1
- **RAM**: > 500 MB
- **Free disk space**: > 350 MB
- **OS:** Windows 11 or higher(Windows 10 might work but was not tested)

## Installation and Running

To install Zenitka, you may use the executable file, or you may assemble the project yourself.

If using an **executable file**, just download it, run it as an administrator and follow the on-screen instructions.

To assemble and run the project **manually**:

1. Install **Godot 4.1.1 Mono**:
    - Download the file from the [link](https://godotengine.org/download/archive/4.1.1-stable/);
    - Run the file;
2. Install **Blender 3.x** through Steam.
3. Set path in Godot to `blender.exe`.
4. Import **Zenitka** into Godot.
5. Open **Zenitka**.
6. Run the project by pressing `play` in the top right corner or press `F5` on the keyboard.

## Implementation details

The project is written in C# using Godot as a game engine.

C# was chosen as it's more convenient for writing simulations or big scenes with multiple objects due to its OOP functions. Godot is used to make the whole process much easier.

## Controls

#### 2D Controls
- **Zooming in/out:** `mouse wheel`
- **Camera Mods switch:** `q` and `e`.
- **Change Speed of simulation:** `+` and `-`.
- **Pause Menu:** `Escape`
- **Settings Panel:** Press left top corner button.

#### 3D Controls
- **Fre fly camera:**
  - **Movement:** `w` `a` `s` `d`
  - **Enter/Leave Rotation Mode:** `left mouse button`/`right mouse button`
  - **Rotate Camera:** Move mouse
- **Camera Mods switch:** `Tab`.
- **Change Speed of simulation:** `+` and `-`.
- **Pause Menu:** `Escape`
- **Settings Panel:** Press left top corner button.

## Contents

The project structure:

<!-- UNDESCRIBED: assets, docs -->

- Assets
    - Fonts
        - VeniceClassic.ttf
    - Materials
        - Explosion.tres
    - Models
        - AntiAir.blend
        - Bullet.blend
        - Bullet.fbx
        - Patriot.fbx
        - Rocket.blend
    - Shaders
        - Explosion.gdshader
    - Sprites
        - Bullet
            - Bullet.png
            - Bullet1.png
            - Bullet2.png
            - Bullet3.png
            - Bullet4.png
            - Bullet5.png
            - Bullet6.png
            - Bullet7.png
        - Rocket
            - Rocket1.png
            - Rocket2.png
            - Rocket3.png
        - Rocket2
            - Rocket1.png
            - Rocket2.png
            - Rocket3.png
            - Rocket4.png
        - RocketExplosion
            - RocketExplosion1.png
            - RocketExplosion2.png
            - RocketExplosion3.png
            - RocketExplosion4.png
            - RocketExplosion5.png
            - RocketExplosion6.png
            - RocketExplosion7.png
            - RocketExplosion8.png
        - Bullet.aseprite
        - Rocket.aseprite
        - Rocket2.aseprite
        - RocketExplosion.aseprite
        - RocketGun.aseprite
        - RocketGun.png
        - Shahed.aseprite
        - Shahed.png
        - Target.aseprite
        - Target.png
        - Zenitka.aseprite
        - Zenitka2.aseprite
        - Zenitka3.aseprite
        - Zenitka4.aseprite
        - Zenitka5.aseprite
        - Zenitka.png
        - Zenitka2.png
        - Zenitka3.png
        - Zenitka4.png
        - Zenitka5.png
    - Styles
        - AntiAirUnScripted(1).blend
        - arrow.png
        - background.png
        - base.tres
        - button cop.png
        - button.png
        - button_hoover.png
        - button_pressed.png
        - icon_settings.svg
        - icons-settings.svg
        - settings_icon.png
    - ammo_rocketModern.mtl
    - bg.jpg
    - logo.jpg
    - rocketLauncherModern.mtl
    - rocketLauncherModernRotating.mtl
- Prefabs
    - 2D
      - Bullet.tscn
      - Cannon.tscn
      - Rocket1.tscn
      - Rocket2.tscn
      - RocketCannon.tscn
      - Target.tscn
    - 3D
      - Bullet.tscn
      - Cannon.tscn
      - Explosion.tscn
      - RocketLauncher.tscn
      - Target.tscn
    - UI
      - Menu.tscn
      - SettingsPanel.tscn
      - SettingsPanel3D.tscn
- Scenes
    - CannonType.tscn
    - DefaultCannon2DUI.tscn
    - DefaultTarget2DUI.tscn
    - Main2D.tscn
    - Main3D.tscn
    - MainUI.tscn
    - Rocket.blend
    - RocketCannon2DUI.tscn
    - RocketTarget2DUI.tscn
    - Settings.tscn
    - TargetType.tscn
- Scripts
    - 2D
      - Targets
        - Bullet.cs
        - Rocket1.cs
        - Rocket2.cs
        - Shell.cs
      - BallisticBody.cs
      - Camera.cs
      - Camera2DTarget.cs
      - Cannon.cs
      - Main2D.cs
      - Solver2D.cs
    - 3D
      - BallisticBody.cs
      - Bullet.cs
      - Camera3DFly.cs
      - CameraTarget.cs
      - CameraTarget2.cs
      - Cannon.cs
      - Explosion.cs
      - IWeapon.cs
      - Main3D.cs
      - Player.cs
      - RocketLauncher.cs
      - Solver3D.cs
      - Target.cs
    - UI
      - CannonType.cs
      - DefaultCannon2DUI.cs
      - DefaultTarget2DUI.cs
      - MainUI.cs
      - Menu.cs
      - RocketCannon2DUI.cs
      - RocketTarget2DUI.cs
      - SettingsPanel.cs
      - SettingsPanel3D.cs
      - Statistics.cs
      - TargetType.cs
    - MainUtils.cs
    - Settings.cs
- Theory
  - BulletTrajectory.ai
  - Logo.ai
  - Logo.svg
  - Test1.nb
  - Test2.nb
  - ti.nb
- .gitattributes
- .gitignore
- README.md
- Zenitka.csproj
- Zenitka.sln
- icon.svg
- main.tscn
- project.godot
   
