# DiscoBall · VR Music Interaction

**A virtual reality prototype exploring music, visual feedback, and interaction in a 3D space.**

DiscoBall lets users interact with audio sources and their visual representations. Its scripts connect sound playback, material changes, volume controls, and character animation to create an audiovisual experience.

## Interaction highlights

- Toggle audio playback alongside changes in object appearance.
- Adjust audio volume and character animation speed.
- Switch between desktop and Oculus player controllers.
- Explore proximity-based interaction hints and VR UI.

## Technology

| Area | Stack |
| --- | --- |
| Engine | Unity **2022.3.16f1** |
| Code | C#, UniRx |
| XR | XR Interaction Toolkit 2.5.2, Oculus XR Plugin 4.1.2, bundled Oculus integration |
| Rendering | Universal Render Pipeline 14.0.9 |

## Open the project

1. Install Unity **2022.3.16f1** with the build modules needed for your headset/platform.
2. Open the repository root through Unity Hub and allow packages to import.
3. Open `Assets/Scenes/SampleScene.unity`.
4. Review XR Plug-in Management and the scene's active controller rig for the intended runtime.
5. Connect a compatible headset for VR testing, or inspect the desktop controller path in the editor.

The project includes both desktop and Oculus controller paths; compatibility with a specific headset has not been verified during publication.

## Code to explore

| Script | Responsibility |
| --- | --- |
| `Assets/scripts/AudioSourceManager.cs` | Playback, volume, materials, and animation speed |
| `Assets/scripts/ColorChangeManager.cs` | Keyboard-triggered audio/visual toggles |
| `Assets/scripts/ControllerSwitcher.cs` | Desktop/VR controller switching |
| `Assets/scripts/Interactable.cs` | Interaction events and hints |
| `Assets/scripts/VRUIInput.cs` | VR interface input |

## Prototype status

`VRMovementController.cs` contains an unfinished InputAction setup: `moveAction` is used without being assigned. If that component is enabled in your scene, its action binding must be completed before use. This publication preserves the prototype and does not claim a verified headset build.

## Credits and source scope

Published from the supplied local Unity project; no existing Git history was present. Unity-generated caches and compiled builds are excluded. Bundled Oculus, Unity sample, UniRx, and other third-party material retain their original licenses and notices. No blanket license is added for third-party assets.
