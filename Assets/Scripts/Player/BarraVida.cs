using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public Salud salud;
    public Scrollbar barraVida;

    public void Start()
    {
        barraVida.size = (float)salud.saludActual / salud.maxSalud;
    }

    public void Update()
    {
        barraVida.size = (float)salud.saludActual / salud.maxSalud;
    }
}
