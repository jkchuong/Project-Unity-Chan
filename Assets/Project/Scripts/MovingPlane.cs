// using System.Collections;
// using System.Collections.Generic;
// using UnityChan;
// using UnityEngine;
// using UnityEngine.UI;
//
// public class MovingPlane : MonoBehaviour
// {
//     [SerializeField] private Transform planeOriginLocation;
//     [SerializeField] private Transform planeEndLocation;
//     public float speed;
//     private Vector3 velocity;
//     private Vector3 direction;
//     public bool isMoving = true;
//     private Button startButton;
//
//     private UnityChanControlScript unityChan;
//
//     public void ActivateThis() => gameObject.SetActive(true);
//     public void DeactivateThis() => gameObject.SetActive(false);
//
//     // Start is called before the first frame update
//     void Start()
//     {
//         direction = transform.forward * -1;
//
//         unityChan = FindObjectOfType<UnityChanControlScript>();
//         if (unityChan)
//         {
//             unityChan.OnDeath += delegate { isMoving = false; };
//         }
//         
//         startButton = GameObject.Find("Start Button").GetComponent<Button>();
//
//         if (startButton)
//         {
//             startButton.onClick.AddListener(delegate { isMoving = true; });
//         }
//     }
//     
//     // Update is called once per frame
//     private void Update()
//     {
//         if (!isMoving) return;
//         MovePlaneTowardsPlayer();
//         ResetPositionWhenReaching(planeEndLocation.position);
//     }
//     private void MovePlaneTowardsPlayer()
//     {
//         velocity = direction;
//         velocity = transform.TransformDirection(velocity);
//         velocity *= speed;
//         transform.localPosition += velocity * Time.deltaTime;
//     }
//     private void ResetPositionWhenReaching(Vector3 endLocationPosition)
//     {
//         if (gameObject.transform.position.z <= endLocationPosition.z)
//         {
//             #if UNITY_EDITOR
//                 Debug.LogWarning($"{gameObject.name}: {gameObject.transform.position} vs {endLocationPosition}");
//             #endif
//
//             DeactivateThis();
//             gameObject.transform.position = planeOriginLocation.position;
//             ActivateThis();
//         }
//     }    
// }
