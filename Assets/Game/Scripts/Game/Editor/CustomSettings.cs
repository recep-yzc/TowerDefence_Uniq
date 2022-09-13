using Game.Utilities;
using UnityEditor;
using UnityEngine;

namespace Game.Editor
{
    public class CustomSettings : ScriptableObject
    {
        #region ClearAllPlayerPrefs

        [MenuItem("CustomSettings/Clear All PlayerPrefs", priority = 0)]
        public static void ClearAllPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("Cleared!");
        }

        #endregion

        #region ShowFPS

        private const string MenuName = "CustomSettings/Show FPS";
        private static bool _isToggled;

        static CustomSettings()
        {
            EditorApplication.delayCall += () =>
            {
                _isToggled = EditorPrefs.GetBool(MenuName, true);
                SetMode(_isToggled);
                Menu.SetChecked(MenuName, _isToggled); // This doesn't appear to work
            };
        }

        [MenuItem("CustomSettings/Show FPS", priority = 1)]
        private static void ToggleMode()
        {
            _isToggled = !_isToggled;
            SetMode(_isToggled);
            Menu.SetChecked(MenuName, _isToggled);
            EditorPrefs.SetBool(MenuName, _isToggled);
        }

        private static void SetMode(bool value)
        {
            if (value)
            {
            }
            else
            {
            }

            FindObjectOfType<FPS>().enabled = value;
        }

        #endregion
    }
}