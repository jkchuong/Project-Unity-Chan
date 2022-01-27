using System;
using System.Collections.Concurrent;
using UnityEngine;

namespace Assets.Project.Scripts
{
    public class ObjectPool<T>
    {
        public ConcurrentBag<T> Bag { get; }
        private readonly Func<T> _objectGenerator;

        // ctor
        public ObjectPool(Func<T> generateObjectFunction)
        {
            _objectGenerator = generateObjectFunction;
        }

        // get object from bag if not create one
        // scenario first item: works as it bag is empty at beggining and retriving from bag leaves bag back at 0
        public T GetFromBag()
        {
            if(Bag.TryTake(out T item))
            {
                return item;
            }

            return _objectGenerator();
        }

        public void ReturnToBag(T itemToReturn)
        {
            try
            {
                Bag.Add(itemToReturn);
            }
            catch
            {                
                Debug.LogError($"Failed to add item to bag");
            }            
        }   
    }
}
