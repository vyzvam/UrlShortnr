using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace App.Repository.Data
{
    public class EfRepository<T>: IRepository<T> where T : class
    {

        protected  DbContext Context ;
        protected readonly bool ShareContext;

        public EfRepository(DbContext context) : this(context, false) { }

        public EfRepository(DbContext context, bool shareContext) 
        {
            Context = context;
            ShareContext = shareContext;
        }

        protected DbSet<T> DbSet
        {
            get { return Context.Set<T>(); }
        }

        public IQueryable<T> All()
        {
            return DbSet.AsQueryable();
        }
        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Any(predicate);
        }

        public int Length
        {

            get { return DbSet.Count(); }
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Count(predicate);
        }

        public T Create(T t)
        {
            DbSet.Add(t);

            if (!ShareContext)
            {
                Context.SaveChanges();
            }

            return t;
        }

        public int Delete(T t)
        {
            DbSet.Remove(t);

            if (!ShareContext)
            {
                return Context.SaveChanges();
            }

            return 0;
        }

        public int Delete(Expression<Func<T, bool>> predicate)
        {
            var collection = FindAll(predicate);

            foreach (var item in collection)
            {
                DbSet.Remove(item);
            }

            if (!ShareContext)
            {
                return Context.SaveChanges();
            }

            return 0;
        }

        public T Find(params object[] keys)
        {
            return DbSet.Find(keys);
        }

        public T Find(Expression<Func<T, bool>> predicate)
        {
            //return DbSet.SingleOrDefault(predicate);
            return DbSet.FirstOrDefault(predicate);
        }

        public IQueryable<T> FindAll(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate).AsQueryable();
        }

        public IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, int index, int size)
        {
            var AmountToSkip = index * size;

            IQueryable<T> query = DbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (AmountToSkip != 0) { query.Skip(AmountToSkip); }
            return query.Take(size).AsQueryable();

        }

        public int Update(T t)
        {
            Context.Entry(t).CurrentValues.SetValues(t);

            DbSet.Attach(t);
            var entry = Context.Entry(t);
            entry.State = EntityState.Modified;

            if (!ShareContext) { return Context.SaveChanges(); }

            return 0;

        }
        
        public int Update(IQueryable<T> t)
        {

            foreach (var item in t)
            {
                DbSet.Attach(item);
                var entry = Context.Entry(item);
                entry.State = EntityState.Modified;
            }

            if (!ShareContext) { return Context.SaveChanges(); }

            return 0;
        }

        public void Dispose()
        {
            if (!ShareContext && Context != null)
            {
                try { Context.Dispose(); } catch { }
            }
        }

    }
}