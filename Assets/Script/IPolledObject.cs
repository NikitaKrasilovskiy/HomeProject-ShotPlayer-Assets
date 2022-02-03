using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IPolledObject
{
    ObjectPooler.ObjectInfo.ObjectType Type { get;  }
}
