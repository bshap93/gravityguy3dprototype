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
        [SerializeField] GameObject torchFlame;

        Animator _animator;


        static readonly int TorchIsOff = Animator.StringToHash("torchIsOff");
        static readonly int TorchIsMid = Animator.StringToHash("torchIsMid");
        static readonly int TorchIsFull = Animator.StringToHash("torchIsFull");

        void Start()
        {
            if (_animator == null) _animator = torchFlame.GetComponent<Animator>();
        }

        public void SetTorchState(TorchState torchState)
        {
            if (torchState == TorchState.Off)
            {
                _animator.SetBool(TorchIsOff, true);
                _animator.SetBool(TorchIsMid, false);
                _animator.SetBool(TorchIsFull, false);
            }
            else if (torchState == TorchState.Mid)
            {
                _animator.SetBool(TorchIsOff, false);
                _animator.SetBool(TorchIsMid, true);
                _animator.SetBool(TorchIsFull, false);
            }
            else if (torchState == TorchState.Full)
            {
                _animator.SetBool(TorchIsOff, false);
                _animator.SetBool(TorchIsMid, false);
                _animator.SetBool(TorchIsFull, true);
            }
        }
    }
}
