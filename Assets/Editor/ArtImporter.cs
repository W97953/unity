using UnityEditor;
using UnityEngine;
using System.IO;
#if UNITY_2020_1_OR_NEWER
using System.IO.Compression;
#endif

public static class ArtImporter
{
    [MenuItem("Tools/Sunny Land/Extract Art Zip")]
    public static void ExtractArtZip()
    {
        string projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
        string zipPath = Path.Combine(projectRoot, "SugerWorld.zip");
        if (!File.Exists(zipPath))
        {
            EditorUtility.DisplayDialog("SugerWorld.zip not found", "Could not find SugerWorld.zip in the project root. Please add the zip to the repository root or provide the correct path.", "OK");
            return;
        }

        string dest = Path.Combine(Application.dataPath, "Art", "SugerWorld");
        try
        {
#if UNITY_2020_1_OR_NEWER
            if (Directory.Exists(dest)) Directory.Delete(dest, true);
            Directory.CreateDirectory(dest);
            ZipFile.ExtractToDirectory(zipPath, dest);
#else
            EditorUtility.DisplayDialog("Unsupported Unity version", "This extractor requires Unity 2020 or newer for System.IO.Compression.ZipFile support. Please extract the zip manually into Assets/Art/SugerWorld.", "OK");
            return;
#endif
            AssetDatabase.Refresh();
            EditorUtility.DisplayDialog("Done", "SugerWorld.zip extracted to Assets/Art/SugerWorld.\nNow run Tools > Sunny Land > Build Example Scene.", "OK");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to extract zip: " + e);
            EditorUtility.DisplayDialog("Error", "Failed to extract zip: " + e.Message, "OK");
        }
    }
}
