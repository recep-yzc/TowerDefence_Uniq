using Game.Interface;
using UnityEngine;

namespace Game.Helper
{
    public class TriggerExitHelper : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject script;

        #region private

        private ITriggerExit triggerExit;

        #endregion

        private void Awake()
        {
            triggerExit = script.GetComponent<ITriggerExit>();
        }

        private void OnTriggerExit(Collider other)
        {
            triggerExit?.TriggerExit(other);
        }
    }
}