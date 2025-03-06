using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUpsAndBuffs/Bomb")]
public class Bomb : TimedPowerUp
{
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject explosionRadiusIndicator;

    private GameObject instantiatedBomb;
    private GameObject instantiatedRadiusIndicator;

    [SerializeField] private float explosionRadius = 10;
    [SerializeField] private int explosionDamange = 10;

    [SerializeField] private LayerMask whatAreDestructable;

    public override void ApplyPowerUp(PlayerController player)
    {
        instantiatedBomb = Instantiate(bombPrefab, player.transform.position, player.transform.rotation);
        Rigidbody rb = instantiatedBomb.GetComponent<Rigidbody>();
        SphereCollider collider = instantiatedBomb.GetComponent<SphereCollider>();

        if (!rb) rb = instantiatedBomb.AddComponent<Rigidbody>();
        if (!collider) instantiatedBomb.AddComponent<SphereCollider>();

        collider.isTrigger = false;
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.linearDamping = 0.1f;
        rb.angularDamping = 0.15f;

        Vector3 direction = player.transform.forward * 8 + player.transform.up * 5;
        rb.AddForce(direction, ForceMode.Impulse);

        float torqueValue = 3f;
        Vector3 randomTorque = new Vector3(
            Random.Range(-torqueValue, torqueValue),
            Random.Range(-torqueValue, torqueValue),
            Random.Range(-torqueValue, torqueValue));
        rb.AddTorque(randomTorque, ForceMode.Impulse);

        instantiatedRadiusIndicator = Instantiate(explosionRadiusIndicator, instantiatedBomb.transform.position, Quaternion.identity);
        instantiatedRadiusIndicator.transform.localScale = new Vector3(explosionRadius * 2, 0.1f, explosionRadius * 2);
        instantiatedRadiusIndicator.AddComponent<BombRadiusFollow>().Initialize(instantiatedBomb);

        Destroy(instantiatedBomb, durationInSeconds + 0.1f);
    }

    public override void DeactivatePowerup(PlayerController player)
    {
        if (instantiatedBomb != null)
        {
            GameObject instantiatedExplosion =
                Instantiate(explosion, instantiatedBomb.transform.position, Quaternion.identity);

            Destroy(instantiatedExplosion, 5f);

            if (instantiatedRadiusIndicator != null) Destroy(instantiatedRadiusIndicator);

            Collider[] objectsInRage = Physics.OverlapSphere(instantiatedExplosion.transform.position, explosionRadius);
            foreach (Collider obj in objectsInRage)
            {
                Enemy enemy = obj.GetComponent<Enemy>();
                if (enemy != null) enemy.TakeDamage(explosionDamange);
            }
        }
    }
}