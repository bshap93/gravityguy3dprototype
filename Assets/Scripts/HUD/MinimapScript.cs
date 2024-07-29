using UnityEngine;

namespace HUD
{
    public class MinimapScript : MonoBehaviour

    {
        public Transform player;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void LateUpdate()
        {
            Vector3 newPosition = player.position;
            newPosition.y = transform.position.y;
            transform.position = newPosition;
            transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
        
        }

        private void OnPreRender()
        {
            // exclude main light from rendering
       
        }
    }
}
