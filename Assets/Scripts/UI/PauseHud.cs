using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseHud : MonoBehaviour
{
    public static PauseHud instance;
    public InputActionReference pauseInput;
    public GameObject pauseGameObject;
    public GameObject settingsGameObject;

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        Pause(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseInput.action.WasPressedThisFrame())
        {
            Pause(!(pauseGameObject.activeSelf || settingsGameObject.activeSelf));
        }
    }


    public void Pause(bool pause)
    {
        if(pause)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            settingsGameObject.SetActive(false);
            pauseGameObject.SetActive(true);
        } else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
            settingsGameObject.SetActive(false);
            pauseGameObject.SetActive(false);
        }
    }

    public void ChangeScene(int index)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(index);
    }



}
