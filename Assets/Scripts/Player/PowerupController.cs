using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerupController : MonoBehaviour
{
    // Start is called before the first frame update
    public Image coolDownImage;
    public Sprite[] coolDownSprites;
    public TextMeshProUGUI coolDownText;

    private float coolDownTime = 15.0f;

    public int divisions = 9;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCooldown(float cooldown) {
        coolDownTime = cooldown;
        coolDownImage.sprite = coolDownSprites[
            Mathf.FloorToInt((coolDownTime / 15.0f) * divisions)
        ];
    }

    public void ShowPoweredUp() {
        coolDownText.enabled = true;
        coolDownImage.sprite = coolDownSprites[0];
    }

    public void HidePoweredUp() {
        coolDownText.enabled = false;
    }
}
