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
            
            if(GUILayout.Button(new GUIContent("Test", "Switch scene to Game"))) 
                SwitchScene("Test");
            if(GUILayout.Button(new GUIContent("City", "Switch scene to Game"))) 
                SwitchScene("City");
            if(GUILayout.Button(new GUIContent("Neon", "Switch scene to Game"))) 
                SwitchScene("Neon");
            if(GUILayout.Button(new GUIContent("Lumosity", "Switch scene to Game"))) 
                SwitchScene("Lumosity");
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