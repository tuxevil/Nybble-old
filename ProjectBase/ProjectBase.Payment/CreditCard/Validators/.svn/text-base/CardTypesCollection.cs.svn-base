using System.Collections;

namespace ProjectBase.Payment.CreditCard.Validators
{
	/// <summary>
	/// A collection of elements of type CardType
	/// </summary>
	public class CardTypeCollection : CollectionBase
	{
		/// <summary>
		/// Initializes a new empty instance of the CardTypeCollection class.
		/// </summary>
		public CardTypeCollection()
		{
			// empty
		}

		/// <summary>
		/// Initializes a new instance of the CardTypeCollection class, containing elements
		/// copied from an array.
		/// </summary>
		/// <param name="items">
		/// The array whose elements are to be added to the new CardTypeCollection.
		/// </param>
		public CardTypeCollection(CardType[] items)
		{
			AddRange(items);
		}

		/// <summary>
		/// Initializes a new instance of the CardTypeCollection class, containing elements
		/// copied from another instance of CardTypeCollection
		/// </summary>
		/// <param name="items">
		/// The CardTypeCollection whose elements are to be added to the new CardTypeCollection.
		/// </param>
		public CardTypeCollection(CardTypeCollection items)
		{
			AddRange(items);
		}

		/// <summary>
		/// Gets or sets the CardType at the given index in this CardTypeCollection.
		/// </summary>
		public virtual CardType this[int index]
		{
			get { return (CardType) List[index]; }
			set { List[index] = value; }
		}

		/// <summary>
		/// Adds the elements of an array to the end of this CardTypeCollection.
		/// </summary>
		/// <param name="items">
		/// The array whose elements are to be added to the end of this CardTypeCollection.
		/// </param>
		public virtual void AddRange(CardType[] items)
		{
			foreach (CardType item in items)
			{
				List.Add(item);
			}
		}

		/// <summary>
		/// Adds the elements of another CardTypeCollection to the end of this CardTypeCollection.
		/// </summary>
		/// <param name="items">
		/// The CardTypeCollection whose elements are to be added to the end of this CardTypeCollection.
		/// </param>
		public virtual void AddRange(CardTypeCollection items)
		{
			foreach (CardType item in items)
			{
				List.Add(item);
			}
		}

		/// <summary>
		/// Adds an instance of type CardType to the end of this CardTypeCollection.
		/// </summary>
		/// <param name="value">
		/// The CardType to be added to the end of this CardTypeCollection.
		/// </param>
		public virtual void Add(CardType value)
		{
			List.Add(value);
		}

		/// <summary>
		/// Determines whether a specfic CardType value is in this CardTypeCollection.
		/// </summary>
		/// <param name="value">
		/// The CardType value to locate in this CardTypeCollection.
		/// </param>
		/// <returns>
		/// true if value is found in this CardTypeCollection;
		/// false otherwise.
		/// </returns>
		public virtual bool Contains(CardType value)
		{
			return List.Contains(value);
		}

		/// <summary>
		/// Return the zero-based index of the first occurrence of a specific value
		/// in this CardTypeCollection
		/// </summary>
		/// <param name="value">
		/// The CardType value to locate in the CardTypeCollection.
		/// </param>
		/// <returns>
		/// The zero-based index of the first occurrence of the _ELEMENT value if found;
		/// -1 otherwise.
		/// </returns>
		public virtual int IndexOf(CardType value)
		{
			return List.IndexOf(value);
		}

		/// <summary>
		/// Inserts an element into the CardTypeCollection at the specified index
		/// </summary>
		/// <param name="index">
		/// The index at which the CardType is to be inserted.
		/// </param>
		/// <param name="value">
		/// The CardType to insert.
		/// </param>
		public virtual void Insert(int index, CardType value)
		{
			List.Insert(index, value);
		}

		/// <summary>
		/// Removes the first occurrence of a specific CardType from this CardTypeCollection.
		/// </summary>
		/// <param name="value">
		/// The CardType value to remove from this CardTypeCollection.
		/// </param>
		public virtual void Remove(CardType value)
		{
			List.Remove(value);
		}

		/// <summary>
		/// Returns an enumerator that can iterate through the elements of this CardTypeCollection.
		/// </summary>
		/// <returns>
		/// An object that implements System.Collections.IEnumerator.
		/// </returns>        
		public new virtual Enumerator GetEnumerator()
		{
			return new Enumerator(this);
		}

		#region Nested type: Enumerator

		/// <summary>
		/// Type-specific enumeration class, used by CardTypeCollection.GetEnumerator.
		/// </summary>
		public class Enumerator : IEnumerator
		{
			private IEnumerator wrapped;

			public Enumerator(CardTypeCollection collection)
			{
				wrapped = ((CollectionBase) collection).GetEnumerator();
			}

			public CardType Current
			{
				get { return (CardType) (wrapped.Current); }
			}

			#region IEnumerator Members

			object IEnumerator.Current
			{
				get { return (CardType) (wrapped.Current); }
			}

			public bool MoveNext()
			{
				return wrapped.MoveNext();
			}

			public void Reset()
			{
				wrapped.Reset();
			}

			#endregion
		}

		#endregion
	}
}