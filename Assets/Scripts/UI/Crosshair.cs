using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Crosshair : MonoBehaviour
{
    public RectTransform lineMinusY;
    public RectTransform linePlusY;
    public RectTransform lineMinusX;
    public RectTransform linePlusX;
    public RectTransform dot;
    
    public RectTransform lineMinusYOutline;
    public RectTransform linePlusYOutline;
    public RectTransform lineMinusXOutline;
    public RectTransform linePlusXOutline;
    public RectTransform dotOutline;

    public Color color;
    public Color outlineColor;
    public int thickness = 3;
    public int outlineThickness = 1;
    public int lineLength = 20;
    public int distanceFromCenter = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lineMinusX.sizeDelta = new Vector2(lineLength, thickness);
        linePlusX.sizeDelta = new Vector2(lineLength, thickness);
        lineMinusY.sizeDelta = new Vector2(thickness, lineLength);
        linePlusY.sizeDelta = new Vector2(thickness, lineLength);
        dot.sizeDelta = new Vector2(thickness, thickness);

        lineMinusXOutline.sizeDelta = new Vector2(lineLength +
            outlineThickness * 2, thickness + outlineThickness * 2);
        linePlusXOutline.sizeDelta = new Vector2(lineLength +
            outlineThickness * 2, thickness + outlineThickness * 2);
        lineMinusYOutline.sizeDelta = new Vector2(thickness +
            outlineThickness * 2, lineLength + outlineThickness * 2);
        linePlusYOutline.sizeDelta = new Vector2(thickness +
            outlineThickness * 2, lineLength + outlineThickness * 2);
        dotOutline.sizeDelta = new Vector2(thickness + outlineThickness * 2,
            thickness + outlineThickness * 2);

        lineMinusXOutline.anchoredPosition = new Vector2(-distanceFromCenter, 0);
        linePlusXOutline.anchoredPosition = new Vector2(distanceFromCenter, 0);
        lineMinusYOutline.anchoredPosition = new Vector2(0, -distanceFromCenter);
        linePlusYOutline.anchoredPosition = new Vector2(0, distanceFromCenter);

        dot.GetComponent<RawImage>().material.color = color;
        dotOutline.GetComponent<RawImage>().material.color = outlineColor;
    }
}
