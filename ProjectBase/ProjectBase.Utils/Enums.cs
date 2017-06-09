using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field, AllowMultiple = false)]
public sealed class EnumDescriptionAttribute : Attribute
{
	private string description;

	public EnumDescriptionAttribute(string description)
	{
		this.description = description;
	}

	public string Description
	{
		get { return description; }
	}
}

public static class EnumHelper
{
	public static string GetDescription(Enum value)
	{
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}

		string description = value.ToString();
		FieldInfo fieldInfo = value.GetType().GetField(description);
		EnumDescriptionAttribute[] attributes =
			(EnumDescriptionAttribute[]) fieldInfo.GetCustomAttributes(typeof (EnumDescriptionAttribute), false);

		if (attributes != null && attributes.Length > 0)
		{
			description = attributes[0].Description;
		}
		return description;
	}

	public static IList GetList(Type type)
	{
		if (type == null)
		{
			throw new ArgumentNullException("type");
		}

		ArrayList list = new ArrayList();
		Array enumValues = Enum.GetValues(type);

		foreach (Enum value in enumValues)
		{
			list.Add(new KeyValuePair<Enum, string>(value, GetDescription(value)));
		}

		return list;
	}
}