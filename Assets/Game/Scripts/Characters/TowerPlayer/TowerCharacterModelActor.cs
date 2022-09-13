using Game.Scriptable;
using UnityEngine;

public class TowerCharacterModelActor : MonoBehaviour
{
    [Header("Data")]
    public TowerCharacterPropertyData TowerCharacterPropertyData;

    [Header("References")]
    [SerializeField] private Transform muzzleTransform;

    public Transform GetMuzzleTransform()
    {
        return muzzleTransform;
    }
}
