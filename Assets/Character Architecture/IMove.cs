﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMove
{
    float MovementSpeed { get; }
    float TurnSpeed { get; }
}
