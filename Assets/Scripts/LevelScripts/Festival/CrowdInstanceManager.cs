using UnityEngine;
using System.Collections.Generic;
using static UnityEditor.PlayerSettings;
using Unity.AI.Navigation;

public class CrowdInstanceManager : MonoBehaviour
{
    public static CrowdInstanceManager instance;
    public List<GameObject> crowdObject = new List<GameObject>();
    public List<CrowdPosition> crowdPositions = new List<CrowdPosition>();
    private List<GameObject> movingCrowd = new List<GameObject>();
    public NavMeshSurface surface;
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
                //bool itemSet = false;
                GameObject chosenObject = null;

                //while (!itemSet)
                //{
                //    int pos = Random.Range(0, crowdObject.Count);
                //    if (!crowdPositions[pos].HasCrowd())
                //    {
                //        chosenObject = crowdObject[0];
                //        crowdPositions[pos].SetCrowd(chosenObject);
                //        itemSet = true;
                //    }
                //}
                foreach (CrowdPosition crowd in crowdPositions)
                {
                    if (!crowd.HasCrowd())
                    {
                        chosenObject = crowdObject[0];
                        crowd.SetCrowd(chosenObject);
                        break;
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
                //bool itemSet = false;
                GameObject chosenObject = null;
                bool assuredSpawn = true;
                if (!crowdPositions[assuredPos].HasCrowd())
                {
                    assuredSpawn = false;
                    chosenObject = crowdObject[0];
                    crowdPositions[assuredPos].SetCrowd(chosenObject);
                    //itemSet = true;
                }

                //while (!itemSet && assuredSpawn)
                //{
                //    int pos = Random.Range(0, crowdObject.Count);
                //    if (!crowdPositions[pos].HasCrowd())
                //    {
                //        chosenObject = crowdObject[0];
                //        crowdPositions[pos].SetCrowd(chosenObject);
                //        itemSet = true;
                //    }
                //}
                if (assuredSpawn)
                {
                    foreach (CrowdPosition crowd in crowdPositions)
                    {
                        if (!crowd.HasCrowd())
                        {
                            chosenObject = crowdObject[0];
                            crowd.SetCrowd(chosenObject);
                            break;
                        }
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

    public void AddToMovingCrowd(GameObject crowd)
    {
        movingCrowd.Add(crowd);
    }

    public void RemoveMovingCrowd(GameObject crowd)
    {
        movingCrowd.Remove(crowd);
        if(movingCrowd.Count <= 0)
        {
            surface.BuildNavMesh();
        }
    }
}
