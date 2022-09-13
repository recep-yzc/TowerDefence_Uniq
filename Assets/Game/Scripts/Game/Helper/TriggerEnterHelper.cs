using Game.Interface;
using UnityEngine;

namespace Game.Helper
{
    public class TriggerEnterHelper : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private GameObject script;

        #region private

        private ITriggerEnter triggerEnter;

        #endregion

        private void Awake()
        {
            triggerEnter = script.GetComponent<ITriggerEnter>();
        }

        private void OnTriggerEnter(Collider other)
        {
            triggerEnter?.TriggerEnter(other);
        }
    }
}