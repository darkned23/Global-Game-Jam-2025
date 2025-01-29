using UnityEngine;
using System.Collections;

public class BubbleExplosion : MonoBehaviour
{
    Renderer _renderer;
    [SerializeField] private float _DisolveSpeed;

    void Start()
    {
        _renderer = GetComponent<Renderer>();

        if (_renderer == null)
            Debug.LogWarning("BubbleExplosion: No se encontró el componente Renderer");
    }
    private void OnCollisionEnter(Collision other)
    {
        StartCoroutine(Corrutine_Disolve());
    }

    IEnumerator Corrutine_Disolve()
    {
        float start = _renderer.material.GetFloat("_Disolve");
        float lerp = 0;
        while (lerp < 1)
        {
            _renderer.material.SetFloat("_Disolve", Mathf.Lerp(start, 1, lerp));
            lerp += Time.deltaTime * _DisolveSpeed;
            yield return null;
        }
    }
}
