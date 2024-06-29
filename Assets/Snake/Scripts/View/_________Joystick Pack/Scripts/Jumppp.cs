using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumppp : MonoBehaviour
{

    public static Jumppp instance;

    private void Awake()
    {
        instance = this;
    }

    public bool isJump;

    public void Jumpppppp()
    {
        isJump = true;
    }

    public void CancelJump()
    {
        isJump = false;
    }
}
