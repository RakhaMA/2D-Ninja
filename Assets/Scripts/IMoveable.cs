using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
    Rigidbody2D RB { get; set; }

    bool isFacingRight { get; set; }
    void Move(Vector2 velocity);
    void CheckForLeftOrRightFacing(Vector2 velocity);
}
