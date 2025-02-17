using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[Serializable]
public class LevelButtonObj
{
    public int levelPos;
    public Button levelButton;
    public Image catSiluete;
    public Image catImage;

    public void CheckForLevel()
    {
        if (PlayerPrefs.GetInt(LevelSelectHud.LEVELKEY) + 1 < levelPos)
        {
            levelButton.interactable = false;
            catImage.color = new Color(1,1,1,0);
            catSiluete.color = new Color(1,1,1,0.5f);

        }
    }
}

public class LevelSelectHud : MonoBehaviour
{
    public static string LEVELKEY = "LastBeaten";
    public InputActionReference pauseInput;
    public List<LevelButtonObj> levelButtons = new List<LevelButtonObj>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        foreach (LevelButtonObj lvlButton in levelButtons)
        {
            lvlButton.CheckForLevel();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseInput.action.WasPressedThisFrame() && gameObject.activeSelf)
        {
            LevelSelectOpen(false);
        }
    }

    public void LevelSelectOpen(bool open)
    {
        if (open)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            gameObject.SetActive(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
            gameObject.SetActive(false);
        }
    }

    public void ChangeScene(int index)
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadingManager.instance.SceneChangeWithLoadScreen(index));
    }

}
