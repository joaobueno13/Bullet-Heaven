using UnityEngine;
using UnityEditor;
using System.IO;

public class ExportSprite
{
    [MenuItem("Assets/Export Selected Sprite")]
    static void ExportSelectedSprite()
    {
        Object obj = Selection.activeObject;

        if (obj is Sprite sprite)
        {
            Texture2D tex = sprite.texture;

            Rect rect = sprite.rect;

            Texture2D newTex = new Texture2D((int)rect.width, (int)rect.height);
            Color[] pixels = tex.GetPixels(
                (int)rect.x,
                (int)rect.y,
                (int)rect.width,
                (int)rect.height
            );

            newTex.SetPixels(pixels);
            newTex.Apply();

            byte[] png = newTex.EncodeToPNG();
            string path = EditorUtility.SaveFilePanel(
                "Salvar Sprite",
                Application.dataPath,
                sprite.name + ".png",
                "png"
            );

            if (!string.IsNullOrEmpty(path))
            {
                File.WriteAllBytes(path, png);
                Debug.Log("Sprite exportado para: " + path);
            }
        }
        else
        {
            Debug.LogError("Selecione um Sprite.");
        }
    }
}
