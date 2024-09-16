using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FireButton : MonoBehaviour
{
    [SerializeField] private KeyCode _key = KeyCode.F;
    [SerializeField] private float _cooldown = 3f;
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _buttonName;

    private bool _canFire;
    private int _roundAccuracy = 1;

    public event Action Fire;

    private void Start()
    {
        _buttonName.text = _key.ToString();
        _canFire = true;
    }

    private void Update()
    {
        if (Input.GetKey(_key) && _canFire)
        {
            Fire?.Invoke();

            StartCoroutine(DisableButton());
        }
    }

    private IEnumerator DisableButton()
    {
        float time = _cooldown;
        _button.interactable = false;
        _canFire = false;

        while (time > 0)
        {
            _buttonName.text = MathF.Round(time, _roundAccuracy).ToString();
            time -= Time.deltaTime;
            yield return null;
        }

        _buttonName.text = _key.ToString();
        _button.interactable = true;
        _canFire = true;
    }
}
