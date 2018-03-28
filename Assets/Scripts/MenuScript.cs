using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

    public GameObject MenuImage;
    public GameObject SettingsButton;
    public GameObject ExitSettingsButton;
    public GameObject ContinueButton;
    public GameObject QuitButton;
    public GameObject Slider;

    void Start()
    {
        Button btn1 = SettingsButton.GetComponent<Button>();
        btn1.onClick.AddListener(TaskOnClick);
        Button btn2 = ExitSettingsButton.GetComponent<Button>();
        btn2.onClick.AddListener(TaskOnClick1);
        Button btn3 = ContinueButton.GetComponent<Button>();
        btn3.onClick.AddListener(TaskOnClick1);
        Button btn4 = ContinueButton.GetComponent<Button>();
        btn4.onClick.AddListener(TaskOnClick2);

    }

    void TaskOnClick()
    {
        SettingsButton.SetActive(false);
        ExitSettingsButton.SetActive(true);
        MenuImage.SetActive(true);
        ContinueButton.SetActive(true);
        QuitButton.SetActive(true);
        Slider.SetActive(true);

    }
    void TaskOnClick1()
    {
        SettingsButton.SetActive(true);
        ExitSettingsButton.SetActive(false);
        MenuImage.SetActive(false);
        ContinueButton.SetActive(false);
        QuitButton.SetActive(false);
        Slider.SetActive(false);
    }
    void TaskOnClick2()
    {
        Application.Quit();
    }


}
