using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    //T jenerik entitity daha sonra veri tabanına yavsıyacak taraf 
    public interface IRepository<T> where T : class,new()
    {
        List<T> List();
       
        void Insert(T p);

        void Update(T p);
        void Delete(T p);



        T GetById(int id);
        
    }
}
