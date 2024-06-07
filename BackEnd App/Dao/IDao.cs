namespace BackEnd_App.Dao
{
    public interface IDao<T>
    {
        /// <summary>
        /// Adds a new element of type T to a table.
        /// </summary>
        /// <param name="element">The new element to add to the table.</param>
        /// <returns>A task that represents the asynchronous operation of adding the element to the table and saving changes to the database.</returns>
        Task<int> Add(T element);

        /// <summary>
        /// Removes an element of the specified type from the table using the element's ID.
        /// </summary>
        /// <param name="id">The ID of the element to be removed.</param>
        /// <returns>A task that represents the asynchronous operation of removing the element from the table and saving changes to the database.</returns>
        Task RemoveById(int id);

        /// <summary>
        /// Updates an element in the table.
        /// </summary>
        /// <param name="element">The updated element to be saved in the table.</param>
        /// <returns>A task that represents the asynchronous operation of updating the element in the table and saving changes to the database.</returns>
        Task Update(T element);

        /// <summary>
        /// Gets the element with the specified ID from the table.
        /// </summary>
        /// <param name="id">The ID of the element to retrieve.</param>
        /// <returns>The element with the specified ID.</returns>
        T GetById(int id);

        /// <summary>
        /// Retrieves all elements of the specified type from the table.
        /// </summary>
        /// <returns>A list of all elements of the specified type in the table.</returns>
        List<T> GetAll();
    }
}
