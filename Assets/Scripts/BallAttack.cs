using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAttack : MonoBehaviour {
    private void Start()
    {
        Destroy(gameObject,3);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Enemy") {
            other.gameObject.GetComponent<EnemyAction>().BehitEvent();
        }
    }
}
