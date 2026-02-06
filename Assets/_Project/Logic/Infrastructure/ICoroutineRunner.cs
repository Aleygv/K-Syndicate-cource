using System.Collections;
using UnityEngine;

namespace _Project.Logic.Infrastructure
{
    public interface ICoroutineRunner
    {
       Coroutine StartCoroutine(IEnumerator coroutine);
    }
}