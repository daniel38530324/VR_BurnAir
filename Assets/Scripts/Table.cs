using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Table : MonoBehaviour
{
    public event Action<int> Finished;

    [SerializeField] GameObject wrong_UI;
    [SerializeField] List<GameObject> equipment, wrong;
    [SerializeField] UnityEvent UpdateLevelState, SendChooseFailData;

    int getCount = 0, equipmentCount;
    GameObject[] originEquipment;

    // Start is called before the first frame update
    void Start()
    {
        equipmentCount = equipment.Count;

        originEquipment = new GameObject[equipmentCount];
        equipment.CopyTo(originEquipment);
    }

    private void OnCollisionEnter(Collision collision)
    {
        int removeIndex = -1;

        foreach(GameObject item in equipment)
        {
            if(collision.gameObject == item)
            {
                getCount++;
                removeIndex = equipment.IndexOf(item);
                Finished(Array.IndexOf(originEquipment, collision.gameObject));
            }

            if(getCount == equipmentCount)
            {
                UpdateLevelState.Invoke();
                Finished = null;
                Destroy(this);
            }
        }

        if(removeIndex != -1)
        {
            equipment.Remove(equipment[removeIndex]);
        }
        
        foreach(GameObject item in wrong)
        {
            if(collision.gameObject == item)
            {
                StartCoroutine(Wrong());
                item.SetActive(false);
            }
        }
    }

    IEnumerator Wrong()
    {
        SendChooseFailData.Invoke();
        wrong_UI.SetActive(true);
        AudioManager.Instance.PlaySound("ChoseFail");
        yield return new WaitForSeconds(5);
        wrong_UI.SetActive(false);
    }
}
