using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NcProjectileBehavior : MonoBehaviour
{
    [SerializeField]
    float speed = 3f;
    [SerializeField]
    int damage;

    float timeDestroy = 3f;

    void Start()
    {
        Destroy(gameObject, timeDestroy);
    }
}
