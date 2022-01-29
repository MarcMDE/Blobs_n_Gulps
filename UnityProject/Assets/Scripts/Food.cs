using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] Color onSelect;
    Color original;

    public bool Taken { get { return taken; } }
    public bool Selected { get { return selected; } }

    bool selected;
    bool taken;

    Renderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<Renderer>();
        original = meshRenderer.material.color;
    }

    private void OnEnable()
    {
        Reset();
    }

    public void Select()
    {
        selected = true;
        meshRenderer.material.color = onSelect;
        //meshRenderer.material = onSelect;
    }

    public void Take()
    {

    }

    public void Reset()
    {
        taken = false;
        selected = false;

        if (meshRenderer != null)
            meshRenderer.material.color = original;
    }
}
