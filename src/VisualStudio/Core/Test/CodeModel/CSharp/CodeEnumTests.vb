' Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

Imports System.Runtime.InteropServices
Imports Microsoft.CodeAnalysis
Imports Microsoft.CodeAnalysis.Text
Imports Roslyn.Test.Utilities

Namespace Microsoft.VisualStudio.LanguageServices.UnitTests.CodeModel.CSharp
    Public Class CodeEnumTests
        Inherits AbstractCodeEnumTests

#Region "Access tests"

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Access1()
            Dim code =
<Code>
enum $$E { }
</Code>

            TestAccess(code, EnvDTE.vsCMAccess.vsCMAccessProject)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Access2()
            Dim code =
<Code>
class C
{
    enum $$E { }
}
</Code>

            TestAccess(code, EnvDTE.vsCMAccess.vsCMAccessPrivate)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Access3()
            Dim code =
<Code>
private enum $$E { }
</Code>

            TestAccess(code, EnvDTE.vsCMAccess.vsCMAccessPrivate)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Access4()
            Dim code =
<Code>
protected enum $$E { }
</Code>

            TestAccess(code, EnvDTE.vsCMAccess.vsCMAccessProtected)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Access5()
            Dim code =
<Code>
protected internal enum $$E { }
</Code>

            TestAccess(code, EnvDTE.vsCMAccess.vsCMAccessProjectOrProtected)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Access6()
            Dim code =
<Code>
internal enum $$E { }
</Code>

            TestAccess(code, EnvDTE.vsCMAccess.vsCMAccessProject)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Access7()
            Dim code =
<Code>
public enum $$E { }
</Code>

            TestAccess(code, EnvDTE.vsCMAccess.vsCMAccessPublic)
        End Sub

#End Region

#Region "Bases tests"

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Bases1()
            Dim code =
<Code>
enum $$E
{
    Foo = 1,
    Bar
}</Code>

            TestBases(code, IsElement("Enum"))
        End Sub

#End Region

#Region "Attributes tests"

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Attributes1()
            Dim code =
<Code>
enum $$C
{
    Foo = 1
}
</Code>

            TestAttributes(code, NoElements)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Attributes2()
            Dim code =
<Code>
using System;

[Flags]
enum $$C
{
    Foo = 1
}
</Code>

            TestAttributes(code, IsElement("Flags"))
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Attributes3()
            Dim code =
<Code>using System;

[Serializable]
[Flags]
enum $$C
{
    Foo = 1
}
</Code>

            TestAttributes(code, IsElement("Serializable"), IsElement("Flags"))
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Attributes4()
            Dim code =
<Code>using System;

[Serializable, Flags]
enum $$C
{
    Foo = 1
}
</Code>

            TestAttributes(code, IsElement("Serializable"), IsElement("Flags"))
        End Sub
#End Region

#Region "FullName tests"

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub FullName1()
            Dim code =
<Code>
enum $$E
{
    Foo = 1,
    Bar
}</Code>

            TestFullName(code, "E")
        End Sub

#End Region

#Region "Name tests"

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Name1()
            Dim code =
<Code>
enum $$E
{
    Foo = 1,
    Bar
}
</Code>

            TestName(code, "E")
        End Sub

#End Region

#Region "AddAttribute tests"
        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub AddAttribute1()
            Dim code =
<Code>
using System;

enum $$E
{
    Foo = 1,
    Bar
}
</Code>

            Dim expected =
<Code>
using System;

[Flags()]
enum E
{
    Foo = 1,
    Bar
}
</Code>
            TestAddAttribute(code, expected, New AttributeData With {.Name = "Flags"})
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub AddAttribute2()
            Dim code =
<Code>
using System;

[Flags]
enum $$E
{
    Foo = 1,
    Bar
}
</Code>

            Dim expected =
<Code>
using System;

[Flags]
[CLSCompliant(true)]
enum E
{
    Foo = 1,
    Bar
}
</Code>
            TestAddAttribute(code, expected, New AttributeData With {.Name = "CLSCompliant", .Value = "true", .Position = 1})
        End Sub

#End Region

#Region "AddMember tests"

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub AddMember1()
            Dim code =
<Code>
enum $$E
{
}
</Code>

            Dim expected =
<Code>
enum E
{
    V
}
</Code>

            TestAddEnumMember(code, expected, New EnumMemberData With {.Name = "V"})
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub AddMember2()
            Dim code =
<Code>
enum $$E
{
}
</Code>

            Dim expected =
<Code>
enum E
{
    V = 1
}
</Code>

            TestAddEnumMember(code, expected, New EnumMemberData With {.Name = "V", .Value = "1"})
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub AddMember3()
            Dim code =
<Code>
enum $$E
{
    V
}
</Code>

            Dim expected =
<Code>
enum E
{
    U = V,
    V
}
</Code>

            TestAddEnumMember(code, expected, New EnumMemberData With {.Name = "U", .Value = "V"})
        End Sub

        <WorkItem(638225)>
        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub AddMember4()
            Dim code =
<Code>
enum $$E
{
    A
}
</Code>

            Dim expected =
<Code>
enum E
{
    A,
    B
}
</Code>

            TestAddEnumMember(code, expected, New EnumMemberData With {.Position = -1, .Name = "B"})
        End Sub

        <WorkItem(638225)>
        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub AddMember5()
            Dim code =
<Code>
enum $$E
{
    A,
    C
}
</Code>

            Dim expected =
<Code>
enum E
{
    A,
    B,
    C
}
</Code>

            TestAddEnumMember(code, expected, New EnumMemberData With {.Position = 1, .Name = "B"})
        End Sub

        <WorkItem(638225)>
        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub AddMember6()
            Dim code =
<Code>
enum $$E
{
    A,
    B
}
</Code>

            Dim expected =
<Code>
enum E
{
    A,
    B,
    C
}
</Code>

            TestAddEnumMember(code, expected, New EnumMemberData With {.Position = -1, .Name = "C"})
        End Sub

        <WorkItem(638225)>
        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub AddMember7()
            Dim code =
<Code>
enum $$E
{
    A,
    B,
    C
}
</Code>

            Dim expected =
<Code>
enum E
{
    A,
    B,
    C,
    D
}
</Code>

            TestAddEnumMember(code, expected, New EnumMemberData With {.Position = -1, .Name = "D"})
        End Sub

#End Region

#Region "RemoveMember tests"

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub RemoveMember1()
            Dim code =
<Code>
enum $$E
{
    A
}
</Code>

            Dim expected =
<Code>
enum E
{
}
</Code>

            TestRemoveChild(code, expected, "A")
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub RemoveMember2()
            Dim code =
<Code>
enum $$E
{
    A,
    B
}
</Code>

            Dim expected =
<Code>
enum E
{
    B
}
</Code>

            TestRemoveChild(code, expected, "A")
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub RemoveMember3()
            Dim code =
<Code>
enum $$E
{
    A,
    B
}
</Code>

            Dim expected =
<Code>
enum E
{
    A
}
</Code>

            TestRemoveChild(code, expected, "B")
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub RemoveMember4()
            Dim code =
<Code>
enum $$E
{
    A,
    B,
    C
}
</Code>

            Dim expected =
<Code>
enum E
{
    A,
    C
}
</Code>

            TestRemoveChild(code, expected, "B")
        End Sub

#End Region

#Region "Set Name tests"
        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub SetName1()
            Dim code =
<Code>
enum $$Foo
{
}
</Code>

            Dim expected =
<Code>
enum Bar
{
}
</Code>

            TestSetName(code, expected, "Bar", NoThrow(Of String)())
        End Sub
#End Region

        Protected Overrides ReadOnly Property LanguageName As String
            Get
                Return LanguageNames.CSharp
            End Get
        End Property

    End Class
End Namespace
