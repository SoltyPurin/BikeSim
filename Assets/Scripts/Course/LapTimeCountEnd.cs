using UnityEngine;

public class LapTimeCountEnd : MonoBehaviour
{
    [SerializeField]private LapTimeCountStart _lapTimeCountStart =   default;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _lapTimeCountStart.PrevLapCountPlus();

        }

    }
}
