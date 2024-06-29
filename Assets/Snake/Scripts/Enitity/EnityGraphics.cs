using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class EnityGraphics : MonoBehaviour 
{
    public void Init(Color colorGraphic, string tag)
    {
        GetComponent<SpriteRenderer>().color = colorGraphic;
        gameObject.tag = tag;
    }
}