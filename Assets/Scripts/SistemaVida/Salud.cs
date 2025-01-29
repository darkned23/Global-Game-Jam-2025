using UnityEngine.SceneManagement;
using UnityEngine;

public class Salud : MonoBehaviour
{
    public int saludActual = 80;
    public int maxSalud = 100;
    public int minSalud = 0;
    public int indexSceneGameOver = 0;
    private void Update()
    {
        if (saludActual <= minSalud)
        {
            saludActual = minSalud;
            Debug.Log($"El {gameObject.name} ha muerto");
            if (gameObject.CompareTag("Player"))
            {
                Debug.Log("Game Over");
                SceneManager.LoadScene(indexSceneGameOver);
            }
            else if (gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Enemy Dead");
                Destroy(gameObject);
            }
        }
        else if (saludActual >= maxSalud)
        {
            saludActual = maxSalud - 1;
            Debug.Log($"El {gameObject.name} ha alcanzado la salud máxima");
        }
    }
    public void CambiarSalud(int cantidad)
    {
        saludActual += cantidad;
        if (saludActual >= maxSalud)
        {
            saludActual = maxSalud;
            Debug.Log($"Cambio de salud a: {saludActual}");
        }
    }
}
