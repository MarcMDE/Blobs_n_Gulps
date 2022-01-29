using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] Color onSelect;
    Color original;
    Rigidbody rb;

    public bool Taken { get { return taken; } }
    public bool Selected { get { return selected; } }

    bool selected;
    bool taken;

    Renderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
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
    }

    public void Take()
    {
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    public void Release()
    {
        // TODO
    }

    public void Reset()
    {
        taken = false;
        selected = false;

        if (meshRenderer != null)
            meshRenderer.material.color = original;

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }
    }
}
