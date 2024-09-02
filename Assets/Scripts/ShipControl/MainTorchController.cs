using System;
using System.Collections.Generic;
using ES3Internal;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;


namespace ShipControl
{
    [Serializable]
    public enum TorchState
    {
        Off,
        Mid,
        Full
    }

    public class MainTorchController : MonoBehaviour
    {
        public List<GameObject> torchFlames;

        private List<Animator> _torchAnimators;


        public bool singleTorch;
        static readonly int TorchIsOff = Animator.StringToHash("torchIsOff");
        static readonly int TorchIsMid = Animator.StringToHash("torchIsMid");
        static readonly int TorchIsFull = Animator.StringToHash("torchIsFull");

        void Start()
        {
            if (singleTorch)
            {
                GameObject torchFlame = torchFlames[0];
                torchFlame.gameObject.SetActive(true);
                _torchAnimators[0] = torchFlame.GetComponent<Animator>();
                SetTorchState(TorchState.Off);
            }
            else
            {
                // Do this when there are multiple torches
            }
        }

        public void SetTorchState(TorchState torchState)
        {
            switch (torchState)
            {
                case TorchState.Off:
                    foreach (GameObject torchFlame in torchFlames)
                    {
                        if (singleTorch)
                        {
                            _torchAnimators[0].SetBool(TorchIsOff, true);
                            _torchAnimators[0].SetBool(TorchIsMid, false);
                            _torchAnimators[0].SetBool(TorchIsFull, false);
                        }
                    }

                    break;
                case TorchState.Mid:
                    foreach (GameObject torchFlame in torchFlames)
                    {
                        if (singleTorch)
                        {
                            _torchAnimators[0].SetBool(TorchIsOff, false);
                            _torchAnimators[0].SetBool(TorchIsMid, true);
                            _torchAnimators[0].SetBool(TorchIsFull, false);
                        }
                    }

                    break;
                case TorchState.Full:
                    foreach (GameObject torchFlame in torchFlames)
                    {
                        if (singleTorch)
                        {
                            _torchAnimators[0].SetBool(TorchIsOff, false);
                            _torchAnimators[0].SetBool(TorchIsMid, false);
                            _torchAnimators[0].SetBool(TorchIsFull, true);
                        }
                    }

                    break;
            }
        }
    }
}
