using UnityEngine;

public class HerirEnemigo : MonoBehaviour
{
    [SerializeField] private int baseDamage = 10;

    private int CalculateDamageBasedOnScale()
    {
        float currentScale = transform.localScale.x;
        return Mathf.RoundToInt(baseDamage * currentScale);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            int damageToApply = -CalculateDamageBasedOnScale();
            Debug.Log($"Ha colisionado con un {collision.gameObject.name}. Daño: {damageToApply}");

            // Obtener el punto de contacto de la colisión
            Vector3 hitPoint = collision.contacts[0].point;

            // Activar partículas de sangre
            EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.PlayBloodEffect(hitPoint);
            }

            collision.gameObject.GetComponent<Salud>().CambiarSalud(damageToApply);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Salud>().CambiarSalud(-baseDamage);
        }
    }
}
