using UnityEngine;

public class LookAtCameraActor : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform holder;

    #region private

    private Transform cameraTransform;

    #endregion

    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        GetHolder().LookAt(cameraTransform, Vector3.up);
    }

    private Transform GetHolder()
    {
        return holder;
    }
}