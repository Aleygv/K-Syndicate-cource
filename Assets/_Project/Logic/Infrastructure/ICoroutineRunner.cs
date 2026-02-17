using System.Collections;
using _Project.Logic.Infrastructure.States;
using UnityEngine;

namespace _Project.Logic.Infrastructure
{
    public interface ICoroutineRunner
    {
       Coroutine StartCoroutine(IEnumerator coroutine);
    }
}