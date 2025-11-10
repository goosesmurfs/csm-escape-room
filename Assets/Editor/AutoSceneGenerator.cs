using UnityEngine;
using UnityEditor;

/// <summary>
/// Automatically checks if scenes exist and offers to generate them
/// Runs when Unity Editor starts
/// </summary>
[InitializeOnLoad]
public class AutoSceneGenerator
{
    static AutoSceneGenerator()
    {
        // Check if scenes exist
        EditorApplication.delayCall += CheckAndOfferGeneration;
    }

    static void CheckAndOfferGeneration()
    {
        // Only run once per session
        if (SessionState.GetBool("ScenesChecked", false))
            return;

        SessionState.SetBool("ScenesChecked", true);

        // Check if scenes exist
        bool levelSelectExists = System.IO.File.Exists("Assets/Scenes/LevelSelect.unity");
        bool questionSceneExists = System.IO.File.Exists("Assets/Scenes/QuestionScene.unity");

        if (!levelSelectExists || !questionSceneExists)
        {
            if (EditorUtility.DisplayDialog("Generate Scenes?",
                "Unity scenes not found. Would you like to auto-generate them now?\n\n" +
                "This will create:\n" +
                "- LevelSelect.unity\n" +
                "- QuestionScene.unity",
                "Yes, Generate", "No, I'll do it manually"))
            {
                SceneGenerator.GenerateAllScenes();
            }
        }
    }
}
