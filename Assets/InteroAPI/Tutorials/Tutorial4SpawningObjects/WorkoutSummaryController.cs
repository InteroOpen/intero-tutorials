using Intero.Common;
using Intero.Workouts;
using UnityEngine;

public class WorkoutSummaryController : MonoBehaviour
{
    [SerializeField]
    private Transform SpawnPoint = null;

    [SerializeField]
    private GameObject item = null;

    [SerializeField]
    private RectTransform content = null;

    // public CanvasController canvas;
    public void UpdateWorkoutSummary(ErgData[] avgErgDatas, Segment[] segments)//List<WorkoutJSON> workouts, string workoutId)
    {
        //        canvas.ShowworkoutSumaryView();

        content.sizeDelta = new Vector2(0, segments.Length * 400);
        // Workout 
        for (int i = 0; i < segments.Length; i++)
        {
            Segment s = segments[i];
            ErgData e = avgErgDatas[i];
            // Segment segment = new SegmentTime()
            // 60 width of item
            float spawnY = i * 220;
            //newSpawn Position
            Vector3 pos = new Vector3(0.0f, -spawnY, SpawnPoint.position.z);
            //instantiate item
            GameObject SpawnedItem = Instantiate(item, pos, SpawnPoint.rotation);
            //setParent
            SpawnedItem.transform.SetParent(SpawnPoint, false);
            //get ItemDetails Component
            WorkoutSummaryItem segmentItem = SpawnedItem.GetComponent<WorkoutSummaryItem>();
            //set type
            segmentItem.UpdateSegmentSummary(s, e);

        }
    }
}
