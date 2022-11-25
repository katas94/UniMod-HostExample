using UnityEngine;

namespace Katas.UniMod.HostExample
{
    /// <summary>
    /// Simple component that will make your GameObject to rotate at a constant speed.
    /// </summary>
    public class Rotator : MonoBehaviour
    {
        [Tooltip("The rotation speed in degrees per second")]
        public float rotationSpeed = 180.0f;

        private void Update()
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}
