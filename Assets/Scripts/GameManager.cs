using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform rHand;
    public GameObject menu;
    public Text onText;
    public Text offText;

    public static int mode;

    GameObject line;
    GameObject toggleOn;
    GameObject toggleOff;
    private bool openMenu;
    private bool musicOn = true;

    // Use this for initialization
    void Start()
    {
        mode = 1;
        line = rHand.Find("Line").gameObject;
        openMenu = true;
        toggleOn = GameObject.Find("ToggleOn");
        toggleOff = GameObject.Find("ToggleOff");
        toggleOff.SetActive(false);
        offText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
        {
            openMenu = !openMenu;
        }

        if (openMenu)
        {
            menu.SetActive(true);
            line.SetActive(true);
            RaycastHit hit;
            if (Physics.Raycast(rHand.position, rHand.forward, out hit))
            {
                if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
                {
                    if (hit.collider.gameObject.name == "Play")
                    {
                        mode = 0;
                        
                    }
                    if (hit.collider.gameObject.name == "Pause")
                    {
                        mode = 1;
                    }
                    if(hit.collider.gameObject.name == "Reset")
                    {
                        SceneManager.LoadScene(0);
                    }
                    if(hit.collider.gameObject.name == "Toggle")
                    {
                        if (musicOn)
                        {
                            musicOn = false;
                            offText.enabled = true;
                            onText.enabled = false;
                            toggleOff.SetActive(true);
                            toggleOn.SetActive(false);
                        }
                        else
                        {
                            musicOn = true;
                            offText.enabled = false;
                            onText.enabled = true;
                            toggleOff.SetActive(false);
                            toggleOn.SetActive(true);
                        }
                    }
                }
                
            }
        }
        else
        {
            menu.SetActive(false);
            line.SetActive(false);
        }
    }

}