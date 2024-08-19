using UnityEngine;

namespace Environment.Vehicles
{
    public class HeadQuartersStationController : MonoBehaviour
    {
        public float rotationSpeed = 2;
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            // rotate the station
            transform.Rotate(Vector3.up * (Time.deltaTime * rotationSpeed));
        }
    }
}
