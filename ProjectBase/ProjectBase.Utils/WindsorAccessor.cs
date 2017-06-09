using System;
using System.Collections.Generic;
using System.Text;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;

namespace ProjectBase.Utils
{
	public sealed class WindsorAccessor : IContainerAccessor
	{
		#region Thread-safe, lazy Singleton

		/// <summary>
		/// This is a thread-safe, lazy singleton.  See http://www.yoda.arachsys.com/csharp/singleton.html
		/// for more details about its implementation.
		/// </summary>
		public static WindsorAccessor Instance
		{
			get
			{
				return Nested.WindsorAccessor;
			}
		}

		/// <summary>
		/// Private constructor to enforce singleton
		/// </summary>
		private WindsorAccessor( )
		{
			windsorContainer = new WindsorContainer( new XmlInterpreter( ) );
		}

		/// <summary>
		/// Assists with ensuring thread-safe, lazy singleton
		/// </summary>
		private class Nested
		{
			static Nested( ) { }
			internal static readonly WindsorAccessor WindsorAccessor =
				new WindsorAccessor( );
		}

		#endregion

		#region IContainerAccessor Members

		private readonly IWindsorContainer windsorContainer;

		public IWindsorContainer Container
		{
			get { return windsorContainer; }
		}

		#endregion
	}
}
