using UnityEngine;

public interface IPossessable
{
    void Move(Vector2 input);
    void Jump();
    void Interact();
    void OnPossess();
    void OnUnpossess();
}
