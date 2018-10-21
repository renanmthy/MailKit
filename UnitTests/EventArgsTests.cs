﻿//
// EventArgsTests.cs
//
// Author: Jeffrey Stedfast <jestedfa@microsoft.com>
//
// Copyright (c) 2013-2018 Xamarin Inc. (www.xamarin.com)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//

using System;
using System.Collections;
using System.Collections.Generic;

using NUnit.Framework;

using MailKit;

namespace UnitTests {
	[TestFixture]
	public class EventArgsTests
	{
		[Test]
		public void TestAlertEventArgs ()
		{
			var args = new AlertEventArgs ("Klingons on the starboard bow!");

			Assert.AreEqual ("Klingons on the starboard bow!", args.Message);

			Assert.Throws<ArgumentNullException> (() => new AlertEventArgs (null));
		}

		[Test]
		public void TestAuthenticatedEventArgs ()
		{
			var args = new AuthenticatedEventArgs ("Access Granted.");

			Assert.AreEqual ("Access Granted.", args.Message);

			Assert.Throws<ArgumentNullException> (() => new AuthenticatedEventArgs (null));
		}

		[Test]
		public void TestFolderRenamedEventArgs ()
		{
			var args = new FolderRenamedEventArgs ("Istanbul", "Constantinople");

			Assert.AreEqual ("Istanbul", args.OldName);
			Assert.AreEqual ("Constantinople", args.NewName);

			Assert.Throws<ArgumentNullException> (() => new FolderRenamedEventArgs (null, "name"));
			Assert.Throws<ArgumentNullException> (() => new FolderRenamedEventArgs ("name", null));
		}

		[Test]
		public void TestMessageEventArgs ()
		{
			var args = new MessageEventArgs (0);

			Assert.AreEqual (0, args.Index);

			Assert.Throws<ArgumentOutOfRangeException> (() => new MessageEventArgs (-1));
		}

		[Test]
		public void TestMessageFlagsChangedEventArgs ()
		{
			var userFlags = new HashSet<string> (new [] { "custom1", "custom2" });
			MessageFlagsChangedEventArgs args;
			var uid = new UniqueId (5);
			ulong modseq = 724;

			args = new MessageFlagsChangedEventArgs (0);
			Assert.AreEqual (0, args.UserFlags.Count);
			Assert.AreEqual (MessageFlags.None, args.Flags);
			Assert.IsFalse (args.UniqueId.HasValue);
			Assert.IsFalse (args.ModSeq.HasValue);
			Assert.AreEqual (0, args.Index);

			args = new MessageFlagsChangedEventArgs (0, MessageFlags.Answered);
			Assert.AreEqual (0, args.UserFlags.Count);
			Assert.AreEqual (MessageFlags.Answered, args.Flags);
			Assert.IsFalse (args.UniqueId.HasValue);
			Assert.IsFalse (args.ModSeq.HasValue);
			Assert.AreEqual (0, args.Index);

			args = new MessageFlagsChangedEventArgs (0, MessageFlags.Answered, modseq);
			Assert.AreEqual (0, args.UserFlags.Count);
			Assert.AreEqual (MessageFlags.Answered, args.Flags);
			Assert.IsFalse (args.UniqueId.HasValue);
			Assert.AreEqual (modseq, args.ModSeq);
			Assert.AreEqual (0, args.Index);

			args = new MessageFlagsChangedEventArgs (0, MessageFlags.Answered, userFlags);
			Assert.AreEqual (userFlags.Count, args.UserFlags.Count);
			Assert.AreEqual (MessageFlags.Answered, args.Flags);
			Assert.IsFalse (args.UniqueId.HasValue);
			Assert.IsFalse (args.ModSeq.HasValue);
			Assert.AreEqual (0, args.Index);

			args = new MessageFlagsChangedEventArgs (0, MessageFlags.Answered, userFlags, modseq);
			Assert.AreEqual (userFlags.Count, args.UserFlags.Count);
			Assert.AreEqual (MessageFlags.Answered, args.Flags);
			Assert.IsFalse (args.UniqueId.HasValue);
			Assert.AreEqual (modseq, args.ModSeq);
			Assert.AreEqual (0, args.Index);

			args = new MessageFlagsChangedEventArgs (0, uid, MessageFlags.Answered);
			Assert.AreEqual (0, args.UserFlags.Count);
			Assert.AreEqual (MessageFlags.Answered, args.Flags);
			Assert.AreEqual (uid, args.UniqueId);
			Assert.IsFalse (args.ModSeq.HasValue);
			Assert.AreEqual (0, args.Index);

			args = new MessageFlagsChangedEventArgs (0, uid, MessageFlags.Answered, modseq);
			Assert.AreEqual (0, args.UserFlags.Count);
			Assert.AreEqual (MessageFlags.Answered, args.Flags);
			Assert.AreEqual (uid, args.UniqueId);
			Assert.AreEqual (modseq, args.ModSeq);
			Assert.AreEqual (0, args.Index);

			args = new MessageFlagsChangedEventArgs (0, uid, MessageFlags.Answered, userFlags);
			Assert.AreEqual (userFlags.Count, args.UserFlags.Count);
			Assert.AreEqual (MessageFlags.Answered, args.Flags);
			Assert.AreEqual (uid, args.UniqueId);
			Assert.IsFalse (args.ModSeq.HasValue);
			Assert.AreEqual (0, args.Index);

			args = new MessageFlagsChangedEventArgs (0, uid, MessageFlags.Answered, userFlags, modseq);
			Assert.AreEqual (userFlags.Count, args.UserFlags.Count);
			Assert.AreEqual (MessageFlags.Answered, args.Flags);
			Assert.AreEqual (uid, args.UniqueId);
			Assert.AreEqual (modseq, args.ModSeq);
			Assert.AreEqual (0, args.Index);

			Assert.Throws<ArgumentOutOfRangeException> (() => new MessageFlagsChangedEventArgs (-1));
			Assert.Throws<ArgumentOutOfRangeException> (() => new MessageFlagsChangedEventArgs (-1, MessageFlags.Answered));
			Assert.Throws<ArgumentOutOfRangeException> (() => new MessageFlagsChangedEventArgs (-1, MessageFlags.Answered, modseq));
			Assert.Throws<ArgumentOutOfRangeException> (() => new MessageFlagsChangedEventArgs (-1, MessageFlags.Answered, userFlags));
			Assert.Throws<ArgumentOutOfRangeException> (() => new MessageFlagsChangedEventArgs (-1, MessageFlags.Answered, userFlags, modseq));
			Assert.Throws<ArgumentOutOfRangeException> (() => new MessageFlagsChangedEventArgs (-1, uid, MessageFlags.Answered));
			Assert.Throws<ArgumentOutOfRangeException> (() => new MessageFlagsChangedEventArgs (-1, uid, MessageFlags.Answered, modseq));
			Assert.Throws<ArgumentOutOfRangeException> (() => new MessageFlagsChangedEventArgs (-1, uid, MessageFlags.Answered, userFlags));
			Assert.Throws<ArgumentOutOfRangeException> (() => new MessageFlagsChangedEventArgs (-1, uid, MessageFlags.Answered, userFlags, modseq));

			Assert.Throws<ArgumentNullException> (() => new MessageFlagsChangedEventArgs (0, MessageFlags.Answered, null));
			Assert.Throws<ArgumentNullException> (() => new MessageFlagsChangedEventArgs (0, MessageFlags.Answered, null, modseq));
			Assert.Throws<ArgumentNullException> (() => new MessageFlagsChangedEventArgs (0, uid, MessageFlags.Answered, null));
			Assert.Throws<ArgumentNullException> (() => new MessageFlagsChangedEventArgs (0, uid, MessageFlags.Answered, null, modseq));
		}
	}
}