using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
    public ParticleSystem bloodParticles;
    public Transform[] puntosPatruya;
    private int posActual = 0;
    public int damage = 10;
    public Transform player;
    public float tiempoEspera = 2f;
    public float detectionRange = 10f; // Rango para detectar al jugador
    public float limitRange = 15f; // Rango para dejar de perseguir al jugador

    public UnityEvent atacar;

    private NavMeshAgent agent;
    private bool perseguir = false;
    float tiempo;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (puntosPatruya.Length > 0)
        {
            agent.SetDestination(puntosPatruya[posActual].position);
        }
        player = Player.instance.transform;
    }

    void Update()
    {
        float distancia = Vector3.Distance(transform.position, player.position);

        if (distancia <= detectionRange)
        {
            // Si el jugador est� dentro del rango de detecci�n, perseguirlo
            perseguir = true;
        }
        else if (distancia > limitRange)
        {
            // dejar de perseguir
            perseguir = false;
        }

        if (perseguir) ChasePlayer();
        else Patruyar();
    }

    void Patruyar()
    {
        // Si no hay puntos de patrulla quedarse en la posici�n actual
        if (puntosPatruya.Length == 0)
        {
            agent.isStopped = true;
            return;
        }
        // Mover al siguiente punto
        agent.isStopped = false;
        if ((!agent.pathPending && agent.remainingDistance < 0.5f) || Time.time > tiempo)
        {
            posActual = (posActual + 1) % puntosPatruya.Length;
            agent.SetDestination(puntosPatruya[posActual].position);
            if (Time.time > tiempo)
            {
                tiempo = Time.time + 30;
            }
        }
    }

    void ChasePlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position); // Perseguir al jugador
        if ((transform.position - player.position).magnitude < 1.5f)
        {
            atacar.Invoke();
            Player.instance.salud.CambiarSalud(-damage);

            Destroy(gameObject);
        }


    }

    public void PlayBloodEffect(Vector3 hitPoint)
    {
        if (bloodParticles != null)
        {
            // Instanciamos directamente el sistema de partículas
            ParticleSystem newBloodEffect = Instantiate(bloodParticles);

            // Posicionamos en el punto de impacto
            newBloodEffect.transform.position = hitPoint;

            // Aseguramos que el sistema se reproduzca
            newBloodEffect.Play();

            // Destruimos después de la duración
            float totalDuration = newBloodEffect.main.duration + newBloodEffect.main.startLifetime.constant;
            Destroy(newBloodEffect.gameObject, totalDuration);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Dibujar el rango de detecci�n y el rango para dejar de perseguir
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, limitRange);
    }
}
