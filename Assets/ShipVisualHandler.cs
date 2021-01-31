using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ShipVisualHandler : MonoBehaviour
{
    [SerializeField]
    private int defaultDisplayLevel;
    [SerializeField]
    private GameObject[] shipParts;

  
    private void Start() => DisplayProgress(defaultDisplayLevel);

    public void DisplayProgress(int displayLevel)
    {
        int partsToShow = Mathf.Min(displayLevel, shipParts.Length);
        for (int i = 0; i < partsToShow; ++i)
        {
            var meshes = shipParts[i].GetComponentsInChildren<MeshRenderer>();
            foreach (var mesh in meshes)
            {
                var color = mesh.material.color;
                color.a = 1f;
                mesh.material.color = color;
            }
        }

        for (int i = partsToShow; i < shipParts.Length; ++i)
        {
            var meshes = shipParts[i].GetComponentsInChildren<MeshRenderer>();
            foreach (var mesh in meshes)
            {
                var color = mesh.material.color;
                color.a = 0.5f;
                mesh.material.color = color;
            }
        }
    }

  
}
