using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public enum Level3State
{
    Explain,       //�������q
    Choose,        //��ܾ������q
    MnO2,          //��J�G��ƿ����q
    Water,         //�[�J�����q
    H2O2,          //�[�J��������q
    Tube,          //�ޤl���q
    Cover,         //��J�M�l���q
    PickUp,        //���_���q
    IncenseSticks, //�u�����ն��q
    Test           //���綥�q
}
public class Level3Manager : MonoBehaviour
{
    [Header("GameManager")]
    [SerializeField] GameObject gameManager;

    [Header("Mission")]
    [SerializeField] Text mission_Text;

    [Header("Object")]
    [SerializeField] GameObject waterTank;
    [SerializeField] GameObject suctionBottle, table, waterBucket, mnO2, h2O2, dropper;
    [SerializeField] Transform spawnPoint;

    Level3State level3State;

    private void Awake()
    {
        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }

        UpdateLevel3State(Level3State.Explain);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateLevel3State(Level3State newState)
    {
        level3State = newState;

        switch (newState)
        {
            case Level3State.Explain:
                
                break;
            case Level3State.Choose:
                mission_Text.transform.parent.gameObject.SetActive(true);
                mission_Text.text = "�N���T��������b��W";
                table.SetActive(true);
                break;
            case Level3State.MnO2:
                mission_Text.text = "�[�J�G��ƿ�";
                table.SetActive(false);
                waterBucket.SetActive(false);
                h2O2.SetActive(false);
                mnO2.transform.position = spawnPoint.position;
                mnO2.transform.rotation = Quaternion.Euler(0, 0, 0);
                mnO2.SetActive(true);
                mnO2.GetComponent<WaterBucket_New>().enabled = true;
                waterTank.SetActive(true);
                suctionBottle.SetActive(true);
                break;
            case Level3State.Water:
                mission_Text.text = "�[�J��";
                mnO2.SetActive(false);
                waterBucket.transform.position = spawnPoint.position;
                waterBucket.transform.rotation = Quaternion.Euler(0, -90, 0);
                waterBucket.SetActive(true);
                waterBucket.GetComponent<WaterBucket_New>().enabled = true;
                break;
            case Level3State.H2O2:
                mission_Text.text = "�[�J�����";
                waterBucket.SetActive(false);
                h2O2.transform.position = spawnPoint.position;
                h2O2.transform.rotation = Quaternion.Euler(0, -180, 0);
                h2O2.SetActive(true);
                h2O2.GetComponent<XRGrabInteractable>().enabled = false;
                dropper.GetComponent<XRGrabInteractable>().enabled = true;
                break;
            case Level3State.Tube:
                mission_Text.text = "��J�ޤl";
                h2O2.SetActive(false);
                dropper.SetActive(false);
                break;
            case Level3State.Cover:
                mission_Text.text = "��J�M�l";
                break;
            case Level3State.PickUp:
                mission_Text.text = "���_�M�l";
                break;
            case Level3State.IncenseSticks:
                mission_Text.text = "�νu������";
                break;
            case Level3State.Test:
                mission_Text.transform.parent.gameObject.SetActive(false);
                break;
        }
    }

    public void UpdateLevel3State_Int(int newState)
    {
        UpdateLevel3State((Level3State)newState);
    }
}
