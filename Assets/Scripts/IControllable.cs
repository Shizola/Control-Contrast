using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
//interface to toggle control of objects
public interface IControllable
{
    void TakeControl();
    void ReleaseControl();
    void TestTakeControl();
    void TestReleaseControl();
}


