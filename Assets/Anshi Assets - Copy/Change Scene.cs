using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
 
public class ChangeScene : MonoBehaviour
{
public InputActionReference action;
 
private void OnEnable()
{
action.action.Enable();
action.action.performed += OnActionPerformed;
}
 
private void OnDisable()
{
action.action.performed -= OnActionPerformed;
action.action.Disable();
}
 
private void OnActionPerformed(InputAction.CallbackContext ctx)
{
SceneManager.LoadScene("kyle-mp1b");
}
}