using System;
using System.Collections.Generic;
using Action = Unity.Plastic.Antlr3.Runtime.Misc.Action;

namespace Infrastructure.Data
{
    [Serializable]
    public class PurchaseData
    {
        public List<BoughtIAP> BoughtIaps = new List<BoughtIAP>();

        public event Action OnChange;

        public void AddPurchase(string id)
        {
            BoughtIAP boughtIAP = GetProduct(id);

            if (boughtIAP != null)
                boughtIAP.Count++;
            else
                BoughtIaps.Add( new BoughtIAP() { IAPId = id, Count = 1 });
            
            OnChange?.Invoke();
        }

        private BoughtIAP GetProduct(string id) =>
            BoughtIaps.Find(x => x.IAPId == id);
    }
}