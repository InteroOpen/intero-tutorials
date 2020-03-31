using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Intero.Workouts;

public class SegmentFiller : MonoBehaviour
{
    [SerializeField]
    private Transform SpawnPoint = null;

    [SerializeField]
    private GameObject item = null;

    [SerializeField]
    private RectTransform content = null;

    public WorkoutJSON workout;
    private void Awake()
    {
        for(int i=0; i<SingletonWorkouts.instancia.workouts.Count; i++)
        {
            if(SingletonWorkouts.instancia.id == SingletonWorkouts.instancia.workouts[i].id)
            {
                workout = SingletonWorkouts.instancia.workouts[i];
            }
        }

        content.sizeDelta = new Vector2(0, workout.segments.Count * 100);

        for (int i=0; i<workout.segments.Count; i++)
        {
            // 60 width of item
            float spawnY = i * 95;
            //newSpawn Position
            Vector3 pos = new Vector3(SpawnPoint.position.x - 100, -spawnY, SpawnPoint.position.z);
            //instantiate item
            GameObject SpawnedItem = Instantiate(item, pos, SpawnPoint.rotation);
            //setParent
            SpawnedItem.transform.SetParent(SpawnPoint, false);
            //get ItemDetails Component
            ClassSegmentUi segmentItem = SpawnedItem.GetComponent<ClassSegmentUi>();
            //set type
            segmentItem.segmentType.text = workout.segments[i].type.ToString();
            //set target
            segmentItem.segmentObjective.text = workout.segments[i].target.ToString();
            //set duration
            segmentItem.segmentDuration.text = workout.segments[i].duration.ToString();
            //set background color
            //no se que valores arroja intensity porque es del tipo SegmentIntensity
            // pero para cambiar el color del fondo seria
            //segmentItem.intensity.color = el color
        }

    }

}
