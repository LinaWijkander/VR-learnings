using UnityEngine;
using UnityEngine.SceneManagement;


// Menu that exists in every scene
public class Menu : MonoBehaviour
{
    //[SerializeField] InputActionReference menuReference; 
    [SerializeField] GameObject levelSelect;
    private Camera vrCamera;

    private void Awake()
    {
        //menuReference.action.started += ShowMainMenu;
        DontDestroyOnLoad(transform.gameObject);
        vrCamera = Camera.main;
    }

    /*private void OnDestroy()
    {
        menuReference.action.started -= ShowMainMenu;
    }*/

    /*private void ShowMainMenu(InputAction.CallbackContext context)
    {
        Debug.Log("<color=yellow> HELLO I AM CLICKING</color>");
        bool isActive = !gameObject.activeSelf;
        MoveToCamera();
        gameObject.SetActive(isActive);
    }*/
    
    // Used from buttons in UI in editor
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ToggleMenu()
    {
        if (!enabled)
            MoveToCamera();
        
        bool isEnabled = !levelSelect.activeSelf;
        levelSelect.SetActive(isEnabled);
        
    }
    
    private void MoveToCamera()
    {
        Vector3 newPosition = new Vector3(vrCamera.transform.position.x,vrCamera.transform.position.x, vrCamera.transform.position.z-0.5f);
        Vector3 newRotation = new Vector3(transform.eulerAngles.x, vrCamera.transform.eulerAngles.y, transform.eulerAngles.z);

        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(newRotation);
    }
}
