using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageEffectBehaviour : MonoBehaviour
{
    [SerializeField] Material renderMaterial;
    void OnRenderImage (RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit (source, destination, renderMaterial);
    }
}
