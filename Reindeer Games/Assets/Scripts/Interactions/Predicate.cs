using System.Collections.Generic;
using UnityEngine;

public class Predicate : ScriptableObject
{
    public virtual bool IsMet(CursorData cursor) => true;
}
