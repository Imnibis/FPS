using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[ExecuteInEditMode]
public class UIRectangle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Texture2D tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, Color.white);
        tex.Apply();
        File.WriteAllBytes(@"C:\Users\imnib\Desktop\rect.png", tex.EncodeToPNG());
    }
}
