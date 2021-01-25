using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Utillity
{
    public class Algorithm
    {
        public static void SortColliderDistance(Collider[] arr,int start, int finish, GameObject obj)
        {
            if (start >= finish)
                return;
            int pivot = start;
            
            int left = start;
            int right = finish;

            while(left<= right)
            {
                while(left <= finish && Vector3.Distance(arr[left].gameObject.transform.position,obj.transform.position) 
                    <= Vector3.Distance(arr[pivot].gameObject.transform.position, obj.transform.position))
                {
                    left++;
                }
                while (right > start && Vector3.Distance(arr[right].gameObject.transform.position, obj.transform.position)
                    <= Vector3.Distance(arr[pivot].gameObject.transform.position, obj.transform.position))
                {
                    right--;
                    
                }

                if(left > right)
                {
                    Collider temp = arr[pivot];
                    arr[pivot] = arr[right];
                    arr[right] = temp;
                    
                }
                else
                {
                    Collider temp = arr[left];
                    arr[left] = arr[right];
                    arr[right] = temp;
                }


                SortColliderDistance(arr, start, right - 1,obj);
                SortColliderDistance(arr, right+1, finish, obj);
            }
        }
    }
}