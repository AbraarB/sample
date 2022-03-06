using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchProjectile : MonoBehaviour
{
    [SerializeField] float dieTime;
    [SerializeField] float damage;

    void Start()
    {
        StartCoroutine(CountDownTimer());
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Die();
    }

    IEnumerator CountDownTimer()
    {
        yield return new WaitForSeconds(dieTime);

        Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
