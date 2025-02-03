using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class PrefabPallete : EditorWindow
{

    public PalleteObject currentPallete;
    private List<GameObject> prefabs;
    public Vector3 positionToSpawn;
    public bool relativeToSelection;
    public bool childToSelection;
    private GameObject lastObj;
    [MenuItem("Window/Prefab Pallete")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<PrefabPallete>("PrefabPallete");
    }
    private void OnValidate()
    {
        Repaint();
    }

    private void OnGUI()
    {
        currentPallete = (PalleteObject)EditorGUILayout.ObjectField(currentPallete, typeof(PalleteObject), false);
        positionToSpawn = EditorGUILayout.Vector3Field("Spawn point",positionToSpawn);
        EditorGUILayout.BeginHorizontal();
        relativeToSelection = GUILayout.Toggle(relativeToSelection,"Relative to selection");
        childToSelection = GUILayout.Toggle(childToSelection,"Child to selection");
        EditorGUILayout.EndHorizontal();
        if (currentPallete != null) 
        { 
            foreach (GameObject prefab in currentPallete.prefabs)
            {
                if(prefab != null)
                {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label(prefab.name);
                        prefabs.Add((GameObject)EditorGUILayout.ObjectField(prefab, typeof(GameObject), false));
                    if (GUILayout.Button("Spawn item in pos"))
                    {
                        GameObject spawnedObj = null;
                        if(relativeToSelection)
                        {
                            spawnedObj = Instantiate(prefab, Selection.activeGameObject.transform.position + positionToSpawn, Quaternion.identity);
                        } else
                        {
                            spawnedObj = Instantiate(prefab, positionToSpawn, Quaternion.identity);
                        }
                        if(spawnedObj != null && childToSelection)
                        {
                            if(Selection.activeGameObject != null)
                            {
                                spawnedObj.transform.parent = Selection.activeGameObject.transform;
                            }
                        }
                        lastObj = spawnedObj;
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
        }
        if (GUILayout.Button("Undo last obj"))
        {
            DestroyImmediate(lastObj);
        }
    }


}
