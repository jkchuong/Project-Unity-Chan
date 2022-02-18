using System.Collections;
using System.Collections.Generic;
using UnityChan;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public bool isMoving = true;
    
    public float scrollSpeed = 0.5f;
    private Material myMaterial;
    private Vector2 offSet;

    private UnityChanControlScript unityChan;

    // Start is called before the first frame update
    private void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        offSet = new Vector2(0f, scrollSpeed);
        unityChan = FindObjectOfType<UnityChanControlScript>();

        if (unityChan)
        {
            unityChan.OnDeath += delegate { isMoving = false; };
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (!isMoving) return;
        myMaterial.mainTextureOffset += offSet * Time.deltaTime;
    }
}
