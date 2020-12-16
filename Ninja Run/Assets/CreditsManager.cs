using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    // UI variables
    public Text messageOverlay;

    // Start is called before the first frame update
    void Start()
    {
        messageOverlay.enabled = true;
        messageOverlay.text = "Credits";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
