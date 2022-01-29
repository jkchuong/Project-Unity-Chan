using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using Assets.Project.Scripts.Obstacles;

namespace Assets.Project.Scripts.Obstacles
{
    public class ObjectPool: MonoBehaviour
    {
        public Dictionary<string, GameObject> Bag { get; }

        // ctor
        public ObjectPool()
        {
            var allObjectsToPool = GameObject.FindGameObjectsWithTag("obstacle");
            PopulateBag(allObjectsToPool);
        }

        private void PopulateBag(GameObject[] objectsToPool)
        {
            foreach (GameObject obj in objectsToPool)
            {
                Bag.Add(obj.name, obj);
            }
        }

        // get object from bag if not create one
        // scenario first item: works as it bag is empty at beggining and retriving from bag leaves bag back at 0
        public GameObject GetFromBag(string specificObstacle)
        {
            return Bag[specificObstacle];
        }

        //public void ReturnToBag(stringGameObject itemToReturn)
        //{
        //    try
        //    {
        //        Bag.Add(itemToReturn);
        //    }
        //    catch
        //    {                
        //        Debug.LogError($"Failed to add item to bag");
        //    }            
        //}


        //private void Attack()
        //{
        //    // reset timer every time you attack
        //    cooldownTimer = 0;
        //    _animator.SetTrigger(paramAttack);

        //    // pooling instead of instantiate/ destroy (because bad for performance) (instead activate and deactive to reuse)
        //    // 1) reset position of first projectile to origin
        //    allFireballs[FindFireball()].transform.position = firePointOriginLocation.position;
        //    // 2) get actual element and send it to correct direction
        //    float currentDirectionSign = Mathf.Sign(transform.localScale.x);
        //    allFireballs[FindFireball()].GetComponent<FireBall>().SetDirectionFireball(currentDirectionSign);

        //    // sound
        //    _audioSource.Play();
        //}

    }
}
