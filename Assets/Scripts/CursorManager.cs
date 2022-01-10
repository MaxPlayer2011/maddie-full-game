using UnityEngine;

namespace GenericManagers.GUI
{
    public static class CursorManager
    {
        public static bool cursorLocked
        {
            get
            {
                return Cursor.lockState == CursorLockMode.Locked;
            }
        }
        
        public static void Lock()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public static void Unlock()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public static void ChangeCursor(Texture2D texture)
        {
            Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
        }

        public static void ResetCursor()
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}
