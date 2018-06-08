using MikValSor.Runtime.Serialization;
using System;

namespace MikValSor.Immutable
{
	/// <summary>
	///		Class used to ensure that objects are immutable
	/// </summary>
	public abstract class EnsuredImmutable
	{
		internal EnsuredImmutable(object value, bool serializable)
		{
			this.value = value;
			this.serializable = serializable;
		}

		/// <summary>
		///		Indicate if value is serializable.
		/// </summary>
		public bool Serializable => serializable;
		private readonly bool serializable;

		/// <summary>
		///		Value that is ensured immutable.
		/// </summary>
		public object Value => value;
		private readonly object value;

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
		public new T Value => value;
		private readonly T value;

		internal EnsuredImmutable(T value, bool serializable = false) : base(value, serializable)
		{
			this.value = value;
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
		/// <exception cref="NotSerializableException">
		///		throws MikValSor.Runtime.Serialization.NotSerializableException if type is not serializable.
		/// </exception>
		public EnsuredImmutableAndSerializable<T> EnsureSerializable()
		{
			if (this is EnsuredImmutableAndSerializable<T> reply) return reply;

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
