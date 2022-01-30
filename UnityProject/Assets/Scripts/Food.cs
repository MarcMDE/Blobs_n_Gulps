using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] Color onSelect;
    [SerializeField] float takenSinStrength;
    Color original;
    Rigidbody rb;

    public bool Taken { get { return taken; } }
    public bool Selected { get { return selected; } }

    bool selected;
    bool taken;

    Renderer meshRenderer;

    void Awake()
    {
        meshRenderer = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
        original = meshRenderer.material.color;
    }

    private void OnEnable()
    {
        Reset();
    }

    private void Update()
    {
        if (taken)
        {
            float offset = Mathf.Sin(Time.deltaTime) * takenSinStrength;
            transform.localPosition += new Vector3(0, offset, 0);
        }
    }

    public void Select()
    {
        selected = true;
        meshRenderer.material.color = onSelect;
    }

    public void Take()
    {
        taken = true;
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
