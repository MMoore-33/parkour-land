using UnityEngine;

public class Terminal : MonoBehaviour
{
    public void Activate()
    {
        Debug.Log("Terminal Activated!");

        // Example action (you can change this)
        GetComponent<Renderer>().material.color = Color.green;
    }
}
