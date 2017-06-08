using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private bool _running;

    public bool Running
    {
        get { return _running; }
    }

    private void Start()
    {
        _running = false;
    }

    public void RunAnimations(Queue<IEnumerator> animations)
    {
        _running = true;
        StartCoroutine(RunAnimationsCo(animations));
    }

    private IEnumerator RunAnimationsCo(Queue<IEnumerator> animations)
    {
        foreach (var enumerator in animations)
        {
            yield return StartCoroutine(enumerator);
        }
        _running = false;
        animations.Clear();
    }
   
}