using UnityEngine;
using UnityEngine.UI;

namespace Mirror.Examples.NetworkRoomCanvas
{

    [RequireComponent(typeof(Button))]
    public class TogglePlayerReadyButton : MonoBehaviour
    {
        NetworkRoomPlayerExample localPlayer;
        private Button button;

        void OnEnable()
        {
            button = GetComponent<Button>();

            if (NetworkClient.active)
            {
                button.gameObject.SetActive(true);
                button.onClick.AddListener(OnClick);

                ClientScene.onLocalPlayerChanged += ClientScene_onLocalPlayerChanged;
            }
            else
            {
                // button only needs to exists on host/client
                button.gameObject.SetActive(false);
            }
        }
        void OnDisable()
        {
            ClientScene.onLocalPlayerChanged -= ClientScene_onLocalPlayerChanged;
        }

        private void ClientScene_onLocalPlayerChanged(NetworkIdentity oldPlayer, NetworkIdentity newPlayer)
        {
            if (newPlayer != null)
            {
                localPlayer = newPlayer.GetComponent<NetworkRoomPlayerExample>();
            }
            else
            {
                localPlayer = null;

            }
        }

        private void OnClick()
        {
            if (localPlayer != null)
            {
                localPlayer.CmdChangeReadyState(!localPlayer.readyToBegin);
            }
        }
    }
}
