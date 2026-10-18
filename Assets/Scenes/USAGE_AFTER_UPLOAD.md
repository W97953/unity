Quick instructions to finish the Sunny Land example locally

1. In Unity (recommended version: 2022.3.55f1c1) open this project root.
2. In the Project window you should see SugerWorld.zip at the project root (next to Assets). If you don't, add it there.
3. In the Unity Editor menu: Tools -> Sunny Land -> Extract Art Zip. This will extract the zip to Assets/Art/SugerWorld and refresh the Asset Database.
4. After extraction, run: Tools -> Sunny Land -> Build Example Scene. This will create a basic scene at Assets/Scenes/ExampleScene.unity and a Player prefab at Assets/Prefabs/Player.prefab.

Notes and next steps
- The editor scripts attempt to assign a first-found sprite from the extracted art as the player's sprite. You will likely want to pick the correct sprite and set up animations/Animator.
- Install 2D Tilemap Extras (RuleTile/AnimatedRuleTile/RandomRuleTile) via the Package Manager or by importing the 2d-extras package from Unity-Technologies. Then use the Tile Palette to create RuleTile/RandomTile/AnimatedRuleTile assets from the art in Assets/Art/SugerWorld.
- Open the created scene, select the Tilemap GameObject and paint tiles. The Tilemap has a TilemapCollider2D + CompositeCollider2D setup.
- In the GameManager GameObject, connect UI (Canvas -> Text) to the GameManager.scoreText field if you want on-screen score.

If you want, I can now:
- Programmatically create RuleTile assets if you tell me which sprite names to use for each tile type.
- Create basic Animator & animation clips for the player if you point to the idle/run frames.

