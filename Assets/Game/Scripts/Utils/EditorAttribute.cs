using System;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
public sealed class ETitleAttribute : Attribute
{
    public string Title { get; private set; }

    public ETitleAttribute(string title)
    {
        Title = title;
    }
}

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
public sealed class ShowIfAttribute : Attribute
{
    /// <summary>
    /// Name of a bool field, property or function to show or hide the property.
    /// </summary>
    public string MemberName { get; private set; }

    /// <summary>
    /// Initializes a new instance of the Sirenix.OdinInspector.ShowIfAttribute class.
    /// </summary>
    /// <param name="memberName">Name of a bool field, property or function to show or hide the property.</param>
    public ShowIfAttribute(string memberName)
    {
        MemberName = memberName;
    }
}

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
public sealed class HideIfAttribute : Attribute
{
    /// <summary>
    /// Name of a bool field, property or function to show or hide the property.
    /// </summary>
    public string MemberName { get; private set; }

    /// <summary>
    /// Initializes a new instance of the Sirenix.OdinInspector.HideIfAttribute class.
    /// </summary>
    /// <param name="memberName">Name of a bool field, property or function to show or hide the property.</param>
    public HideIfAttribute(string memberName)
    {
        MemberName = memberName;
    }
}