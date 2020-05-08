using UnityEngine;
using Intero.Workouts;
using InteroAPI.OAuth;

public class SegmentFiller : MonoBehaviour
{
    [SerializeField]
    private Transform SpawnPoint = null;

    [SerializeField]
    private GameObject item = null;

    [SerializeField]
    private RectTransform content = null;

    public CanvasController canvas;

    public WorkoutManager workoutManager;

    public ClientUI client = null;
    public void ShowSelectedWorkout(WorkoutJSON workout)//List<WorkoutJSON> workouts, string workoutId)
    {
        canvas.ShowworkoutSumaryView();
        if(client!=null)
            client.StartClient(canvas.GetUsername());
        if (workout == null)
        {
            print("SegmentFiller.ShowSelectedWorkout Nu!!!");
           // return;
        }
        workoutManager.LoadWorkout(workout);
        /*
         
        WorkoutJSON workout = null;
        for (int i=0; i< workouts.Count; i++)
        {
            if(workoutId == workouts[i].id)
            {
                workout = SingletonWorkouts.instancia.workouts[i];
            }
        }
        */
        content.sizeDelta = new Vector2(0, workout.segments.Length * 100);
        string[] strDifficulty = { "recuperación", "medio", "Fuerte!" };
        // Workout 
        for (int i=0; i<workout.segments.Length; i++)
        {
            Segment s = Segment.Factory(workout.segments[i]);
            // Segment segment = new SegmentTime()
            // 60 width of item
            float spawnY = i * 95;
            //newSpawn Position
            Vector3 pos = new Vector3(0.0f, -spawnY, SpawnPoint.position.z);
            //instantiate item
            GameObject SpawnedItem = Instantiate(item, pos, SpawnPoint.rotation);
            //setParent
            SpawnedItem.transform.SetParent(SpawnPoint, false);
            //get ItemDetails Component
            ClassSegmentUi segmentItem = SpawnedItem.GetComponent<ClassSegmentUi>();
            //set type
            segmentItem.segmentType.text = strDifficulty[(int)s.typeIntensity];// .type.ToString();
            //set target
            segmentItem.segmentObjective.text = s.getTextObjective(); // workout.segments[i].target.spm.ToString();
            //set duration
            segmentItem.segmentDuration.text = s.getTextRemaining(0);// workout.segments[i].duration.ToString();
            //set background color
            //no se que valores arroja intensity porque es del tipo SegmentIntensity
            // pero para cambiar el color del fondo seria
            //segmentItem.intensity.color = el color
        }

    }

}
