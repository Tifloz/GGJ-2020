using UnityEngine.EventSystems;

public interface IPowerUpEvents : IEventSystemHandler
{
    void OnPowerUpCollected (PowerUp powerUp, BaseEntity player);

    void OnPowerUpExpired (PowerUp powerUp, BaseEntity player);
}