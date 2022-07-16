using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityToolbarExtender;

namespace Editors
{
    [InitializeOnLoad]
    public class SceneSwitchRightButton
    {
        static SceneSwitchRightButton()
        {
            ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUI);
        }

        static void OnToolbarGUI()
        {
            GUILayout.FlexibleSpace();
            
            if(GUILayout.Button(new GUIContent("Game", "Switch scene to Game"))) 
                SwitchScene("Game");
        }

        public static void SwitchScene(string sceneName)
        {
            string[] guids = AssetDatabase.FindAssets("t:scene " + sceneName, null);

            if (guids.Length == 0)
                Debug.LogWarning("Couldn't find scene file");
            else
            {
                string scenePath = AssetDatabase.GUIDToAssetPath(guids[0]);
                EditorSceneManager.OpenScene(scenePath);
            }
        }
    }
}