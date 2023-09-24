
namespace MilitaryElite.Core.Models
{
    using Enums;
    public interface IMission
    {
        string CodeName { get; }
        State State { get;  }

        public void CompleteMission();
    }
}
