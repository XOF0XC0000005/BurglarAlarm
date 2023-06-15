using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AlarmTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent _triggered;

    private AudioSource _audioSource;
    private float _volumeSpeed = 0.3f;
    private float _minimumVolume = 0.01f;
    private float _minVolume = 0f;

    private bool isTriggered = false;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0f;
    }

    private void Update()
    {
        if (isTriggered)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _audioSource.maxDistance, _volumeSpeed * Time.deltaTime);

            // 2 способ
            //_audioSource.volume += _volumeSpeed * Time.deltaTime;
        }
        else
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _minVolume, _volumeSpeed * Time.deltaTime);

            // 2 способ
            //_audioSource.volume -= _volumeSpeed * Time.deltaTime;
        }

        if (_audioSource.volume < _minimumVolume && !isTriggered)
            _audioSource.Stop();
    }   

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<Burglar>(out Burglar burglar))
        {
            _triggered?.Invoke();
            isTriggered = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent<Burglar>(out Burglar burglar))
        {
            isTriggered = false;
        }
    }
}
