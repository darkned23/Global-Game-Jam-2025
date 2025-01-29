using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class MoveBubble : MonoBehaviour
{
    [Header("Movement Configuration")]
    [Space(10)]
    [SerializeField] public float upwardSpeed = 0.3f;
    [SerializeField] private float horizontalSpeed = 0.6f;
    [SerializeField] private float rotationSpeed = 4f;

    [Header("Wave Properties")]
    [Space(10)]
    [Tooltip("Controls the side-to-side wave motion")]
    [SerializeField] private float waveAmplitude = 0.6f;
    [SerializeField] private float waveFrequency = 0.5f;
    [SerializeField] private float forwardWaveAmplitude = 1.5f;
    [SerializeField] private float forwardWaveFrequency = 0.5f;
    [SerializeField] private float verticalWaveAmplitude = 1.3f;
    [SerializeField] private float verticalWaveFrequency = 0.3f;

    [Header("Scale Properties")]
    [Space(10)]
    [Range(0.1f, 2f)]
    [SerializeField] private float minScale = 0.7f;
    [Range(2f, 10f)]
    [SerializeField] private float maxScale = 5f;

    [Header("Height Configuration")]
    [Space(10)]
    [SerializeField] private float maxHeight = 30f;
    [SerializeField] private float heightVariation = 8f;

    [Header("Dispersion Settings")]
    [Space(10)]
    [SerializeField] private float disperseForce = 4f;

    private float timePassed;
    private float timeOffset;
    private float individualAmplitude;
    private float individualFrequency;
    private Vector2 moveDirection;
    private float targetScale;
    private float targetHeight;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.linearDamping = 1f;
        rb.angularDamping = 0.5f;

        timeOffset = Random.Range(0f, 3f * Mathf.PI);

        individualAmplitude = forwardWaveAmplitude * Random.Range(0.8f, 2f);
        individualFrequency = forwardWaveFrequency * Random.Range(0.8f, 1.5f);

        moveDirection = Random.insideUnitCircle.normalized * disperseForce;

        targetHeight = maxHeight + Random.Range(-heightVariation, heightVariation);

        float randomVariation = Random.Range(0.5f, 1.5f);

        targetScale = Random.Range(minScale, maxScale) * randomVariation;
        transform.localScale = Vector3.zero;

        StartCoroutine(MovementSequence());
    }

    IEnumerator MovementSequence()
    {
        float progress = 0f;

        while (progress < 1f)
        {
            timePassed += Time.deltaTime;
            progress = transform.position.y / targetHeight;

            // Escalado simplificado
            float scaleProgress = Mathf.Clamp01(progress);
            transform.localScale = Vector3.one * Mathf.Lerp(0, targetScale, Mathf.SmoothStep(0, 1, scaleProgress));

            // Calcular fuerzas
            Vector3 forces = Vector3.zero;

            // Fuerza de ascenso
            float upwardForce = upwardSpeed * (1f - progress * 0.7f);
            forces.y += upwardForce;

            // Fuerzas de dispersión y ondas
            float disperseInfluence = Mathf.SmoothStep(0, 1, progress * 1.2f);
            float xForce = Mathf.Sin((timePassed + timeOffset) * waveFrequency) * waveAmplitude;
            float zForce = Mathf.Cos((timePassed + timeOffset * 1.3f) * waveFrequency) * waveAmplitude;

            forces.x += (xForce + moveDirection.x * disperseInfluence) * horizontalSpeed;
            forces.z += (zForce + moveDirection.y * disperseInfluence) * horizontalSpeed;

            // Aplicar fuerzas
            rb.AddForce(forces, ForceMode.Force);

            // Rotación
            rb.AddTorque(Vector3.up * rotationSpeed, ForceMode.Force);

            yield return null;
        }

        while (true)
        {
            timePassed += Time.deltaTime;
            Vector3 forces = Vector3.zero;

            float xSin = Mathf.Sin((timePassed + timeOffset) * individualFrequency) * individualAmplitude;
            float ySin = Mathf.Sin((timePassed + timeOffset * 0.7f) * verticalWaveFrequency) * verticalWaveAmplitude;
            float zSin = Mathf.Sin((timePassed + timeOffset * 1.3f) * individualFrequency) * individualAmplitude;

            forces.x += (xSin + moveDirection.x) * horizontalSpeed;
            forces.y += ySin * horizontalSpeed * 0.3f;
            forces.z += (zSin + moveDirection.y) * horizontalSpeed;

            rb.AddForce(forces, ForceMode.Force);
            rb.AddTorque(Vector3.up * rotationSpeed, ForceMode.Force);

            yield return null;
        }
    }
}