using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Entity
{
    void ReceiveHit(float _damage);
    void StartDeath();
}
