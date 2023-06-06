using UnityEngine;

namespace TopDownShooter
{
    [CreateAssetMenu(fileName = "NPSTasks", menuName = "NPS/Create Tasks List")]
    public class TasksData : ScriptableObject
    {
        public string[] tasks;
        public string[] tasksDiscription;
    }
}
