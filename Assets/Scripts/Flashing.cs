using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashing : MonoBehaviour
{
    
    public Color color = new Color(1, 0, 1, 1);

    [Range(0.0f, 1.0f)]
    public float minBrightness = 0.0f;

    [Range(0.0f, 1)]
    public float maxBrightness = 0.5f;

    [Range(0.2f, 30.0f)]
    public float rate = 1;

    [SerializeField]
    private bool _autoStart = false;

    private float _h, _s, _v;      
    private float _deltaBrightness;   
    private Renderer _renderer;
    private Material _material;
    private readonly string _keyword = "_EMISSION";
    private readonly string _colorName = "_EmissionColor";

    private Coroutine _glinting;

    private void Start()
    {
        _renderer = gameObject.GetComponent<Renderer>();
        _material = _renderer.material;

        if (_autoStart)
        {
            StartGlinting();
        }
    }

    private void OnValidate()
    {
       
        if (minBrightness < 0 || minBrightness > 1)
        {
            minBrightness = 0.0f;   
        }
        if (maxBrightness < 0 || maxBrightness > 1)
        {
            maxBrightness = 1.0f;         
        }
        if (minBrightness >= maxBrightness)
        {
            minBrightness = 0.0f;
            maxBrightness = 1.0f;           
        }

        if (rate < 0.2f || rate > 30.0f)
        {
            rate = 1;
        }
        
        _deltaBrightness = maxBrightness - minBrightness;

        float tempV = 0;
        Color.RGBToHSV(color, out _h, out _s, out tempV);
    }

    public void StartGlinting()
    {
        _material.EnableKeyword(_keyword);

        if (_glinting != null)
        {
            StopCoroutine(_glinting);
        }
        _glinting = StartCoroutine(IEGlinting());
    }

    
    public void StopGlinting()
    {
        _material.DisableKeyword(_keyword);

        if (_glinting != null)
        {
            StopCoroutine(_glinting);
        }
    }

    private IEnumerator IEGlinting()
    {
        Color.RGBToHSV(color, out _h, out _s, out _v);
        _v = minBrightness;
        _deltaBrightness = maxBrightness - minBrightness;

        bool increase = true;
        while (true)
        {
            if (increase)
            {
                _v += _deltaBrightness * Time.deltaTime * rate;
                increase = _v <= maxBrightness;
            }
            else
            {
                _v -= _deltaBrightness * Time.deltaTime * rate;
                increase = _v <= minBrightness;
            }
            _material.SetColor(_colorName, Color.HSVToRGB(_h, _s, _v));
           
            yield return null;
        }
    }
}
