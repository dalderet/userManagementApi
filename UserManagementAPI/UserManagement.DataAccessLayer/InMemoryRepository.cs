﻿namespace UserManagement.DataAccessLayer
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using UserManagement.Model.Api;

    public class InMemoryRepository<T> : IRepository<T> where T : class
    {
        private readonly ConcurrentDictionary<int, T> _repository = new ConcurrentDictionary<int, T>();

        public async Task<bool> CreateItemAsync(T item)
        {
            var id = GetIdValueFromItem(item);
            return await CreateItemAsync(id, item);
        }

        public async Task CreateItemsAsync(List<T> items)
        {
            foreach (var item in items)
            {
                var id = GetIdValueFromItem(item);
                await CreateItemAsync(id, item);
            }
        }

        private Task<bool> CreateItemAsync(int id, T item)
        {
            bool added = _repository.TryAdd(id, item);
            return Task.FromResult(added);
        }

        public async Task<int> CreateItemAndReturnAutogeneratedIdAsync(T item)
        {
            int id = GetIdValueFromItem(item);
            await CreateItemAsync(id, item);
            return id;
        }

        public Task<T> GetItemAsync(int id, bool ifNotExistsReturnDefaultValue = false)
        {
            if (_repository.TryGetValue(id, out T value))
            {
                return Task.FromResult(value);
            }
            else
            {
                if (!ifNotExistsReturnDefaultValue)
                    throw new KeyNotFoundException($"Item {id} not found");
                return Task.FromResult((T)Activator.CreateInstance(typeof(T)));
            }
        }

        public Task<IEnumerable<T>> GetItemsAsync(Expression<Func<T, bool>> predicate)
        {
            return Task.FromResult((IEnumerable<T>)_repository.Values.AsQueryable().Where(predicate));
        }

        private int GetIdValueFromItem(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (item.GetType().GetProperty("Id") != null)
            {
                return (int)typeof(T).GetProperty("Id")?.GetValue(item);
            }
            throw new ArgumentException(nameof(item));
        }

        public Task<bool> ExistItemAsync(int id)
        {
            return Task.FromResult(_repository.TryGetValue(id, out _));
        }

        public Task DeleteItemAsync(int id)
        {
            _repository.TryRemove(id, out _);
            return Task.CompletedTask;
        }

        public Task UpdateItemAsync(int id, T item)
        {
            _repository[id] = item;
            return Task.CompletedTask;
        }

        public async Task UpsertItemAsync(T item)
        {
            var id = GetIdValueFromItem(item);
            if (_repository.ContainsKey(id))
                await UpdateItemAsync(id, item);
            else
                await CreateItemAsync(id, item);
        }

        public void Dispose()
        {
            // Method intentionally left empty.
        }
    }
}
