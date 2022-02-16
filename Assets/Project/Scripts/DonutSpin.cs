using System;
using UnityEngine;

namespace Project.Scripts
{
    public class DonutSpin : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed = 60;
        
        private void Update()
        {
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0, Space.World);
        }
    }
}
