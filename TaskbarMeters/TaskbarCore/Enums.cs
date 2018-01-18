using System;

namespace TaskbarCore
{
	[Flags]
	public enum DwmWindowAttribute
	{
		Flip3DPolicy = 8
	}

	public enum Flip3DPolicy
	{
		Default = 0,
		ExcludeBelow,
		ExcludeAbove
	}
}