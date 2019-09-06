using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHaveStats
{
    float Health { get; }
    float Attack { get; }
    float Defense { get; }
}
