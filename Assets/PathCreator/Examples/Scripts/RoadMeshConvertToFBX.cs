using PathCreation;
using PathCreation.Examples;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMeshConvertToFBX : MonoBehaviour
{
    [SerializeField] private PathCreator _PathCreator;
    [SerializeField] private MeshRenderer mainRoadMeshrenderer;

    [Button("CreateMesh")]
    public void CreatePathMesh()
    {
        _PathCreator.GetComponent<RoadMeshCreator>().CreateMesh(mainRoadMeshrenderer);
    }
}
