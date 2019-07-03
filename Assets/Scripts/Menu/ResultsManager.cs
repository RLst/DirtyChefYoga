using UnityEngine;
using UnityEngine.SceneManagement;
namespace DirtyChefYoga
{
    public class ResultsManager : MonoBehaviour
    {

        public void PressRestart()
        {
            SceneManager.LoadScene(1);
        }

        public void PressMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void PressQuit()
        {
            Application.Quit();
        }

    }
}