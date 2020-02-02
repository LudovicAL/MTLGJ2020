using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiGradient : MonoBehaviour
{
    public class ColorChange : MonoBehaviour {
        [SerializeField]
        Gradient gradient;
        [SerializeField]
        float duration;
        float t = 0f;
        GameObject _houseIntegritySlider;
        void Start() {
            _houseIntegritySlider = GameObject.FindGameObjectWithTag("HouseIntegritySlider");
        }
        void Update() {
            float value = Mathf.Lerp(0f, 1f, t);
            t += Time.deltaTime / duration;
            Color color = gradient.Evaluate(value);
            Debug.Log(_houseIntegritySlider.GetComponent<Image>());
            _houseIntegritySlider.GetComponent<Image>().color = color;
        }
    }
}
