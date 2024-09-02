using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player.Effects
{
    public class AttitudeJetsController : MonoBehaviour
    {
        public List<GameObject> forwardJets;
        public List<GameObject> backwardJets;
        [FormerlySerializedAs("leftJets")] public List<GameObject> leftTurnJets;
        [FormerlySerializedAs("rightJets")] public List<GameObject> rightTurnJets;
        [FormerlySerializedAs("strafeHorizontalJets")]
        public List<GameObject> strafeRightJets;
        [FormerlySerializedAs("strafeUpJets")] public List<GameObject> strafeLeftJets;


        private List<Animator> _forwardJetAnimators;
        static readonly int IsThrusting = Animator.StringToHash("isThrusting");
        public AttitudeJetsController(List<Animator> forwardJetAnimators)
        {
            _forwardJetAnimators = forwardJetAnimators;
        }

        // Start is called before the first frame update
        void Start()
        {
            foreach (var jet in strafeRightJets)
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

        public void AttitudeThrustForward()
        {
            foreach (var jet in forwardJets)
            {
                Animator animator = jet.GetComponent<Animator>();
                if (animator.GetBool(IsThrusting) == false)
                    animator.SetBool(IsThrusting, true);
            }
        }


        public void EndThrusterForward()
        {
            foreach (var jet in forwardJets)
            {
                Animator animator = jet.GetComponent<Animator>();
                if (animator.GetBool(IsThrusting) == true)
                    animator.SetBool(IsThrusting, false);
            }
        }

        public void ThrustBackward()
        {
            foreach (var jet in backwardJets)
            {
                Animator animator = jet.GetComponent<Animator>();
                if (animator.GetBool(IsThrusting) == false)
                    animator.SetBool(IsThrusting, true);
            }
        }

        public void EndThrusterBackward()
        {
            foreach (var jet in backwardJets)
            {
                Animator animator = jet.GetComponent<Animator>();
                if (animator.GetBool(IsThrusting) == true)
                    animator.SetBool(IsThrusting, false);
            }
        }

        public void ThrustLeft()
        {
            foreach (var jet in leftTurnJets)
            {
                Animator animator = jet.GetComponent<Animator>();
                if (animator.GetBool(IsThrusting) == false)
                    animator.SetBool(IsThrusting, true);
            }
        }

        public void EndThrusterLeft()
        {
            foreach (var jet in leftTurnJets)
            {
                Animator animator = jet.GetComponent<Animator>();
                if (animator.GetBool(IsThrusting) == true)
                    animator.SetBool(IsThrusting, false);
            }
        }

        public void ThrustRight()
        {
            foreach (var jet in rightTurnJets)
            {
                Animator animator = jet.GetComponent<Animator>();
                if (animator.GetBool(IsThrusting) == false)
                    animator.SetBool(IsThrusting, true);
            }
        }

        public void EndThrusterRight()
        {
            foreach (var jet in rightTurnJets)
            {
                Animator animator = jet.GetComponent<Animator>();
                if (animator.GetBool(IsThrusting) == true)
                    animator.SetBool(IsThrusting, false);
            }
        }
    }
}
