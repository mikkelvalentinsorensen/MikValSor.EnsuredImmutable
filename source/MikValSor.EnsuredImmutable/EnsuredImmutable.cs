using MikValSor.Runtime.Serialization;
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

			if (SerializableValidator.Instance.IsSerializable(immutable))
			{
				return new EnsuredImmutableAndSerializable<T>(immutable);
			}
			return new EnsuredImmutable<T>(immutable);
		}
	}

	/// <summary>
	///		Class used to ensure that objects are immutable
	/// </summary>
	public class EnsuredImmutable<T> : EnsuredImmutable
	{
		/// <summary>
		///		Value that is ensured immutable.
		/// </summary>
		public readonly T Value;

		/// <summary>
		///		Indicate if value is serializable.
		/// </summary>
		public readonly bool Serializable;

		internal EnsuredImmutable(T value, bool serializable = false)
		{
			Value = value;
			Serializable = serializable;
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

		/// <summary>
		///		Ensures that Value is serializable.
		/// </summary>
		/// <returns>
		///		Returns EnsuredImmutableAndSerializable´1 with the Value.
		/// </returns>
		public EnsuredImmutableAndSerializable<T> EnsureSerializable()
		{
			var reply = this as EnsuredImmutableAndSerializable<T>;
			if (reply != null) return reply;

			//Not serializable.
			SerializableValidator.Instance.EnsureSerializable(Value);
			throw new ApplicationException("SerializableValidator.Instance.EnsureSerializable(Value) should have thrown exception.");
		}
	}

	/// <summary>
	///		Class used to ensure that objects are immutable
	/// </summary>
	public sealed class EnsuredImmutableAndSerializable<T> : EnsuredImmutable<T>
	{
		internal EnsuredImmutableAndSerializable(T value) : base(value, true)
		{
		}
	}
}
