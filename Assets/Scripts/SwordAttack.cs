using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Fence") {
            GameManager.Instance.SwordAttack();
        }
    }
}
