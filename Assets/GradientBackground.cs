using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GradientBackground : MonoBehaviour
{
    public Color startColor = Color.red;
    public Color endColor = Color.blue;


    public void Awake()
    {
        var mesh = GetComponent<MeshFilter>().sharedMesh;
        var colors = new Color[mesh.vertices.Length];
        colors[0] = startColor;
        colors[1] = endColor;
        colors[2] = startColor;
        colors[3] = endColor;
        mesh.colors = colors;
    }

}
