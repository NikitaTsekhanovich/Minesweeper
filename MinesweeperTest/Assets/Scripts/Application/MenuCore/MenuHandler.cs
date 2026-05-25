using UnityEngine;

namespace Application.MenuCore
{
    public class MenuHandler : MonoBehaviour
    {
        public void ChangeState(bool state)
        {
            gameObject.SetActive(state);
        }
    }
}