#if UNITY_5 || UNITY_5_3_OR_NEWER
using Svelto.DataStructures;
using Svelto.Tasks.Internal.Unity;

namespace Svelto.Tasks.Unity
{
    public class LateMonoRunner : MonoRunner
    {
        public LateMonoRunner(string name, bool mustSurvive = false)
        {
            UnityCoroutineRunner.InitializeGameObject(name, ref _go, mustSurvive);

            var runnerBehaviour = _go.AddComponent<RunnerBehaviourLate>();
            var runnerBehaviourForUnityCoroutine = _go.AddComponent<RunnerBehaviour>();
            var info = new UnityCoroutineRunner.RunningTasksInfo() { runnerName = name };

            runnerBehaviour.StartLateCoroutine(new UnityCoroutineRunner.Process
                (_newTaskRoutines, _coroutines, _flushingOperation, info,
                 UnityCoroutineRunner.StandardTasksFlushing,
                 runnerBehaviourForUnityCoroutine, StartCoroutine));
        }

        const int NUMBER_OF_INITIAL_COROUTINE = 3;
    }
}
#endif