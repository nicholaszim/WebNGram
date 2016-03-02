﻿using BAL.Intefaces;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Repositories
{
	public class BaseRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
	{
		internal MainContext context;
		internal DbSet<TEntity> dbSet;

		public BaseRepository(MainContext context)
		{
			this.context = context;
			this.dbSet = context.Set<TEntity>();
		}

		public virtual IQueryable<TEntity> All
		{
			get
			{
				return dbSet;
			}
		}

		public virtual IEnumerable<TEntity> Get(
			Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includeProperties = "")
		{
			IQueryable<TEntity> query = dbSet;

			if (filter != null)
			{
				query = query.Where(filter);
			}

			foreach (var includeProperty in includeProperties.Split
				(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProperty);
			}

			if (orderBy != null)
			{
				return orderBy(query).ToList();
			}
			else
			{
				return query.ToList();
			}
		}

		public virtual void Insert(TEntity entity)
		{
			dbSet.Add(entity);
		}

		public virtual void Delete(TEntity entityToDelete)
		{
			if (context.Entry(entityToDelete).State == EntityState.Detached)
			{
				dbSet.Attach(entityToDelete);
			}
			dbSet.Remove(entityToDelete);
		}

		public virtual void Update(TEntity entityToUpdate)
		{
			try
			{
				dbSet.Attach(entityToUpdate);
			}
			catch { }
			context.Entry(entityToUpdate).State = EntityState.Modified;
		}

		public void SetStateModified(TEntity entity)
		{
			context.Entry<TEntity>(entity).State = EntityState.Modified;
		}
	}
}
