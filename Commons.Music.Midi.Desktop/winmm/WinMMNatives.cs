﻿using System;
using System.Runtime.InteropServices;

namespace Commons.Music.Midi.WinMM
{
	[StructLayout (LayoutKind.Sequential)]
	struct MidiInCaps
	{
		public short Mid;
		public short Pid;
		public ushort DriverVersion;
		[MarshalAs (UnmanagedType.LPStr, SizeConst = WinMMNatives.MaxPNameLen)]
		public string Name;
		public int Support;
	}

	[StructLayout (LayoutKind.Sequential)]
	struct MidiOutCaps
	{
		public short Mid;
		public short Pid;
		public ushort DriverVersion;
		[MarshalAs (UnmanagedType.LPStr, SizeConst = WinMMNatives.MaxPNameLen)]
		public string Name;
		public short Technology;
		public short Voices;
		public short Notes;
		public short ChannelMask;
		public int Support;
	}

	[StructLayout (LayoutKind.Sequential)]
	struct MidiHdr
	{
		public IntPtr Data;
		public int BufferLength;
		public int BytesRecorded;
		public IntPtr User;
		public int Flags;
		public IntPtr Next; // of MidiHdr
		public IntPtr Reserved;
		public int Offset;
		public IntPtr Reserved2;
	}

	public enum MidiInOpenFlags
	{
		Null,
		Function,
		Thread,
		Window,
		MidiIOStatus,
	}

	public enum MidiOutOpenFlags
	{
		Null,
		Function,
		Thread,
		Window,
		Event,
	}

	public delegate void MidiInProc (IntPtr midiIn, uint msg, ref int instance, ref int param1, ref int param2);
	public delegate void MidiOutProc (IntPtr midiOut, uint msg, ref int instance, ref int param1, ref int param2);

	public static class WinMMNatives
	{
		public const string LibraryName = "winmm";
		public const int MaxPNameLen = 32;

		[DllImport (LibraryName)]
		internal static extern int midiInGetNumDevs ();

		[DllImport (LibraryName)]
		internal static extern int midiOutGetNumDevs ();

		[DllImport (LibraryName)]
		internal static extern int midiInGetDevCaps (ref uint uDeviceID, out MidiInCaps midiInCaps, uint sizeOfMidiInCaps);

		[DllImport (LibraryName)]
		internal static extern int midiOutGetDevCaps (ref uint uDeviceID, out MidiOutCaps midiOutCaps, uint sizeOfMidiOutCaps);

		[DllImport (LibraryName)]
		internal static extern int midiInOpen (out IntPtr midiIn, uint deviceID, MidiInProc callback, IntPtr callbackInstance, MidiInOpenFlags flags);

		[DllImport(LibraryName)]
		internal static extern int midiOutOpen (out IntPtr midiIn, uint deviceID, MidiOutProc callback, IntPtr callbackInstance, MidiOutOpenFlags flags);

		[DllImport(LibraryName)]
		internal static extern int midiInClose (IntPtr midiIn);

		[DllImport (LibraryName)]
		internal static extern int midiOutClose (IntPtr midiIn);

		[DllImport (LibraryName)]
		internal static extern int midiOutMessage (IntPtr deviceID, uint msg, ref int dw1, ref int dw2);

		[DllImport(LibraryName)]
		internal static extern int midiOutShortMsg (IntPtr deviceID, uint msg);

		[DllImport(LibraryName)]
		internal static extern int midiOutLongMsg (IntPtr deviceID, uint msg, [MarshalAs (UnmanagedType.LPArray, SizeParamIndex = 3)] byte [] midiOutHdr, int midiOutHdrSize);
	}
}
