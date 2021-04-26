using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageLifeController : MonoBehaviour
{
    [SerializeField] private Sprite _spriteLifeEmpty;
    [SerializeField] private Sprite _spriteLifeFull;

    private Image _image;

    private bool _isEmpty = true;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.sprite = _spriteLifeEmpty;
    }
    
    public void SetLifeFull()
    {
        if (_isEmpty)
        {
            _isEmpty = false;

            _image.sprite = _spriteLifeFull;
        }
    }
    
    public void SetLifeEmpty()
    {
        if (!_isEmpty)
        {
            _isEmpty = true;

            _image.sprite = _spriteLifeEmpty;
        }
    }
}
