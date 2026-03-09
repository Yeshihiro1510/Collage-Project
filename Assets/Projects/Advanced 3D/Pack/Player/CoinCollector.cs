using System;
using TMPro;
using UnityEngine;

namespace Projects.Advanced_3D
{
    public class CoinCollector : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        
        private float _balance;

        private void Awake()
        {
            _text.text = _balance.ToString();
        }

        public void Add(float value)
        {
            _balance++;
            _text.text = _balance.ToString();
        }
    }
}