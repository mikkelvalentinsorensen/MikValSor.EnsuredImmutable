namespace MikValSor.Immutable.Extensions
{
	/// <summary>
	///		Class used to ensure that objects are immutable
	/// </summary>
	public static class EnsuredImmutableExtender
	{
		/// <summary>
		///		Validates object is immutable and encapsulated object in an MikValSor.Immutable.EnsuredImmutable´1.
		/// </summary>
		/// <typeparam name="T">
		///		type if immutable object.
		/// </typeparam>
		/// <param name="immutable">
		///		Object to be validated as immutable.
		/// </param>
		/// <returns>
		///		Returns EnsuredImmutable with immutable object.
		/// </returns>
		/// <exception cref="NotImmutableException">
		///		throws a MikValSor.Immutable.NotImmutableException exception if immutable is mutable.
		/// </exception>
		public static EnsuredImmutable<T> EnsureImmutable<T>(this T immutable)
		{
			return EnsuredImmutable.Create(immutable);
		}
	}
}
