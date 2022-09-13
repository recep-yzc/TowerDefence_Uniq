using Game.Event;
using UnityEngine;

namespace Characters.Player
{
    public class PlayerMovementActor : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private PlayerComponentActor playerComponentActor;

        [SerializeField] private CharacterController characterController;
        [SerializeField] private Transform modelFollowTransform;

        #region private

        private float MovementSpeed => playerComponentActor.Property.MovementSpeed;
        private float RotateSpeed => playerComponentActor.Property.RotateSpeed;

        private Vector3 normalised;
        private float magnitude;

        private Quaternion lookRotation;

        #endregion

        private void Start()
        {
            playerComponentActor.GameEventSystem.Publish(GameEvents.SendCameraFollowTransform, modelFollowTransform);
        }

        #region Event

        private void OnEnable()
        {
            Listen(true);
        }

        private void OnDisable()
        {
            Listen(false);
        }

        private void Listen(bool status)
        {
            playerComponentActor.GameEventSystem.SaveEvent(GameEvents.SendPlayerJoystickValue, status,
                GetPlayerHandleInput);
        }

        #endregion

        private void GetPlayerHandleInput(object[] a)
        {
            normalised = (Vector3)a[0];
            magnitude = (float)a[1];
        }

        private void Update()
        {
            HandleInputForPlayer();

            switch (playerComponentActor.MovementState)
            {
                case MovementStates.Waiting:

                    break;
                case MovementStates.Moving:

                    HandleMovement();
                    HandleRotate();

                    break;
                default:
                    break;
            }
        }

        private void HandleInputForPlayer()
        {
            if (Input.GetMouseButtonDown(0))
            {
            }

            if (Input.GetMouseButton(0))
            {
                if (magnitude >= playerComponentActor.Property.ActThreshold)
                {
                    playerComponentActor.MovementState = MovementStates.Moving;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                playerComponentActor.MovementState = MovementStates.Waiting;
            }
        }

        private void HandleMovement()
        {
            characterController.SimpleMove(normalised * MovementSpeed);
        }

        private void HandleRotate()
        {
            Vector3 lookDirection = normalised;

            if (lookDirection != Vector3.zero)
            {
                lookRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, RotateSpeed * Time.deltaTime);
            }
        }
    }
}