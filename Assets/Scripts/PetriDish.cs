using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetriDish : MonoBehaviour
{
    [SerializeField] Level5Manager level5Manager;
    [SerializeField] GameObject steelWool;
    [SerializeField] GameObject warn_UI;
    float timer, duration = 4;
    bool isTrigger;

    void Update()
    {
        if(isTrigger && steelWool != null)
        {   
            if (timer < duration)
            {
                float t = timer / duration;
                steelWool.GetComponent<Renderer>().material.color = Color.Lerp(Color.black, new Color(0.3207547f, 0.2617479f, 0.2617479f), t);
                timer += Time.deltaTime;
            }
            else
            {
                steelWool.GetComponent<Renderer>().material.color = new Color(0.3207547f, 0.2617479f, 0.2617479f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SteelWool") && !isTrigger && other.name == "Steel Wool")
        {
            Destroy(other.gameObject);
            steelWool.SetActive(true);
            isTrigger = true;
            StartCoroutine(UpdateLevelState(Level5State.Water));
        }

        if (other.CompareTag("H2O2") && !isTrigger)
        {
            StartCoroutine(UpdateLevelState(Level5State.Bag1));
        }

        if (other.CompareTag("Vinegar") && !isTrigger)
        {
            StartCoroutine(ReturnState(Level5State.Vinegar));
        }
    }

    IEnumerator ReturnState(Level5State returnState)
    {
        isTrigger = true;
        warn_UI.SetActive(true);
        yield return new WaitForSeconds(3);
        level5Manager.ReturnLevelState(returnState);
        warn_UI.SetActive(false);
        isTrigger = false;
    }

    IEnumerator UpdateLevelState(Level5State state)
    {
        yield return new WaitForSeconds(2);
        level5Manager.UpdateLevel5State(state);
        isTrigger = false;
    }
}
