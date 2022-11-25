using UnityEngine;

namespace Katas.UniMod.HostExample
{
    public static class HostExampleApi
    {
        public static void SayHello(string name)
        {
            Debug.Log($"Hey {name}, welcome to the UniMod Host Example!");
        }
    }
}