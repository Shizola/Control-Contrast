using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour, IControllable
{
    public bool InControl { get; set; }

    public Material controlMaterial;
    public Material releaseMaterial;

    private Renderer _renderer;
    private Outline _outline;
    

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _renderer = GetComponentInChildren<Renderer>();
    }


    public void ReleaseControl()
    {
        InControl = false;
        Material mat = _renderer.material;
        mat = releaseMaterial;
        _renderer.material = mat;
        _outline.OutlineColor = controlMaterial.color;

        Debug.Log("Release Control Marker");
    }

    public void TakeControl()
    {
        InControl = true;
        Material mat = _renderer.material;
        mat = controlMaterial;
        _renderer.material = mat;
        _outline.OutlineColor = releaseMaterial.color;

        Debug.Log("Take Control Marker");
    }

    [Button]
    public void TestReleaseControl()
    {
        ReleaseControl();
        Debug.Log("Test Release Control Marker");
    }

    [Button]
    public void TestTakeControl()
    {
        TakeControl();
        Debug.Log("Test Take Control Marker");
    }
}
