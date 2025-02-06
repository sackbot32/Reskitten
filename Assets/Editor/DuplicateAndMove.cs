using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
public class DuplicateAndMove : EditorWindow
{
    Vector3 newPosition = Vector3.zero;
    string objName = "";
    int objectAmount = 0;
    bool addTo = false;
    [MenuItem("Window/DuplicateAndMove")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<DuplicateAndMove>("Duplicate and Move");
    }
    private void OnGUI()
    {
        objName = EditorGUILayout.TextField("Parent name",objName);
        if(GUILayout.Button("Get parent name"))
        {
            objName = Selection.activeGameObject.name;
        }
        objectAmount = EditorGUILayout.IntField("Index", objectAmount);
        newPosition = EditorGUILayout.Vector3Field("New position", newPosition);
        addTo = EditorGUILayout.Toggle("Add to current pos",addTo);

        if (GUILayout.Button("Duplicate and move"))
        {
            Duplicate(Selection.gameObjects, newPosition,addTo);
            
        }
    }


    private void Duplicate(GameObject[] objs, Vector3 newPos, bool add)
    {
        Transform lastSelected = null;
        if (add)
        {
            foreach (GameObject obj in objs)
            {
                GameObject newObj = Instantiate(obj, Vector3.zero, obj.transform.rotation, obj.transform.parent);
                newObj.transform.localScale = obj.transform.localScale;
                newObj.transform.localPosition = obj.transform.localPosition + newPos;
                lastSelected = newObj.transform;
                objectAmount++;
                RenameClone(objName, newObj,objectAmount);
            }
        }
        else
        {
            foreach (GameObject obj in objs)
            {
                GameObject newObj = Instantiate(obj, Vector3.zero, obj.transform.rotation, obj.transform.parent);
                newObj.transform.localScale = obj.transform.localScale;
                newObj.transform.localPosition = newPos;
                lastSelected = newObj.transform;
                objectAmount++;
                RenameClone(objName, newObj, objectAmount);
            }
        }
        Selection.activeTransform = lastSelected;
    }

    private void RenameClone(string newName,GameObject newObj, int index = 0)
    {
        if(index == 0)
        {
            newObj.name = newName;
        } else
        {
            newObj.name = newName + "-" + index;
        }
        
    }
}
