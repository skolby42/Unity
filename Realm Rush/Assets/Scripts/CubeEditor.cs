using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
public class CubeEditor : MonoBehaviour
{
    [SerializeField] [Range(1f, 20f)] float gridSize = 10;

    TextMesh textMesh;

    void Update()
    {
        SetSnapPosition();
        UpdateText();
    }

    private void SetSnapPosition()
    {
        Vector3 snapPos;
        snapPos.x = Mathf.RoundToInt(transform.position.x / gridSize) * gridSize;
        snapPos.y = 0f;  // Disable snap in Y axis
        snapPos.z = Mathf.RoundToInt(transform.position.z / gridSize) * gridSize;

        transform.position = snapPos;
    }

    private void UpdateText()
    {
        textMesh = GetComponentInChildren<TextMesh>();

        float gridX = transform.position.x / gridSize;
        float gridZ = transform.position.z / gridSize;

        textMesh.text = $"{gridX},{gridZ}";
    }
}
