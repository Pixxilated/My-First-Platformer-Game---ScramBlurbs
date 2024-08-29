using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite onSwitch;
    public Sprite offSwitch;

    bool isOn = false;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void FlipSwitch()
    {
        if (isOn) // if on - turn off
        {
            spriteRenderer.sprite = offSwitch;
            isOn = false;
        }
        else // if off - turn on
        {
            spriteRenderer.sprite = onSwitch;
            isOn = true;
        }
    }
}
