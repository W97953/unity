using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.IO;

public static class SunnyExampleBuilder
{
    [MenuItem("Tools/Sunny Land/Build Example Scene")]
    public static void BuildExampleScene()
    {
        // Check that art exists
        string artPath = "Assets/Art/SugerWorld";
        if (!AssetDatabase.IsValidFolder(artPath))
        {
            if (!EditorUtility.DisplayDialog("Art not found", "Assets/Art/SugerWorld not found. Please run Extract Art Zip first or add your art to Assets/Art/SugerWorld.\nDo you want to continue and create a skeleton scene without art?", "Continue", "Cancel"))
                return;
        }

        // Create Scenes folder
        if (!AssetDatabase.IsValidFolder("Assets/Scenes"))
            AssetDatabase.CreateFolder("Assets", "Scenes");
        if (!AssetDatabase.IsValidFolder("Assets/Prefabs"))
            AssetDatabase.CreateFolder("Assets", "Prefabs");

        // Create new scene
        Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        // Create Grid and Tilemap
        GameObject grid = new GameObject("Grid");
        grid.AddComponent<UnityEngine.Tilemaps.Grid>();

        GameObject tilemapGO = new GameObject("Tilemap");
        tilemapGO.transform.parent = grid.transform;
        var tilemap = tilemapGO.AddComponent<UnityEngine.Tilemaps.Tilemap>();
        tilemapGO.AddComponent<UnityEngine.Tilemaps.TilemapRenderer>();

        // Add tilemap collider & composite
        var tilemapCollider = tilemapGO.AddComponent<UnityEngine.Tilemaps.TilemapCollider2D>();
        var rb = tilemapGO.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        var comp = tilemapGO.AddComponent<CompositeCollider2D>();
        tilemapCollider.usedByComposite = true;

        // Create Player
        GameObject player = new GameObject("Player");
        var sr = player.AddComponent<SpriteRenderer>();
        // Attempt to assign a sprite from art (first found)
        if (AssetDatabase.IsValidFolder(artPath))
        {
            string[] guids = AssetDatabase.FindAssets("t:Sprite", new[] { artPath });
            if (guids.Length > 0)
            {
                var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(AssetDatabase.GUIDToAssetPath(guids[0]));
                if (sprite) sr.sprite = sprite;
            }
        }
        var rb2 = player.AddComponent<Rigidbody2D>();
        rb2.freezeRotation = true;
        player.AddComponent<CapsuleCollider2D>().direction = CapsuleDirection2D.Vertical;

        // Add PlayerController script if available
        var playerControllerType = typeof(MonoBehaviour);
        var scripts = AssetDatabase.FindAssets("PlayerController t:Script");
        if (scripts.Length > 0)
        {
            string path = AssetDatabase.GUIDToAssetPath(scripts[0]);
            var script = AssetDatabase.LoadAssetAtPath<MonoScript>(path);
            if (script != null)
            {
                player.AddComponent(script.GetClass());
            }
        }

        // groundCheck child
        GameObject groundCheck = new GameObject("GroundCheck");
        groundCheck.transform.parent = player.transform;
        groundCheck.transform.localPosition = new Vector3(0, -0.6f, 0);

        // Tag player (create tag if missing)
        try
        {
            player.tag = "Player";
        }
        catch {
            // create tag
            AddTag("Player");
            player.tag = "Player";
        }

        // Create GameManager
        GameObject gm = new GameObject("GameManager");
        var gmScripts = AssetDatabase.FindAssets("GameManager t:Script");
        if (gmScripts.Length > 0)
        {
            string path = AssetDatabase.GUIDToAssetPath(gmScripts[0]);
            var script = AssetDatabase.LoadAssetAtPath<MonoScript>(path);
            if (script != null)
                gm.AddComponent(script.GetClass());
        }

        // Save Player prefab
        string prefabPath = "Assets/Prefabs/Player.prefab";
        PrefabUtility.SaveAsPrefabAsset(player, prefabPath);

        // Save Scene
        string scenePath = "Assets/Scenes/ExampleScene.unity";
        EditorSceneManager.SaveScene(scene, scenePath);

        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("Done", "Example scene created at Assets/Scenes/ExampleScene.unity and Player prefab at Assets/Prefabs/Player.prefab.\nOpen the scene in Unity and finish assigning Tile assets / Tilemap rules as needed.", "OK");
    }

    static void AddTag(string tag)
    {
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty tagsProp = tagManager.FindProperty("tags");

        // check if tag exists
        for (int i = 0; i < tagsProp.arraySize; i++)
        {
            SerializedProperty t = tagsProp.GetArrayElementAtIndex(i);
            if (t.stringValue.Equals(tag)) return;
        }

        tagsProp.InsertArrayElementAtIndex(tagsProp.arraySize);
        SerializedProperty newTag = tagsProp.GetArrayElementAtIndex(tagsProp.arraySize - 1);
        newTag.stringValue = tag;
        tagManager.ApplyModifiedProperties();
    }
}
