using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace dttests.Helpers
{
public static class EnumExtensions
{
    public static TAttribute GetAttribute<TAttribute>(this Enum value)
        where TAttribute : Attribute
    {
        var type = value.GetType();

            var name = Enum.GetName(type, value);
            return type.GetField(name) // I prefer to get attributes this way
                .GetCustomAttributes(false)
                .OfType<TAttribute>()
                .SingleOrDefault();



    }

    public static bool HasAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
    {
        var type = value.GetType();
        return type.GetCustomAttributes(typeof(TAttribute), false).Any();
    }
}

public class DataTablesColumnAttribute : Attribute
{
    public string Name { get; private set; }
    public int Target { get; private set; }
    public bool Visible { get; private set; }

    internal DataTablesColumnAttribute(string name)
    {
        this.Name = name;
    }

    internal DataTablesColumnAttribute(int target)
    {
        this.Target = target;
    }

    internal DataTablesColumnAttribute(bool visible)
    {
        this.Visible = visible;
    }
    
    internal DataTablesColumnAttribute(string name, int target)
    {
        this.Name = name;
        this.Target = target;
    }

    internal DataTablesColumnAttribute(string name, int target, bool visible)
    {
        this.Name = name;
        this.Target = target;
        this.Visible = visible;
    }


}}