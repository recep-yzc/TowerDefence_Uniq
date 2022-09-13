using Cinemachine;
using DG.Tweening;
using Game.Event;
using Game.Interface;
using UnityEngine;

namespace Game.Camera
{
    public class CameraController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

        [Header("Camera Properties")] 
        [SerializeField] private CameraSystemProperty cameraSystemProperty;

        #region private
        private IGameEventSystem gameEventSystem = new GameEventSystem();

        private Tween tweenRotation;
        private Tween tweenPosition;
        private Tween tweenFov;
        private Tween tweenDamping;

        private Transform followTarget;
        private CinemachineTransposer cinemachineTransposer;

        #endregion

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
            gameEventSystem.SaveEvent(GameEvents.SendCameraFollowTransform, status, GetCameraFollowTransform);
        }

        #endregion

        private void Awake()
        {
            cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();

            SetDamping(cameraSystemProperty.Damping, 0f);
            SetOffSet(cameraSystemProperty.Offset, 0f);
            SetRotation(cameraSystemProperty.Rotation, 0f);
            SetFieldOfView(cameraSystemProperty.FieldOfView, 0f);
        }

        private void GetCameraFollowTransform(object[] args)
        {
            followTarget = (Transform)args[0];
            cinemachineVirtualCamera.Follow = followTarget;
        }

        private void SetDamping(Vector3 targetDamping, float duration = 0.4f, Ease ease = Ease.Unset)
        {
            if (duration == 0)
            {
                cinemachineTransposer.m_XDamping = targetDamping.x;
                cinemachineTransposer.m_YDamping = targetDamping.y;
                cinemachineTransposer.m_ZDamping = targetDamping.z;
            }
            else
            {
                Vector3 currentDamping = new(cinemachineTransposer.m_XDamping, cinemachineTransposer.m_YDamping,
                    cinemachineTransposer.m_ZDamping);

                tweenDamping.Kill();
                tweenDamping = DOTween.To(() => currentDamping, x => currentDamping = x, targetDamping, duration)
                    .SetEase(ease).OnUpdate(() =>
                    {
                        cinemachineTransposer.m_XDamping = currentDamping.x;
                        cinemachineTransposer.m_YDamping = currentDamping.y;
                        cinemachineTransposer.m_ZDamping = currentDamping.z;
                    });
            }
        }

        private void SetOffSet(Vector3 targetOffset, float duration, Ease ease = Ease.Unset)
        {
            Vector3 currentOffSet = cinemachineTransposer.m_FollowOffset;

            tweenPosition.Kill();
            tweenPosition = DOTween.To(() => currentOffSet, x => currentOffSet = x, targetOffset, duration)
                .SetEase(ease).OnUpdate(() => { cinemachineTransposer.m_FollowOffset = currentOffSet; });
        }

        private void SetRotation(Vector3 targetRotation, float duration, Ease ease = Ease.Unset)
        {
            tweenRotation.Kill();
            tweenRotation = cinemachineVirtualCamera.transform.DOLocalRotate(targetRotation, duration).SetEase(ease);
        }

        private void SetFieldOfView(float targetFieldOfView, float duration, Ease ease = Ease.Unset)
        {
            float currentFieldOfView = cinemachineVirtualCamera.m_Lens.FieldOfView;

            tweenFov.Kill();
            tweenFov = DOTween.To(() => currentFieldOfView, x => currentFieldOfView = x, targetFieldOfView, duration)
                .SetEase(ease).OnUpdate(() => { cinemachineVirtualCamera.m_Lens.FieldOfView = currentFieldOfView; });
        }
    }
}