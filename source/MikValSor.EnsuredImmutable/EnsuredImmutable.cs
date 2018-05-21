using System;

namespace MikValSor.Immutable
{
	/// <summary>
	///		Class used to ensure that objects are immutable
	/// </summary>
	public abstract class EnsuredImmutable
	{
		internal EnsuredImmutable()
		{
		}

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
		public static EnsuredImmutable<T> Create<T>(T immutable)
		{
			ImmutableValidator.Instance.EnsureImmutable(immutable);
			return new EnsuredImmutable<T>(immutable);
		}
	}

	/// <summary>
	///		Class used to ensure that objects are immutable
	/// </summary>
	public sealed class EnsuredImmutable<T> : EnsuredImmutable
	{
		/// <summary>
		///		Value that is ensured immutable.
		/// </summary>
		public readonly T Value;

		internal EnsuredImmutable(T value)
		{
			Value = value;
		}

		/// <summary>
		///		Overrides extension methode.
		/// </summary>
		/// <returns>
		///		Method parent object.
		/// </returns>
		public EnsuredImmutable<T> EnsureImmutable()
		{
			return this;
		}
	}
}
