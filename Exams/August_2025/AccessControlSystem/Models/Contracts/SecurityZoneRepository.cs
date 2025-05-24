using AccessControlSystem.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlSystem.Models.Contracts
{
    internal class SecurityZoneRepository:IRepository<ISecurityZone>
    {
        private readonly List<ISecurityZone> _securityZoneList;
        public SecurityZoneRepository()
        {
            _securityZoneList = new List<ISecurityZone>();
        }
        public IReadOnlyCollection<ISecurityZone> Models
        {
            get
            {
                return _securityZoneList.AsReadOnly();
            }
        }
        public void AddNew(ISecurityZone model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "Security zone model cannot be null.");
            }
            _securityZoneList.Add(model);
        }
        public ISecurityZone GetByName(string modelName)
        {
            if (string.IsNullOrWhiteSpace(modelName))
            {
                return null; 
            }
            return _securityZoneList.FirstOrDefault(z => z.Name.Equals(modelName, StringComparison.OrdinalIgnoreCase));
        }
        public int SecurityCheck(string modelName)
        {
            return GetByName(modelName).AccessLevelRequired;
        }
    }   
    
    }

