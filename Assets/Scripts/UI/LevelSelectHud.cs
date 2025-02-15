using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelSelectHud : MonoBehaviour
{
    public InputActionReference pauseInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(true);
        LevelSelectOpen(false);
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
        SceneManager.LoadScene(index);
    }

}
