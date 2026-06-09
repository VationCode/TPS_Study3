using UnityEngine;

public class AnimationEventRelay : MonoBehaviour
{
    private PlayerManager _playerManager;

    private void Awake()
    {
        _playerManager = GetComponentInParent<PlayerManager>();
    }

    public void OnReload()
    {
        _playerManager.Reload();
    }
    public void OnReloadInsertSound()
    {
        _playerManager.ReloadInsertClip();
    }
    public void OnReloadRemoveSound()
    {
        _playerManager.ReloadSound();
    }
}
