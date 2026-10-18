This folder describes how to create the example scene locally.

Quick local scene setup:
1. In Unity, create a new Scene and save it as Assets/Scenes/ExampleScene.unity.
2. Add a Grid -> Tilemap (Rectangular). Add a Tilemap Renderer component and Tilemap Collider 2D + CompositeCollider2D + Rigidbody2D (set to Static) for better collision performance.
3. Create Player GameObject: SpriteRenderer, Rigidbody2D, CapsuleCollider2D (or Box), PlayerController script. Add an empty child object at player's feet and assign to groundCheck.
4. Create Canvas with a Text element and assign it to GameManager.scoreText. Create a GameManager GameObject and attach GameManager script.
5. Create Collectible prefabs (Sprite + CircleCollider2D isTrigger + Collectible script).

After you add your art into Assets/Art/, create Tile assets and paint the level.
