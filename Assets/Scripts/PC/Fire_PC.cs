using UnityEngine;

public class Fire_PC : MonoBehaviour
{
    public Level2Manager_PC level2Manager;
    private Vector3 myScale;
    private float durationTime = 5, timer = 0;
    private bool turnOff;
    public string fireName;
    public bool beenused;

    private void Start()
    {
        myScale = transform.localScale;
    }

    private void Update()
    {
        if (turnOff)
        {
            if (timer < durationTime)
            {
                timer += Time.deltaTime;
                transform.localScale = Vector3.Lerp(myScale, Vector3.zero, timer / durationTime);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bubble") && !beenused){
            if(fireName == "Electric"){
                beenused = true;
                level2Manager.fireCount++;
                level2Manager.UpdateFireCount();
                level2Manager.GetKnowledgePoints(level2Manager.FireEx_DryPowder_UI, true);
                durationTime = 5;
                turnOff = true;
                level2Manager.CheckFinish(2);
            }else{
                level2Manager.GetWrong();
            }
        }else if(other.CompareTag("Metal") && !beenused){
            if(fireName == "Chemical"){
                beenused = true;
                level2Manager.fireCount++;
                level2Manager.UpdateFireCount();
                level2Manager.GetKnowledgePoints(level2Manager.FireEx_Metal_UI, true);
                durationTime = 5;
                turnOff = true;
                level2Manager.CheckFinish(2);
            }else{
                level2Manager.GetWrong();
            }
        }else if(other.CompareTag("Water") && !beenused)
        {
            if(fireName == "Podium"){
                beenused = true;
                level2Manager.fireCount++;
                level2Manager.UpdateFireCount();
                level2Manager.GetKnowledgePoints(level2Manager.WaterBucket_UI, true);
                durationTime = 1;
                turnOff = true;
                level2Manager.CheckFinish(0);
            }else{
                level2Manager.GetWrong();
            }
        }else if(other.CompareTag("Cover") && !beenused)
        {
            level2Manager.GetWrong();
        }
    }
    
}
