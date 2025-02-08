using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void Play()
    {
        //AudioManager.Instance?.PlayPaper();
        //Go to next scene (current index + 1)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //I take the current index of the scene and add 1.

    }

  /*  //public void Reiniciar()
    //{
    //    //AudioManager.Instance?.PlayPaper();
    //    // Cargar la escena actual para reiniciarla
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //}*/

    public void ChangeScene(string sceneName)
    {
        //AudioManager.Instance?.PlayPaper();
        SceneManager.LoadScene(sceneName);
    }



    public void Exit()
    {
       // AudioManager.Instance?.PlayPaper();
#if UNITY_EDITOR
        // Si estamos en el editor, det�n el juego
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // Si es una compilaci�n, cierra la aplicaci�n
            Application.Quit();
#endif
    }

}
