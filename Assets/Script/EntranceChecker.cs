using UnityEngine;

[RequireComponent(typeof(AlarmTrigger))]
public class EntranceChecker : MonoBehaviour
{
    private AlarmTrigger _alarm;

    private void Awake()
    {
        _alarm = GetComponent<AlarmTrigger>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<Burglar>(out Burglar burglar))
        {
            _alarm.IncreaseVolumeToMax();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent<Burglar>(out Burglar burglar))
        {
            _alarm.DeacreaseVolumeToMin();
        }
    }
}
