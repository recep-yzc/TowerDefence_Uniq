using UnityEngine;

namespace Game.Helper
{
    public class ParentFindHelper : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private GameObject parent;

        public T GetParent<T>()
        {
            return parent.GetComponent<T>();
        }
    }
}