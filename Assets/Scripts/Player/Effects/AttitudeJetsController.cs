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
        static readonly int IsThrusting = Animator.StringToHash("isThrusting");
        public AttitudeJetsController(List<Animator> forwardJetAnimators)
        {
            _forwardJetAnimators = forwardJetAnimators;
        }

        // Start is called before the first frame update
        void Start()
        {
            foreach (var jet in strafeHorizontalJets)
            {
                jet.SetActive(false);
            }

            var jets = GameObject.FindGameObjectsWithTag("PlayerThrustFlame");

            foreach (var jet in jets)
            {
                Animator animator = jet.GetComponent<Animator>();
                if (animator.GetBool(IsThrusting) == true)
                    animator.SetBool(IsThrusting, false);
            }
        }

        void ThrustForward()
        {
            foreach (var jet in forwardJets)
            {
                Animator animator = jet.GetComponent<Animator>();
                if (animator.GetBool(IsThrusting) == false)
                    animator.SetBool(IsThrusting, true);
            }
        }

        void EndThrusterForward()
        {
            foreach (var jet in forwardJets)
            {
                Animator animator = jet.GetComponent<Animator>();
                if (animator.GetBool(IsThrusting) == true)
                    animator.SetBool(IsThrusting, false);
            }
        }

        void ThrustBackward()
        {
            foreach (var jet in backwardJets)
            {
                Animator animator = jet.GetComponent<Animator>();
                if (animator.GetBool(IsThrusting) == false)
                    animator.SetBool(IsThrusting, true);
            }
        }

        void EndThrusterBackward()
        {
            foreach (var jet in backwardJets)
            {
                Animator animator = jet.GetComponent<Animator>();
                if (animator.GetBool(IsThrusting) == true)
                    animator.SetBool(IsThrusting, false);
            }
        }

        void ThrustLeft()
        {
            foreach (var jet in leftJets)
            {
                Animator animator = jet.GetComponent<Animator>();
                if (animator.GetBool(IsThrusting) == false)
                    animator.SetBool(IsThrusting, true);
            }
        }

        void EndThrusterLeft()
        {
            foreach (var jet in leftJets)
            {
                Animator animator = jet.GetComponent<Animator>();
                if (animator.GetBool(IsThrusting) == true)
                    animator.SetBool(IsThrusting, false);
            }
        }

        void ThrustRight()
        {
            foreach (var jet in rightJets)
            {
                Animator animator = jet.GetComponent<Animator>();
                if (animator.GetBool(IsThrusting) == false)
                    animator.SetBool(IsThrusting, true);
            }
        }

        void EndThrusterRight()
        {
            foreach (var jet in rightJets)
            {
                Animator animator = jet.GetComponent<Animator>();
                if (animator.GetBool(IsThrusting) == true)
                    animator.SetBool(IsThrusting, false);
            }
        }
    }
}
