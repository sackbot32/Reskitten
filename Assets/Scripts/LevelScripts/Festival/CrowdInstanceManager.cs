using UnityEngine;
using System.Collections.Generic;
using static UnityEditor.PlayerSettings;

public class CrowdInstanceManager : MonoBehaviour
{
    public static CrowdInstanceManager instance;
    public List<GameObject> crowdObject = new List<GameObject>();
    public List<CrowdPosition> crowdPositions = new List<CrowdPosition>();
    private void Awake()
    {
        instance = this;
    }
    public void SetCrowd(int ammount)
    {
        for (int i = 0; i < ammount; i++)
        {
            if (crowdObject.Count > 0)
            {
                bool itemSet = false;
                GameObject chosenObject = null;
                while (!itemSet)
                {
                    int pos = Random.Range(0, crowdObject.Count);
                    if (!crowdPositions[pos].HasCrowd())
                    {
                        chosenObject = crowdObject[0];
                        crowdPositions[pos].SetCrowd(chosenObject);
                    }
                }

                crowdObject.Remove(chosenObject);

            } else
            {
                print("No crowd left");
            }
        }
    }

    public void SetCrowd(int assuredPos,int ammount)
    {
        for (int i = 0; i < ammount; i++)
        {
            if (crowdObject.Count > 0)
            {
                bool itemSet = false;
                GameObject chosenObject = null;
                bool assuredSpawn = true;
                if (!crowdPositions[assuredPos].HasCrowd())
                {
                    assuredSpawn = false;
                    chosenObject = crowdObject[0];
                    crowdPositions[assuredPos].SetCrowd(chosenObject);
                    itemSet = true;
                }

                while (!itemSet && assuredSpawn)
                {
                    int pos = Random.Range(0, crowdObject.Count);
                    if (!crowdPositions[pos].HasCrowd())
                    {
                        chosenObject = crowdObject[0];
                        crowdPositions[pos].SetCrowd(chosenObject);
                        itemSet = true;
                    }
                }

                crowdObject.Remove(chosenObject);

            }
            else
            {
                print("No crowd left");
            }
        }
    }
}
