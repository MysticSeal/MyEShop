using MyEShop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyEShop.DataAccess.InMemory
{
    public class InMemoryRepository<T> where T : BaseEntity 
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        string className;

        public InMemoryRepository()
        {
            className = typeof(T).Name;
            items = cache[className] as List<T>;
            if(items == null)
            {
                items = new List<T>();
            }
        }

        public void Commit()
        {
            cache[className] = items;
        }

        public void Insert(T t)
        {
            items.Add(t);
        }

        public void Update(T t)
        {
            T tUpdate = items.Find(p => p.id == t.id);

            if (tUpdate == null)
            {
                tUpdate = t;
            }
            else
            {
                throw new Exception(className + "Was Not Found");
            }

        }

        public T Find(string id)
        {
            T tFind = items.Find(i => i.id == id);

            if (tFind == null)
            {
                throw new Exception(className + "Was Not Found");
            }
            else
            {
                return tFind;
            }
        }

        public IQueryable<T> Collection()
        {
            return items.AsQueryable(); 
        }

        public void Delete(string id)
        {
            T tDelete = items.Find(i => i.id == id); 

            if(tDelete == null)
            {
                throw new Exception(className + "Was Not Found");
            }
            else
            {
                items.Remove(tDelete);
                
            }
        }
    }
}
