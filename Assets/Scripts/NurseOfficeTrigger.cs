using UnityEngine;

public class NurseOfficeTrigger : MonoBehaviour
{
    public Nurse nurse;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Nurse"))
        {
            nurse.inOffice = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Nurse"))
        {
            nurse.inOffice = false;
        }
    }
}