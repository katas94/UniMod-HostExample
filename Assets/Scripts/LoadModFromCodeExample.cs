using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Katas.UniMod.HostExample
{
    /// <summary>
    /// Example of how to initialize the UniMod context from code and load only the example mod.
    /// </summary>
    public class LoadModFromCodeExample : MonoBehaviour
    {
        private const string HostId = "com.katas.unimod.host-example";
        private const string HostVersion = "1.0.0";
        private const string ModExampleId = "com.katas.unimod.mod-example";
        
        private void Awake()
        {
            Initialize().Forget();
        }

        private static async UniTaskVoid Initialize()
        {
            // if the context were already initialized this would throw an error. You can use UniModRuntime.IsContextInitialized for checking
            UniModRuntime.InitializeContext(HostId, HostVersion);
            

            // refresh the context it so it scans all installed mods
            UniModRuntime.Context.AddSource(embeddedModSource);
            await UniModRuntime.Context.RefreshAsync();
            
            // try to load the example mod
            bool successful = await UniModRuntime.Context.TryLoadModAsync(ModExampleId);
            
            // IMPORTANT: you should never use the IMod.LoadAsync() method directly unless you know what you are doing.
            // Always use the TryLoadModAsync() method from the context as we just did.

            if (successful)
                Debug.Log("Mod example loaded successfully!");
            else
                CheckForExampleModIssues();
        }

        private static void CheckForExampleModIssues()
        {
            // try to get the mod instance from the context. The mod instance contains useful information about the mod
            IMod exampleMod = UniModRuntime.Context.GetMod(ModExampleId);
            
            // if the instance is null it means that the mod may not be installed or something went wrong when installing/fetching it
            if (exampleMod is null)
            {
                Debug.LogError($"Could not find the example mod, is it properly installed under \"{UniModRuntime.Context.InstallationFolder}\"?");
                return;
            }
            
            // the mod instance exists, so check if it has any issues
            if (exampleMod.Issues == 0)
            {
                Debug.Log("The example mod doesn't have any issues");
                return;
            }
            
            // check all possible issues
            var issues = Enum.GetValues(typeof(ModIssues)) as ModIssues[];
            foreach (ModIssues issue in issues)
            {
                if (exampleMod.Issues.HasFlag(issue))
                    Debug.Log($"Issue found on the mod example: {issue}");
            }
        }
    }
}