using System;
using UnityEditor;

namespace Game.Editor
{
    [InitializeOnLoad]
    public class ScriptRenderExecutionHelper
    {
        static ScriptRenderExecutionHelper()
        {
            foreach (MonoScript monoScript in MonoImporter.GetAllRuntimeMonoScripts())
            {
                if (monoScript.GetClass() != null)
                {
                    foreach (var a in Attribute.GetCustomAttributes(monoScript.GetClass(), typeof(ScriptOrder)))
                    {
                        var currentOrder = MonoImporter.GetExecutionOrder(monoScript);
                        var newOrder = ((ScriptOrder)a).order;
                        if (currentOrder != newOrder)
                            MonoImporter.SetExecutionOrder(monoScript, newOrder);
                    }
                }
            }
        }
    }
}