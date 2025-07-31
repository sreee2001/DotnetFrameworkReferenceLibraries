using Infrastructure.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;

namespace Repository.Database
{
    /// <summary>
    /// Provides a base implementation for a repository pattern, enabling data access and manipulation for entities of
    /// type <typeparamref name="T"/>. This class is designed to work with an underlying database service and supports
    /// common operations such as retrieval, saving, and deletion of entities.
    /// </summary>
    /// <remarks>This abstract class serves as a foundation for implementing repositories that interact with a
    /// database. It provides a set of common methods for CRUD operations and enforces the implementation of certain
    /// behaviors, such as clearing dirty flags and marking entities as deleted, through abstract methods.</remarks>
    /// <typeparam name="T">The type of entity managed by the repository. Must inherit from <see cref="Entity"/> and have a parameterless
    /// constructor.</typeparam>
    public abstract class Repository<T> : IRepository<T> where T : Entity, new()
    {
        #region Protected Members

        [Import]
        protected IDbService DbService { get; set; }

        private DbSet<T> ModelDbSet => DbService.Set<T>();

        protected Action PostSaveAction = () => { };
        protected Action PostSaveAllAction = () => { };

        #endregion

        /// <inheritdoc />
        public T GetById(int id)
        {
            return ModelDbSet.Select(b => b).FirstOrDefault(b => b.Id == id);
        }

        /// <inheritdoc />
        public IQueryable<T> GetAll()
        {
            return ModelDbSet;
        }

        /// <inheritdoc />
        public void Save(T model)
        {
            model.SetAuditInfo();
            if (model.Id == 0)
                ModelDbSet.Add(model);
            Commit();
            ClearDirtyFlag(model);
            PostSaveAction?.Invoke();
        }

        /// <inheritdoc />
        public void Save(IEnumerable<T> models)
        {
            foreach (T model in models)
                model.SetAuditInfo();
            ModelDbSet.AddRange(models.Where(b => b.Id == 0));
            Commit();
            ClearDirtyFlag(models);
            PostSaveAllAction?.Invoke();
        }


        /// <inheritdoc />
        private void Commit()
        {
            DbService.SaveChanges();
        }

        /// <inheritdoc />
        public abstract void ClearDirtyFlag(T model);

        /// <inheritdoc />
        public void ClearDirtyFlag(IEnumerable<T> models)
        {
            foreach (T model in models)
                ClearDirtyFlag(model);
        }

        /// <inheritdoc />
        public void Delete(T model)
        {
            MarkAsDelete(model);
            Commit();
        }

        /// <inheritdoc />
        public void Delete(IEnumerable<T> models)
        {
            MarkAsDelete(models);
            Commit();
        }

        /// <inheritdoc />
        public abstract void MarkAsDelete(T model);

        /// <summary>
        /// Marks the specified entity as deleted by removing it from the database context.
        /// </summary>
        /// <remarks>This method removes the entity from the database context, which will result in its
        /// deletion         during the next save operation. Ensure that the entity is properly tracked by the database
        /// context         before calling this method.</remarks>
        /// <typeparam name="TEntity">The type of the entity to be marked as deleted. Must inherit from <see cref="Entity"/>.</typeparam>
        /// <param name="entity">The entity instance to be marked as deleted. Cannot be <see langword="null"/>.</param>
        protected void MarkEntityAsDeleted<TEntity>(TEntity entity) where TEntity : Entity
        {
            DbService.Remove(entity);
        }

        /// <inheritdoc />
        public void MarkAsDelete(IEnumerable<T> models)
        {
            List<T> list = models.ToList();
            foreach (T model in list)
            {
                MarkAsDelete(model);
            }
        }

        /// <inheritdoc />
        public virtual void Refresh(int id)
        {
            if (id == 0)
                return;
            T entity = GetById(id);
            DbService.Refresh(entity);
        }

        /// <inheritdoc />
        public void ResetContext()
        {
            DbService.ResetContext();
        }
    }
}
