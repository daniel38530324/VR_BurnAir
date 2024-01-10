using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireEX : MonoBehaviour
{
    [SerializeField] GameObject bubble;
    [SerializeField] Rigidbody plug;
    [SerializeField] Image fireEX_Image;
    [SerializeField] Sprite[] fireEX_Sprite;

    private bool pullPlug, hold, aim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHold(bool toggle)
    {
        hold = toggle;
    }

    public void OnAim(bool toggle)
    {
        aim = toggle;
        if (toggle)
        {
            ChangeFireEXImage(2);
        }
        else
        {
            ChangeFireEXImage(1);
        }
    }

    public void OnShoot(bool toogle)
    {
        if(toogle)
        {
            if (pullPlug && hold && aim)
            {
                Debug.Log("µo®g");
                bubble.SetActive(true);
                ChangeFireEXImage(3);
            }
        }
        else
        {
            bubble.SetActive(false);
            ChangeFireEXImage(2);
        }
        
    }

    public void SetPull()
    {
        pullPlug = true;
    }

    public void Pull()
    {
        plug.isKinematic = false;
        ChangeFireEXImage(1);
    }

    public void ChangeFireEXImage(int index)
    {
        fireEX_Image.sprite = fireEX_Sprite[index];
    }
}
