using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealShuffle
{
  public static class Globals
  {
    public static SpotifyClientConfig DefaultConfig = SpotifyClientConfig.CreateDefault();

    private static Random rng = new Random();

    public static void Shuffle<T>(this IList<T> list)
    {
      int n = list.Count;
            Console.WriteLine(n);
      while (n > 1)
      {
        n--;
        int k = rng.Next(n + 1);
        T value = list[k];
        list[k] = list[n];
        list[n] = value;
      }
    }
  }
}
