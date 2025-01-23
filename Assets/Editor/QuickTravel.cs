using UnityEditor;
using UnityEngine;

public class QuickTravel : EditorWindow
{
    QuickTravelObject currentObj;
    Transform player;
    string posName;
    [MenuItem("Window/QuickTravel")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<QuickTravel>("Quicktravel");
    }

    private void OnValidate()
    {
        Repaint();
    }
    private void OnGUI()
    {
        currentObj = (QuickTravelObject)EditorGUILayout.ObjectField(currentObj, typeof(QuickTravelObject), false);
        if (GUILayout.Button("Reload"))
        {
            Repaint();
        }
        if (currentObj != null) 
        { 
            foreach (QuickTravelPos item in currentObj.positions)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(item.posName);
                GUILayout.Label("X:" + item.pos.x);
                GUILayout.Label("Y:" + item.pos.y);
                GUILayout.Label("Z:" + item.pos.z);
                if (GUILayout.Button("Teleport Here"))
                {
                    if (player == null)
                    {
                        player = GameObject.FindGameObjectWithTag("Player").transform;
                    }
                    if (player != null)
                    {
                        player.position = item.pos;
                    }
                }
                GUILayout.EndHorizontal();

            }
        }
        

        posName = EditorGUILayout.TextField("Position name",posName);
        if (GUILayout.Button("Add point"))
        {
            if (player == null) 
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;

            }
            if(player != null && currentObj != null)
            {
                currentObj.positions.Add(new QuickTravelPos(posName,player.position));
                Repaint();
            }

        }
    }
}
