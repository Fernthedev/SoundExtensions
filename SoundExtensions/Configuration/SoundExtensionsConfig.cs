using System.Runtime.CompilerServices;
using IPA.Config.Stores;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace SoundExtensions.Configuration
{
    internal class SoundExtensionsConfig
    {
        public static SoundExtensionsConfig Instance { get; set; }

        public virtual void OnReload()
        { }

        public virtual void Changed()
        { }

        public virtual void CopyFrom(SoundExtensionsConfig other)
        { }
    }
}