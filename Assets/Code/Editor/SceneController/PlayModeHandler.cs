using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine.SceneManagement;

namespace Code.Editor.SceneController
{
    [InitializeOnLoad]
    public static class PlayModeHandler
    {
        private const string LastOpenedSceneKey = "editor_last_opened_scene";
        private const string LastOpenedPrefabKey = "editor_last_opened_prefab";
        
        private static string LastOpenedScene
        {
            get => PlayerPrefs.GetString(LastOpenedSceneKey, null);
            set => PlayerPrefs.SetString(LastOpenedSceneKey, value);
        }

        private static string LastOpenedPrefab
        {
            get => PlayerPrefs.GetString(LastOpenedPrefabKey, null);
            set => PlayerPrefs.SetString(LastOpenedPrefabKey, value);
        }
        
        static PlayModeHandler()
        {
            EditorApplication.playModeStateChanged += EditorApplication_PlayModeStateChanged;
        }
        
        public static void HandleExitingEditMode()
        {
            SaveActivePrefab();
            SaveActiveScene();
        }


        private static void HandleEnteredEditMode()
        {
            ReopenSavedScene();
            ReopenSavedPrefab();
        }
        
        private static void SaveActiveScene()
        {
            string path = SceneManager.GetActiveScene().path;
            string guid = AssetDatabase.AssetPathToGUID(path);

            LastOpenedScene = guid;

            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        }
        
        private static void ReopenSavedScene()
        {
            if (string.IsNullOrEmpty(LastOpenedScene))
                return;

            string path = AssetDatabase.GUIDToAssetPath(LastOpenedScene);
            EditorSceneManager.OpenScene(path);
        }
        
        private static void SaveActivePrefab()
        {
            PrefabStage stage = PrefabStageUtility.GetCurrentPrefabStage();

            if (stage != null)
            {
                string path = stage.assetPath;
                string guid = AssetDatabase.AssetPathToGUID(path);

                LastOpenedPrefab = guid;
            }
            else
                LastOpenedPrefab = null;
        }

        private static void ReopenSavedPrefab()
        {
            if (string.IsNullOrEmpty(LastOpenedPrefab))
                return;

            string path = AssetDatabase.GUIDToAssetPath(LastOpenedPrefab);
            Object prefabObject = AssetDatabase.LoadAssetAtPath<Object>(path);
            AssetDatabase.OpenAsset(prefabObject);
        }

        private static void EditorApplication_PlayModeStateChanged(PlayModeStateChange playModeState)
        {
            if (playModeState == PlayModeStateChange.EnteredEditMode) 
                HandleEnteredEditMode();
        }
    }
}