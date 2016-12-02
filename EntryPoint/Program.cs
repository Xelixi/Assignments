using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntryPoint
{
#if WINDOWS || LINUX
  public static class Program
  {

    [STAThread]
    static void Main()
    {

      var fullscreen = false;
      read_input:
      switch (Microsoft.VisualBasic.Interaction.InputBox("Which assignment shall run next? (1, 2, 3, 4, or q for quit)", "Choose assignment", VirtualCity.GetInitialValue()))
      {
        case "1":
          using (var game = VirtualCity.RunAssignment1(SortSpecialBuildingsByDistance, fullscreen))
            game.Run();
          break;
        case "2":
          using (var game = VirtualCity.RunAssignment2(FindSpecialBuildingsWithinDistanceFromHouse, fullscreen))
            game.Run();
          break;
        case "3":
          using (var game = VirtualCity.RunAssignment3(FindRoute, fullscreen))
            game.Run();
          break;
        case "4":
          using (var game = VirtualCity.RunAssignment4(FindRoutesToAll, fullscreen))
            game.Run();
          break;
        case "q":
          return;
      }
      goto read_input;
    }

        public static double calcDistance(Vector2 specialBuildingVector, Vector2 house)
        {
            return Math.Sqrt(Math.Pow(house.X - specialBuildingVector.X, 2) + Math.Pow(house.Y - specialBuildingVector.Y, 2));
        }

    public static void Merge(Vector2[] specialBuildingsVectorArray, int low, int middle, int high, Vector2 house)
        {
     
            Vector2[] tempArray = new Vector2[specialBuildingsVectorArray.Count()];

            int i, left_end, num_elements, temp_pos;

            left_end = (middle - 1);
            temp_pos = low;
            num_elements = (high - low + 1);

            while ((low <= left_end) && (middle <= high))
            {
                if (calcDistance(specialBuildingsVectorArray[low], house) <= calcDistance(specialBuildingsVectorArray[middle], house))
                    tempArray[temp_pos++] = specialBuildingsVectorArray[low++];
                else
                    tempArray[temp_pos++] = specialBuildingsVectorArray[middle++];
            }

            while (low <= left_end)
                tempArray[temp_pos++] = specialBuildingsVectorArray[low++];

            while (middle <= high)
                tempArray[temp_pos++] = specialBuildingsVectorArray[middle++];

            for (i = 0; i < num_elements; i++)
            {
                specialBuildingsVectorArray[high] = tempArray[high];
                high--;
            }
        }

        public static void MergeSort(Vector2[] specialBuildings, int low, int high, Vector2 house)
        {
            int middle;

            if (high > low)
            {
                middle = (high + low) / 2;
                MergeSort(specialBuildings, low, middle, house);
                MergeSort(specialBuildings, (middle + 1), high, house);
                Merge(specialBuildings, low, (middle + 1), high, house);
                
            }

        }

    private static IEnumerable<Vector2> SortSpecialBuildingsByDistance(Vector2 house, IEnumerable<Vector2> specialBuildings)
    {
      Vector2[] specialBuildingsArray = specialBuildings.ToArray();

      MergeSort(specialBuildingsArray, 0, specialBuildingsArray.Count() - 1, house);
      return specialBuildingsArray;
      
    }

    private static IEnumerable<IEnumerable<Vector2>> FindSpecialBuildingsWithinDistanceFromHouse(
      IEnumerable<Vector2> specialBuildings, 
      IEnumerable<Tuple<Vector2, float>> housesAndDistances)
    {
      return
          from h in housesAndDistances
          select
            from s in specialBuildings
            where Vector2.Distance(h.Item1, s) <= h.Item2
            select s;
    }

    private static IEnumerable<Tuple<Vector2, Vector2>> FindRoute(Vector2 startingBuilding, 
      Vector2 destinationBuilding, IEnumerable<Tuple<Vector2, Vector2>> roads)
    {
      var startingRoad = roads.Where(x => x.Item1.Equals(startingBuilding)).First();
      List<Tuple<Vector2, Vector2>> fakeBestPath = new List<Tuple<Vector2, Vector2>>() { startingRoad };
      var prevRoad = startingRoad;
      for (int i = 0; i < 30; i++)
      {
        prevRoad = (roads.Where(x => x.Item1.Equals(prevRoad.Item2)).OrderBy(x => Vector2.Distance(x.Item2, destinationBuilding)).First());
        fakeBestPath.Add(prevRoad);
      }
      return fakeBestPath;
    }

    private static IEnumerable<IEnumerable<Tuple<Vector2, Vector2>>> FindRoutesToAll(Vector2 startingBuilding, 
      IEnumerable<Vector2> destinationBuildings, IEnumerable<Tuple<Vector2, Vector2>> roads)
    {
      List<List<Tuple<Vector2, Vector2>>> result = new List<List<Tuple<Vector2, Vector2>>>();
      foreach (var d in destinationBuildings)
      {
        var startingRoad = roads.Where(x => x.Item1.Equals(startingBuilding)).First();
        List<Tuple<Vector2, Vector2>> fakeBestPath = new List<Tuple<Vector2, Vector2>>() { startingRoad };
        var prevRoad = startingRoad;
        for (int i = 0; i < 30; i++)
        {
          prevRoad = (roads.Where(x => x.Item1.Equals(prevRoad.Item2)).OrderBy(x => Vector2.Distance(x.Item2, d)).First());
          fakeBestPath.Add(prevRoad);
        }
        result.Add(fakeBestPath);
      }
      return result;
    }
  }
#endif
}