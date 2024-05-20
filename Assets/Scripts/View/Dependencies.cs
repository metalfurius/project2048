using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dependencies : MonoBehaviour
{
    [SerializeField]
    private Vector2Int size = new Vector2Int(4, 4);
    private void Start()
    {
        D2048 d2048 = new D2048(size,size.x);
    }
}
