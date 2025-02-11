using Unity.AI.Navigation;
using UnityEngine;

public class NavMeshManager : MonoBehaviour
{
    public static NavMeshManager instance;
    public NavMeshSurface navMesh;

    private void Awake()
    {
        instance = this;
    }
}
