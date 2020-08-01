using UnityEngine;

namespace Animations
{
    public class Spin : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 25f;
        [SerializeField] private float axisRotationSpeed = 10f;

        private Vector3 _axis = Vector3.up;

        void FixedUpdate()
        {
            transform.Rotate(_axis, rotationSpeed * Time.fixedDeltaTime);
            _axis = Quaternion.AngleAxis(axisRotationSpeed * Time.fixedDeltaTime, Vector3.right) * _axis;
        }
    }
}