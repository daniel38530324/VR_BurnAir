using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Table : MonoBehaviour
{
    [SerializeField] List<GameObject> equipment;
    [SerializeField] UnityEvent UpdateLevelState;

    int getCount = 0, equipmentCount;
    // Start is called before the first frame update
    void Start()
    {
        equipmentCount = equipment.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
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
            }

            if(getCount == equipmentCount)
            {
                UpdateLevelState.Invoke();
                Destroy(this);
            }
        }

        if(removeIndex != -1)
        {
            equipment.Remove(equipment[removeIndex]);
        }
        
    }
}
