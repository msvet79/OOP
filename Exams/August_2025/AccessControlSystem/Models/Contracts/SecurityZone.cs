using AccessControlSystem.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AccessControlSystem.Models.Contracts
{
    public class SecurityZone : ISecurityZone
    {
        private string? name;
        private int accessLevelRequired;
        private readonly List<int> accessLog;

        public SecurityZone(string name, int accessLevelRequired)
        {
            Name = name;
            AccessLevelRequired = accessLevelRequired;
            accessLog = new List<int>();
        }

        public string Name
        {
            get => name!;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidSecurityZoneName);
                }
                name = value;
            }
        }

        public int AccessLevelRequired
        {
            get => accessLevelRequired;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidAccessLevel);
                }
                accessLevelRequired = value;
            }
        }

        public IReadOnlyCollection<int> AccessLog => accessLog;

        public void LogAccessKey(int securityId)
        {
            if (securityId < 100 || securityId > 999)
            {
                throw new ArgumentOutOfRangeException(nameof(securityId), "Security ID must be between 100 and 999.");
            }
            if (!accessLog.Contains(securityId))
            {
                accessLog.Add(securityId);
            }
        }
    }
}

