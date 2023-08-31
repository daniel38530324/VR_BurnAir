using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Robot : MonoBehaviour
{
    [SerializeField] Level6State level6State;
    [SerializeField] Level6Manager level6Manager;
    [SerializeField] Material[] materials;
    [SerializeField] bool isHead;
    [SerializeField] MeshRenderer robot;
    [SerializeField] GameObject robot_Old;
    bool timerTrigger = true;
    float timer;

    private void Awake()
    {
        if(level6State == Level6State.Lemonade)
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
        if (level6State == Level6State.WD40)
        {
            level6Manager.OnWD40Num += WD40;
        }
        if(level6State == Level6State.PaintGun)
        {
            level6Manager.OnPaintGunNum += PaintGun;
        }
        if(level6State == Level6State.PlasticSleeve)
        {
            level6Manager.OnPlasticSleeveNum += PlasticSleeve;
        }
    }
    private void Start()
    {
        
    }

    private void OnDestroy()
    {
        if (level6State == Level6State.WD40)
        {
            level6Manager.OnWD40Num -= WD40;
        }
        if (level6State == Level6State.PaintGun)
        {
            level6Manager.OnPaintGunNum -= PaintGun;
        }
        if (level6State == Level6State.PlasticSleeve)
        {
            level6Manager.OnPlasticSleeveNum -= PlasticSleeve;
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
            level6Manager.UpdateNum(Level6State.WD40);
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
            level6Manager.UpdateNum(Level6State.PaintGun);
        }
        else if (other.CompareTag("PlasticSleeve"))
        {
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            level6Manager.UpdateNum(Level6State.PlasticSleeve);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rag"))
        {
            if (!timerTrigger) return;

            timer += Time.deltaTime;
            if (timer >= 3)
            {
                timerTrigger = false;
                StartCoroutine(Rag());
            }
        }
    }

    IEnumerator Lemonade()
    {
        yield return new WaitForSeconds(3);
        level6Manager.UpdateLevel6State(Level6State.Rag);
    }

    IEnumerator Rag()
    {
        GetComponent<BoxCollider>().enabled = false;
        robot.enabled = true;
        robot_Old.SetActive(false);
        yield return new WaitForSeconds(5);
        level6Manager.UpdateLevel6State(Level6State.WD40);
    }

    void WD40()
    {
        StartCoroutine(WD40_Success());
    }

    IEnumerator WD40_Success()
    {
        yield return new WaitForSeconds(5);
        level6Manager.UpdateLevel6State(Level6State.PaintGun);
    }

    void PaintGun()
    {
        StartCoroutine(PaintGun_Success());
    }

    IEnumerator PaintGun_Success()
    {
        yield return new WaitForSeconds(5);
        level6Manager.UpdateLevel6State(Level6State.PlasticSleeve);
    }

    void PlasticSleeve()
    {
        StartCoroutine(PlasticSleeve_Success());
    }

    IEnumerator PlasticSleeve_Success()
    {
        yield return new WaitForSeconds(5);
        level6Manager.UpdateLevel6State(Level6State.Test);
    }
}
