using System;
using UnityEngine;

namespace Project.Scripts
{
    public class DonutSpin : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed = 3;
        
        private void Update()
        {
            transform.Rotate(0, rotateSpeed, 0);
        }
    }
}
