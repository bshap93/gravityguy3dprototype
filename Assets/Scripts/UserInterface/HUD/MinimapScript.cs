using UnityEngine;

namespace UserInterface.HUD
{
    public class MinimapScript : MonoBehaviour

    {
        public Transform player;


        // Update is called once per frame
        void LateUpdate()
        {
            Vector3 newPosition = player.position;
            newPosition.y = transform.position.y;
            transform.position = newPosition;
            transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
            // transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
        }
    }
}
