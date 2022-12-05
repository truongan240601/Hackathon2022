using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    public static TreeController instance;

    public List<GameObject> listTrees = new List<GameObject>();
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null || instance != this)
        {
            instance = this;
        }
    }


}
