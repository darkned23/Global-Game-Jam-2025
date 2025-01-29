using UnityEngine;

public class DarVida : MonoBehaviour
{
    public void Curar(int cantidadDeVida)
    {
        // Curar al jugador
        Player.instance.salud.CambiarSalud(cantidadDeVida);
        Destroy(gameObject);
    }
}
