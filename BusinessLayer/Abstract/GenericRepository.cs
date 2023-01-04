using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public class GenericRepository<T> : IRepository<T> where T : class, new()
    {
        denemeEntities db =new denemeEntities();
        
        DbSet<T> data;

        public GenericRepository()
        {
            data = db.Set<T>();
        }

        public void Delete(T p)
        {
           data.Remove(p);
           db.SaveChanges();
        }
        //id ye göre deger dönderecegiz bu yuzdev deger döneceginden return ediyoruz
        public T GetById(int id)
        {
            return data.Find(id);
        }

        public void Insert(T p)
        {
            data.Add(p);
            db.SaveChanges();
        }

        public List<T> List()
        {
            var con = new denemeEntities();
            return con.Set<T>().ToList();
        }

        public void Update(T p)
        {
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {

                Console.WriteLine(e);
            }
        }
    }
}
