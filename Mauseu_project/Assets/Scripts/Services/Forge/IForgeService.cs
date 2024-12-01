using System;

namespace Services.Forge
{
    public interface IForgeService
    {
        public void Upgrade(Guid id);
        public bool CanUpgrade(Guid id);
    }
}