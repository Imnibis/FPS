using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
public class LabelAttribute : PropertyAttribute
{
    public readonly string label;

    public LabelAttribute(string label)
    {
        this.label = label;
    }
}