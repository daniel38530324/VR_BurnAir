using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardScript : MonoBehaviour
{
    public InputField TextField;
    public InputField TextField2;
    public GameObject EngLayoutSml, EngLayoutBig, SymbLayout;

    private int InputState = 0;

    public void Click_InputField(string id)
    {
        if(id == "user")
        {
            InputState = 1;
        }
        else if(id == "pass")
        {
            InputState = 2;
        }
    }

    public void alphabetFunction(string alphabet)
    {
        if(InputState == 1)
        {
            TextField.text = TextField.text + alphabet;
        }
        else if(InputState == 2)
        {
            TextField2.text = TextField2.text + alphabet;
        }
    }

    public void BackSpace()
    {
        if (InputState == 1)
        {
            if (TextField.text.Length > 0) TextField.text = TextField.text.Remove(TextField.text.Length - 1);
        }
        else if (InputState == 2)
        {
            if (TextField2.text.Length > 0) TextField2.text = TextField2.text.Remove(TextField2.text.Length - 1);
        }
    }

    public void CloseAllLayouts()
    {
        EngLayoutSml.SetActive(false);
        EngLayoutBig.SetActive(false);
        SymbLayout.SetActive(false);

    }

    public void ShowLayout(GameObject SetLayout)
    {

        CloseAllLayouts();
        SetLayout.SetActive(true);

    }

}
