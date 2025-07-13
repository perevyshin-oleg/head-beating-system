using UnityEngine;

namespace PunchFeature.Core
{
    public class Puncher : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _punchForce;

        private void Awake()
        {
            _camera ??= Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.gameObject.TryGetComponent(out PunchReceiver punchReceiver))
                    {
                        Punch(punchReceiver, hit.point, ray.direction.normalized);
                    }
                }
            }
        }

        private void Punch(PunchReceiver punchReceiver, Vector3 pointPosition, Vector3 direction)
        {
            punchReceiver?.GetPunch(pointPosition, direction * _punchForce);
        }
    }
}