using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchBehavior : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] float shootCooldown;
    [SerializeField] float shootSpeed;

    private float distToPlayer;
    private bool canShoot;
    public Transform player, shootPos;
    public GameObject witchProjectile;

    void Start()
    {
        canShoot = true;
    }

    void Update()
    {
        distToPlayer = Vector2.Distance(transform.position, player.position);
        
        if(distToPlayer <= range)
        {
            if(player.position.x > transform.position.x && transform.localScale.x < 0
                || player.position.x < transform.position.x && transform.localScale.x > 0)
            {
                WitchFlip();
            }

            if(canShoot) StartCoroutine(WitchShoot());
        }
    }

    void WitchFlip()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    IEnumerator WitchShoot()
    {
        canShoot = false;

        yield return new WaitForSeconds(shootCooldown);
        GameObject newWitchProjectile = Instantiate(witchProjectile, shootPos.position, Quaternion.identity);

        newWitchProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * Time.fixedDeltaTime, 0f);
        Debug.Log("Witch shooted projectile");

        canShoot = true;
    }
}
