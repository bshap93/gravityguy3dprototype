using System.Collections.Generic;
using UnityEngine;

namespace Player.Effects
{
    public class AttitudeJetsController : MonoBehaviour
    {
        public List<GameObject> forwardJets;
        public List<GameObject> backwardJets;
        public List<GameObject> leftJets;
        public List<GameObject> rightJets;
        public List<GameObject> strafeHorizontalJets;
        
        private List<Animator> _forwardJetAnimators;

        // Start is called before the first frame update
        void Start()
        {
            foreach (var jet in strafeHorizontalJets)
            {
                jet.SetActive(false);
            }
            
            

        }

        void ThrustForward()
        {
            foreach (var jet in forwardJets)
            {
                jet.SetActive(true);
            }
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}
