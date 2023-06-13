using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetriDish : MonoBehaviour
{
    [SerializeField] Level5Manager level5Manager;
    GameObject steelWool;
    float timer, duration = 4;
    bool isTrigger;
    // Start is called before the first frame update
    void Start()
    {
        steelWool = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(isTrigger)
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("SteelWool"))
        {
            Destroy(collision.gameObject);
            steelWool.SetActive(true);
            isTrigger = true;
            StartCoroutine(UpdateLevelState());
        }
    }

    IEnumerator UpdateLevelState()
    {
        yield return new WaitForSeconds(5);
        level5Manager.UpdateLevel5State(Level5State.Water);
        isTrigger = false;
    }
}
