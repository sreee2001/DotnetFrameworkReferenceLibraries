using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Repository.Extensions
{
    public static class DbSetExtension
    {
        /// <summary>
        /// Adds SortableEntity into a DbSet using a list of strings
        /// </summary>
        /// <param name="set"></param>
        /// <param name="values"></param>
        /// <typeparam name="T"></typeparam>
        public static void SortableEntityAddValues<T>(this DbSet<T> set, List<string> values) where T : SortableEntity, new()
        {
            // Validate the DbSet and values list
            {
                // Ensure the DbSet is not null
                if (set == null)
                {
                    throw new ArgumentNullException(nameof(set), "The DbSet cannot be null.");
                }

                // Ensure the values list is not null or empty
                if (values == null)
                {
                    throw new ArgumentNullException(nameof(values), "The values list cannot be null.");
                }
            }

            // Store out the maximum Id in the DbSet
            int maxId = -1;

            // Check if the DbSet already contains entities, then find out the largest ID value
            {
                if (set.Local.Count > 0)
                {
                    foreach (var entity in set.Local)
                    {
                        if (entity.Id > maxId)
                        {
                            maxId = entity.Id;
                        }
                    }
                    // Update the Ids of the new entities to avoid conflicts
                    for (int i = 0; i < values.Count; i++)
                    {
                        values[i] = $"{maxId + i + 1}: {values[i]}";
                    }
                }
            }

            int index = maxId + 1;
            foreach (var value in values)
            {
                // check if the value is null or empty
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Value cannot be null or empty.", nameof(values));
                }
                // check if the value already exists in the DbSet
                if (set.Local.Any(e => e.Name == value))
                {
                    //throw new InvalidOperationException($"The value '{value}' already exists in the DbSet.");
                    //SKIP the duplicate value
                }
                else
                {
                    set.Add(new T { Id = index, Name = value, SortOrder = index });
                    ++index;
                }
            }
        }
    }
}
