using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySpriteRendererOnStart : MonoBehaviour
{
    private void Start()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            Destroy(spriteRenderer);
        }
    }
}
