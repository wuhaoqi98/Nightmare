using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform rHand;
    public GameObject menu;
    public Text gameMessage;
    
    public Text onText;
    public Text offText;

    public static int mode;
    public static bool gameOver;

    GameObject line;
    GameObject toggleOn;
    GameObject toggleOff;
    private bool openMenu;
    private bool musicOn = true;
    private float timer = 0;
    private float messageTime = 1;

    // Use this for initialization
    void Start()
    {
        mode = 0;
        line = rHand.Find("Line").gameObject;
        openMenu = true;
        toggleOn = GameObject.Find("ToggleOn");
        toggleOff = GameObject.Find("ToggleOff");
        toggleOff.SetActive(false);
        offText.enabled = false;
        gameMessage.enabled = false;
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
                        setMessage("Clear The Area", 1);
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

        if (gameOver)
        {
            mode = 1;
            setMessage("You Died, Mission Failed!", 100);
        }

        timer += Time.deltaTime;
        if(timer >= messageTime)
        {
            gameMessage.enabled = false;
        }
    }

    private void setMessage(string message, float time)
    {
        gameMessage.text = message;
        gameMessage.enabled = true;
        timer = 0;
        messageTime = time;
    }

}