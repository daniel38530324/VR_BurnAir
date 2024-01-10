using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Robot_PC : MonoBehaviour
{
    [SerializeField] Level6State_PC level6State;
    [SerializeField] Level6Manager_PC level6Manager;
    [SerializeField] Material[] materials;
    [SerializeField] bool isHead;
    [SerializeField] MeshRenderer robot, plastic_food;
    [SerializeField] GameObject robot_Old;
    bool timerTrigger = true;
    bool trigger;
    float timer;

    private void Awake()
    {
        if(level6State == Level6State_PC.Lemonade)
        {
            int index = -1;
            foreach (Material item in materials)
            {
                index++;
                if (index == 4)
                {
                    item.color = new Color(0.5f, 0.5f, 0.5f, 1);
                }
                else if (index == 7)
                {
                    item.color = new Color(0.857f, 0.7910824f, 0f, 1);
                }
                else if (index == 10)
                {
                    item.color = new Color(0f, 0.0961f, 0.5f, 1);
                }
                else if (index == 12)
                {
                    item.color = new Color(0.6792453f, 0.05446777f, 0.05446777f, 1);
                }
                else
                {
                    item.color = new Color(1, 1, 1, 1);
                }
            }

            robot_Old.SetActive(true);
        }
        if (level6State == Level6State_PC.WD40)
        {
            level6Manager.OnWD40Num += WD40;
        }
        if(level6State == Level6State_PC.PaintGun)
        {
            level6Manager.OnPaintGunNum += PaintGun;
        }
        if(level6State == Level6State_PC.PlasticSleeve)
        {
            level6Manager.OnPlasticSleeveNum += PlasticSleeve;
        }
    }
    private void Start()
    {
        
    }

    private void OnDestroy()
    {
        if (level6State == Level6State_PC.WD40)
        {
            level6Manager.OnWD40Num -= WD40;
        }
        if (level6State == Level6State_PC.PaintGun)
        {
            level6Manager.OnPaintGunNum -= PaintGun;
        }
        if (level6State == Level6State_PC.PlasticSleeve)
        {
            level6Manager.OnPlasticSleeveNum -= PlasticSleeve;
        }
    }

    private void Update() {
        if(timerTrigger == false){
            timer += Time.deltaTime;

            if (timer >= 3 && !trigger)
            {
                StartCoroutine(Rag());
                trigger = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Lemonade"))
        {
            StartCoroutine(Lemonade());
        }
        else if (other.CompareTag("WD40"))
        {
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            level6Manager.UpdateNum(Level6State_PC.WD40);
        }
        else if (other.CompareTag("PaintGun"))
        {
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            if(isHead)
            {
                materials[3].color = new Color(0, 0.3268819f, 1, 1);
                materials[4].color = new Color(0, 0.3268819f, 1, 1);
            }
            else
            {
                materials[0].color = new Color(0, 0.3268819f, 1, 1);
            }
            level6Manager.UpdateNum(Level6State_PC.PaintGun);
        }
        else if (other.CompareTag("PlasticSleeve"))
        {
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            plastic_food.enabled = true;
            level6Manager.UpdateNum(Level6State_PC.PlasticSleeve);
        }
        else if (other.CompareTag("Rag"))
        {
            if (!timerTrigger) return;

            timerTrigger = false;
        }
    }

    IEnumerator Lemonade()
    {
        yield return new WaitForSeconds(3);
        level6Manager.UpdateLevel6State(Level6State_PC.Rag);
    }

    IEnumerator Rag()
    {
        GetComponent<BoxCollider>().enabled = false;
        robot.enabled = true;
        robot_Old.SetActive(false);
        yield return new WaitForSeconds(5);
        level6Manager.UpdateLevel6State(Level6State_PC.WD40);
    }

    void WD40()
    {
        StartCoroutine(WD40_Success());
    }

    IEnumerator WD40_Success()
    {
        yield return new WaitForSeconds(5);
        level6Manager.UpdateLevel6State(Level6State_PC.PaintGun);
    }

    void PaintGun()
    {
        StartCoroutine(PaintGun_Success());
    }

    IEnumerator PaintGun_Success()
    {
        yield return new WaitForSeconds(5);
        level6Manager.UpdateLevel6State(Level6State_PC.PlasticSleeve);
    }

    void PlasticSleeve()
    {
        StartCoroutine(PlasticSleeve_Success());
    }

    IEnumerator PlasticSleeve_Success()
    {
        yield return new WaitForSeconds(5);
        level6Manager.UpdateLevel6State(Level6State_PC.Test);
    }
}
